using UnityEngine;
using CPCCore.Extensions;
using System;
using UnboundLib.Networking;
using UnboundLib;
using LuckLib;
using Photon.Pun;

namespace CPCClasses.MonoBehaviours
{
    public class NEWCritHitBehaviour : PreventMultipleObj
    {
        Gun gun;
        Player player;
        public Action<GameObject, int> CritHitAction;
        public void Start()
        {
            this.player = this.GetComponentInParent<Player>();
            this.gun = this.player.data.weaponHandler.gun;
            this.gun.ShootPojectileAction += this.OnShootProjectileAction;
            UnityEngine.Debug.Log(gun.ShootPojectileAction);
        }
        public void SyncCriticalHit(int critAmt, Player player, GameObject bullet)
        {
            NEWCritHitBehaviour critB = player.gameObject.GetComponentInChildren<NEWCritHitBehaviour>();
            SpawnedAttack spawnedAttack = bullet.GetComponent<SpawnedAttack>();
            if (critB.CritHitAction != null)
            {
                critB.CritHitAction(bullet, critAmt);
            }
            if (critAmt > 0)
            {
                if (critAmt == 1)
                    spawnedAttack.SetColor(critB.gun.GetAdditionalData().CritColor);
                else
                    spawnedAttack.SetColor(critB.gun.GetAdditionalData().DoubleCritColor);
                if (critB.gun.GetAdditionalData().criticalHitDamage1 > 0)
                {
                    bullet.GetComponent<ProjectileHit>().damage *= critB.gun.GetAdditionalData().criticalHitDamage1 * critAmt;
                }
            }
        }
        public void OnShootProjectileAction(GameObject obj)
        {
            if (player.data.view.IsMine)
            {
                int critCount = 0;
                Luck luck = player.GetComponent<Luck>();
                float chance = gun.GetAdditionalData().criticalHitChance1;

                while (luck.RollLuck(chance))
                {
                    critCount++;
                    chance -= 1f;
                }
                if (gun.GetAdditionalData().guranteedCrits)
                    critCount = 1;
                obj.GetComponent<ChildRPC>().CallFunction("CallCritSync", critCount);
            }
        }
        public void OnDestroy()
        {
            gun.ShootPojectileAction -= OnShootProjectileAction;
        }
    }
}