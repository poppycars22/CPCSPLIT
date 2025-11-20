using System;
using HarmonyLib;
using ModdingUtils.Utils;
using CPCCore.Extensions;
using UnboundLib;
using Photon.Pun;
using UnboundLib.Networking;
using UnityEngine;
using System.Collections.Generic;
using CPCCommissions.Extensions;

namespace CPCCommissions.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "Hit")]
    class ProjectileHitPatchHit
    {
        // patch for random whynack
        private static bool Prefix(ProjectileHit __instance, ref HitInfo hit, ref Player ___ownPlayer, ref float ___damage, bool forceCall = false)
        {
            if (hit.collider.GetComponent<HealthHandler>() != null && ___ownPlayer.data.stats.GetAdditionalDataCPCCom().splitDmg)
            {
                if (__instance.view.IsMine || !__instance.sendCollisions)
                {
                    List<Player> aliveList = new List<Player>();
                    foreach (Player player2 in PlayerManager.instance.players)
                        if (PlayerStatus.PlayerAliveAndSimulated(player2) && player2.teamID != ___ownPlayer.teamID)
                            aliveList.Add(player2);

                    int player = UnityEngine.Random.Range(0, aliveList.Count);
                    //UnityEngine.Debug.Log("Alive list length: " + aliveList.Count + " Player in the alive list: " + player);
                    Player playerA = aliveList[player];

                    if (!hit.collider.GetComponent<HealthHandler>().GetComponent<Block>().IsBlocking())
                    {
                        playerA.data.healthHandler.CallTakeDamage(Vector2.up * ___damage * 0.25f, hit.point, null, ___ownPlayer, true);
                        ___damage *= 0.75f;
                    }
                }
            }
            if (hit.collider.GetComponent<HealthHandler>() !=null && ___ownPlayer.data.stats.GetAdditionalDataCPCCom().rngDmg)
            {
                if(__instance.view.IsMine || !__instance.sendCollisions)
                {
                        List<Player> aliveList = new List<Player>();
                        foreach(Player player2 in PlayerManager.instance.players)
                            if(PlayerStatus.PlayerAliveAndSimulated(player2))
                                aliveList.Add(player2);

                        int player = UnityEngine.Random.Range(0,aliveList.Count);
                        //UnityEngine.Debug.Log("Alive list length: " + aliveList.Count + " Player in the alive list: " + player);
                        Player playerA = aliveList[player];

                        if (!hit.collider.GetComponent<HealthHandler>().GetComponent<Block>().IsBlocking())
                        {
                            __instance.view.RPC("RPCA_DoHit", RpcTarget.All, hit.point, hit.normal, (Vector2)__instance.move.velocity, playerA.data.view.ViewID, -1, playerA.data.block.IsBlocking());
                            return false;
                        }
                }
                return true;
            }
            return true;
        }
    }
}