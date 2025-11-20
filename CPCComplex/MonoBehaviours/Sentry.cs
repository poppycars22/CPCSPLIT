using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using System.Linq;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ModdingUtils.GameModes;
using System.Security.Cryptography;
using CPCCore.Utilities;
using CPCCore.MonoBehaviours;

namespace CPCComplex.MonoBehaviours
{
    public class SentryGunMono : MonoBehaviour, IPointEndHookHandler
    {
        Block block;
        Player player;
        Gun gun;
        GameObject test;
        Vector3 temp;
        Vector3 aim;
        int ammo = 3;
        float cd = 0;
        private PhotonView photonView;
        public SimulatedGun[] savedGuns = new SimulatedGun[1];

        public void OnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
            block.BlockAction -= OnBlock;
            gun.ShootPojectileAction -= OnShootProjectileAction;
            Destroy(test);
            UnityEngine.GameObject.Destroy(savedGuns[0]);
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if(test==null)
                test = Object.Instantiate(ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("TestObj"));
            temp = player.transform.position;
            aim = new Vector3(player.data.input.aimDirection.x, player.data.input.aimDirection.y, 0);
            test.transform.position = temp;
            test.GetComponent<SpriteRenderer>().color = player.GetTeamColors().color;
            ammo = gun.gunAmmo.maxAmmo *2;
        }
        public void OnShootProjectileAction(GameObject obj)
        {
            if (obj.GetComponentsInChildren<StopRecursion>().Length > 0)
            {
                return;
            }
            SimulatedGun Sentry = savedGuns[0];
            Sentry.CopyGunStatsExceptActions(this.gun);
            Sentry.CopyAttackAction(this.gun);
            Sentry.CopyShootProjectileAction(this.gun);
            Sentry.ShootPojectileAction -= this.OnShootProjectileAction;
            Sentry.objectsToSpawn = Sentry.objectsToSpawn.Concat(StopRecursionMono.StopRecursionSpawn).ToArray();
            Sentry.bursts = 0;
            Sentry.damage = gun.damage/2;
            Sentry.numberOfProjectiles = 1;
            ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(2, () =>
            {
                if (test != null && ammo > 0 && cd <= 0)
                {
                    Sentry.SimulatedAttack(this.player.playerID, temp, aim, 1f, 1);
                    ammo--;
                    cd += 0.1f;
                }
            });
        }
        public void OnPointEnd()
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Test] Round Ended");
            if (test != null)
                Destroy(test);
        }
        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            photonView = GetComponent<PhotonView>();
            this.player = gameObject.GetComponent<Player>();
            this.gun = this.player.data.weaponHandler.gun;
            this.block = this.player.GetComponent<Block>();
            if (savedGuns[0] == null)
            {
                savedGuns[0] = new GameObject("BlockBullets").AddComponent<SimulatedGun>();
            }
            block.BlockAction += OnBlock;
            this.gun.ShootPojectileAction += this.OnShootProjectileAction;
        }
        public void Update()
        {     
            if (test != null && player.data.view.IsMine)
            {
                Vector3 Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - test.transform.position;
                float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 45;
                test.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                aim = new Vector3(Direction.x, Direction.y, 0);
                photonView.RPC("SyncDir", RpcTarget.Others, angle, Direction);
            }
            if (cd > 0)
                cd -= TimeHandler.deltaTime;
        }
        [PunRPC]
        public void SyncDir(float angle, Vector3 Direction)
        {
            test.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            aim = new Vector3(Direction.x, Direction.y, 0);
        }
    }
}
