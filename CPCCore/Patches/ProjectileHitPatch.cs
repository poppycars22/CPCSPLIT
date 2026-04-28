using System;
using HarmonyLib;
using CPCCore.MonoBehaviours;
using CPCCore.Extensions;
using UnboundLib;
using Photon.Pun;
using UnboundLib.Networking;
using UnityEngine;
using Photon.Realtime;

namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "Hit")]
    class ProjectileHitPatchHit
    {
        // patch for block mover
        private static void Postfix(ProjectileHit __instance, ref HitInfo hit, ref Player ___ownPlayer, ref float ___damage, bool forceCall = false)
        {
            if(hit.collider.GetComponent<HealthHandler>() !=null)
            {
                if (___ownPlayer != null && ___ownPlayer.data.stats.GetAdditionalData().firstDamage == true)
                {
                    ___ownPlayer.data.stats.GetAdditionalData().damageMult = ___ownPlayer.data.stats.GetAdditionalData().damageMultMax;
                    ___ownPlayer.data.stats.GetAdditionalData().firstDamage = false;
                }
                if (___ownPlayer != null && ___ownPlayer.data.stats.GetAdditionalData().reducingDmg)
                {
                    ___damage *= ___ownPlayer.data.stats.GetAdditionalData().damageMult;
                    if (___ownPlayer.data.stats.GetAdditionalData().damageMult > 0.05f)
                        ___ownPlayer.data.stats.GetAdditionalData().damageMult -= ___ownPlayer.data.stats.GetAdditionalData().reducingDmgAmt;
                    if (___ownPlayer.data.stats.GetAdditionalData().damageMult < 0.05f)
                        ___ownPlayer.data.stats.GetAdditionalData().damageMult = 0.05f;
                }
            }
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    class ProjectileHitPatchRPCA_DoHit
    {
        private static void Postfix(ProjectileHit __instance, Vector2 hitPoint, Vector2 hitNormal, Vector2 vel, ref Player ___ownPlayer, ref float ___damage, int viewID = -1, int colliderID = -1, bool wasBlocked = false)
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
            if (hitInfo.collider != null && hitInfo.collider.GetComponent<HealthHandler>() == null && ___ownPlayer != null && hitInfo.transform != null && ___ownPlayer.GetComponent<CooldownBlock>() != null && ___ownPlayer.GetComponent<CooldownBlock>().duration <= 0)
            {
                if (___ownPlayer.data.stats.GetAdditionalData().blockMover && !___ownPlayer.data.stats.GetAdditionalData().blockPush)
                {
                    if (___ownPlayer.data.view.IsMine && !PhotonNetwork.OfflineMode)
                    {
                        NetworkingManager.RPC(typeof(ProjectileHitPatchRPCA_DoHit), nameof(BugFix), new object[] { viewID, colliderID, hitPoint, hitNormal, true, ___ownPlayer.playerID });
                    }
                    else if (PhotonNetwork.OfflineMode)
                    {
                        var move = hitInfo.transform.gameObject.GetOrAddComponent<Smooth>();
                        move.currentPos = hitInfo.transform.position;
                        move.targetPos = hitInfo.transform.position + ((___ownPlayer.transform.position - hitInfo.transform.position).normalized * ___ownPlayer.data.stats.GetAdditionalData().blockMoveStrength * 3);
                        move.targetPos *= Mathf.Log(___ownPlayer.data.weaponHandler.gun.damage * ___ownPlayer.data.weaponHandler.gun.bulletDamageMultiplier, ___ownPlayer.data.weaponHandler.gun.projectiles[0].objectToSpawn.GetComponent<ProjectileHit>().damage)+1;
                        move.speed = 2;
                    }
                    ___ownPlayer.GetComponent<CooldownBlock>().duration = 3;
                }
                else if (___ownPlayer.data.stats.GetAdditionalData().blockMover)
                {
                    if (___ownPlayer.data.view.IsMine && !PhotonNetwork.OfflineMode)
                    {
                        NetworkingManager.RPC(typeof(ProjectileHitPatchRPCA_DoHit), nameof(BugFix), new object[] { viewID, colliderID, hitPoint, hitNormal, false, ___ownPlayer.playerID });
                    }
                    else if (PhotonNetwork.OfflineMode)
                    {
                        var move = hitInfo.transform.gameObject.GetOrAddComponent<Smooth>();
                        move.currentPos = hitInfo.transform.position;
                        move.targetPos = hitInfo.transform.position + ((hitInfo.transform.position - ___ownPlayer.transform.position).normalized * ___ownPlayer.data.stats.GetAdditionalData().blockMoveStrength * 3);
                        move.targetPos *= Mathf.Log(___ownPlayer.data.weaponHandler.gun.damage * ___ownPlayer.data.weaponHandler.gun.bulletDamageMultiplier, ___ownPlayer.data.weaponHandler.gun.projectiles[0].objectToSpawn.GetComponent<ProjectileHit>().damage)+1;
                        move.speed = 2;
                    }
                    ___ownPlayer.GetComponent<CooldownBlock>().duration = 3;
                }
            }
        }

        [UnboundRPC]
        private static void BugFix(int viewID, int colliderID, Vector2 hitPoint, Vector2 hitNormal, bool pull, int playerID)
        {
            HitInfo hitInfo = new HitInfo();
            hitInfo.point = hitPoint;
            hitInfo.normal = hitNormal;
            hitInfo.collider = null;
            Player player = PlayerManager.instance.GetPlayerWithID(playerID);
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

            if (pull)
            {
                var move = hitInfo.transform.gameObject.GetOrAddComponent<Smooth>();
                move.currentPos = hitInfo.transform.position;
                move.targetPos = hitInfo.transform.position + ((player.transform.position - hitInfo.transform.position).normalized * player.data.stats.GetAdditionalData().blockMoveStrength * (Mathf.Log(player.data.weaponHandler.gun.damage * player.data.weaponHandler.gun.bulletDamageMultiplier, player.data.weaponHandler.gun.projectiles[0].objectToSpawn.GetComponent<ProjectileHit>().damage) + 1));
                //move.targetPos *= Mathf.Log(player.data.weaponHandler.gun.damage * player.data.weaponHandler.gun.bulletDamageMultiplier, player.data.weaponHandler.gun.projectiles[0].objectToSpawn.GetComponent<ProjectileHit>().damage)+1;
                move.speed = 2;
            }
            else
            {
                var move = hitInfo.transform.gameObject.GetOrAddComponent<Smooth>();
                move.currentPos = hitInfo.transform.position;
                move.targetPos = hitInfo.transform.position + ((hitInfo.transform.position - player.transform.position).normalized * player.data.stats.GetAdditionalData().blockMoveStrength * (Mathf.Log(player.data.weaponHandler.gun.damage * player.data.weaponHandler.gun.bulletDamageMultiplier, player.data.weaponHandler.gun.projectiles[0].objectToSpawn.GetComponent<ProjectileHit>().damage) + 1));
                //move.targetPos *= Mathf.Log(player.data.weaponHandler.gun.damage * player.data.weaponHandler.gun.bulletDamageMultiplier, player.data.weaponHandler.gun.projectiles[0].objectToSpawn.GetComponent<ProjectileHit>().damage)+1;
                move.speed = 2;
            }
        }
    }
}