using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;
using CPCCore.MonoBehaviours;

namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    [HarmonyPriority(Priority.LowerThanNormal)]
    class HealtHandlerPatchDoDamage
    {
        // patch for Totem and Damage Reduction
        private static bool Prefix(HealthHandler __instance, ref Vector2 damage, Vector2 position, Color blinkColor, GameObject damagingWeapon, Player damagingPlayer, bool healthRemoval, ref bool lethal, bool ignoreBlock)
        {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            Player player = data.player;
            if (!data.isPlaying)
            {
                return false;
            }

            if (data.dead)
            {
                return false;
            }

            if (__instance.isRespawning)
            {
                return false;
            }

            if (damagingPlayer != null && damagingPlayer.data.stats.GetAdditionalData().firstDamage == true)
            {
                damagingPlayer.data.stats.GetAdditionalData().damageMult = damagingPlayer.data.stats.GetAdditionalData().damageMultMax;
                damagingPlayer.data.stats.GetAdditionalData().firstDamage = false;
            }

            if (damagingPlayer != null && damagingPlayer.data.stats.GetAdditionalData().reducingDmg)
            {
                damage *= damagingPlayer.data.stats.GetAdditionalData().damageMult;
                if (damagingPlayer.data.stats.GetAdditionalData().damageMult > 0.05f)
                    damagingPlayer.data.stats.GetAdditionalData().damageMult -= damagingPlayer.data.stats.GetAdditionalData().reducingDmgAmt;
                if (damagingPlayer.data.stats.GetAdditionalData().damageMult < 0.05f)
                    damagingPlayer.data.stats.GetAdditionalData().damageMult = 0.05f;
            }

            if (player.data.stats.GetAdditionalData().firstHit == true)
            {
                player.data.stats.GetAdditionalData().firstHit = false;
                if (player.data.stats.GetAdditionalData().firstHitdmgReduction > 0)
                    damage /= player.data.stats.GetAdditionalData().firstHitdmgReduction;
            }

            /*if (player.data.stats.GetAdditionalData().storeDamage && !player.data.stats.GetAdditionalData().takeStoredDamage)
            {
                player.data.stats.GetAdditionalData().storedDamage += damage;
                if (player.gameObject.GetOrAddComponent<StoredDmgCD>().duration <= 0)
                    player.gameObject.GetOrAddComponent<StoredDmgCD>().duration = 5;
                return false;
            }
            else if (player.data.stats.GetAdditionalData().storeDamage && player.data.stats.GetAdditionalData().takeStoredDamage)
            {
                player.data.stats.GetAdditionalData().takeStoredDamage = false;
                damage += player.data.stats.GetAdditionalData().storedDamage;
                player.data.stats.GetAdditionalData().storedDamage = Vector2.zero;
            }*/

            return true;
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(Block), "blocked")]
    class BlockPiercePatch
    {
        private static bool Prefix(Block __instance, GameObject projectile, Vector3 forward, Vector3 hitPos)
        {
            bool destroy = false;
            ProjectileHit proj = projectile.GetComponent<ProjectileHit>();
            HealthHandler healthHandler = (HealthHandler)Traverse.Create(__instance).Field("health").GetValue();

            if (projectile.GetComponent<ProjectileHit>().ownPlayer.data.stats.GetAdditionalData().blockPierce > 0)
            {
                Vector2 damage = ((proj.bulletCanDealDeamage ? proj.damage : 1f) * projectile.GetComponent<ProjectileHit>().ownPlayer.data.stats.GetAdditionalData().blockPierce) * forward.normalized;
                healthHandler.TakeDamage(damage, hitPos, proj.projectileColor, proj.ownWeapon, proj.ownPlayer, true, true);
                destroy = true;
            }
            if (destroy)
            {
                // destroy the bullet
                UnityEngine.GameObject.Destroy(projectile);
                return false;
            }
            return true;
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    class UpwardsKnockbackPatch
    {
        private static void Prefix(ProjectileHit __instance, Vector2 hitPoint, Vector2 hitNormal, Vector2 vel, int viewID, int colliderID, bool wasBlocked)
        {
            HitInfo hitInfo = new HitInfo();
            hitInfo.point = hitPoint;
            hitInfo.normal = hitNormal;
            hitInfo.collider = null;
            if (viewID != -1)
            {
                PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);
                hitInfo.collider = photonView.GetComponentInChildren<Collider2D>();
                hitInfo.transform = photonView.transform;
            }
            else if (colliderID != -1)
            {
                hitInfo.collider = MapManager.instance.currentMap.Map.GetComponentsInChildren<Collider2D>()[colliderID];
                hitInfo.transform = hitInfo.collider.transform;
            }
            HealthHandler healthHandler = null;
            if ((bool)hitInfo.transform)
            {
                healthHandler = hitInfo.transform.GetComponent<HealthHandler>();
            }
            PlayerVelocity playerVelocity = null;
            if ((bool)hitInfo.transform)
            {
                playerVelocity = hitInfo.transform.GetComponentInParent<PlayerVelocity>();
            }

            if ((bool)playerVelocity)
            {
                float num2 = 1f;
                float num3 = Mathf.Clamp(playerVelocity.mass / 100f * num2, 0f, 1f) * num2;
                if ((bool)healthHandler)
                {
                    if (__instance.hasControl && __instance.spawnedAttack.spawner != null && __instance.spawnedAttack.spawner.data.stats.GetAdditionalData().upwardsKnockback != 0)
                    {
                        healthHandler.CallTakeForce(Vector2.up * num3 * __instance.spawnedAttack.spawner.data.stats.GetAdditionalData().upwardsKnockback * Mathf.Pow(__instance.damage / 55f, 2f) * 5000, ForceMode2D.Impulse, true, false, 0);
                    }
                }
            }

            if ((bool)hitInfo.transform)
            {
                NetworkPhysicsObject component4 = hitInfo.transform.GetComponent<NetworkPhysicsObject>();
                if ((bool)component4 && __instance.canPushBox && __instance.spawnedAttack.spawner != null && __instance.spawnedAttack.spawner.data.stats.GetAdditionalData().upwardsKnockback != 0)
                {
                    component4.BulletPush(Vector2.up * ((__instance.spawnedAttack.spawner.data.stats.GetAdditionalData().upwardsKnockback + 5) * Mathf.Pow(__instance.damage / 55f, 2f)) * 5000, hitInfo.transform.InverseTransformPoint(component4.transform.position), __instance.spawnedAttack.spawner.data);
                }
            }
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "Heal")]
    class ExtraHealingPatch
    {
        private static void Prefix(HealthHandler __instance, ref float healAmount)
        {
            Player player = (Player)__instance.GetFieldValue("player");
            healAmount *= player.data.stats.GetAdditionalData().ExtraHeal;
        }
    }
}