using System;
using CPCClasses.MonoBehaviours;
using ModdingUtils.MonoBehaviours;
using ModdingUtils.RoundsEffects;
using UnboundLib;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class WarpathMono : MonoBehaviour
    {
        private CharacterData data;
        private Player player;
        private Block block;
        private WeaponHandler weaponHandler;
        private Gun gun;
        private void Start()
        {
            this.data = this.gameObject.GetComponentInParent<CharacterData>();
        }
        private void Update()
        {
            

            if (!player)
            {
                if (!(data is null))
                {
                    player = data.player;
                    block = data.block;
                    weaponHandler = data.weaponHandler;
                    gun = weaponHandler.gun;

                    gun.ShootPojectileAction += OnShootProjectileAction;
                }

            }
        }
        private void OnShootProjectileAction(GameObject obj)
        {

            Vector2 velocity = (Vector2)this.data.playerVel.GetFieldValue("velocity");
            Vector2 aim = this.data.input.aimDirection;
            float angle = Vector2.Angle(velocity.normalized, aim.normalized);
            float directionMult = (90 - angle) / 90;
            float speedMult = velocity.magnitude/ 17;
            ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
            
            //UnityEngine.Debug.Log($"[{velocity}] {(bullet.damage)}");
            bullet.damage *= (speedMult) + (directionMult * speedMult);
            
            //UnityEngine.Debug.Log($"[{velocity}] {(bullet.damage)}");
        }
        private void OnDestroy()
        {
            gun.ShootPojectileAction -= OnShootProjectileAction;
        }
    }
}