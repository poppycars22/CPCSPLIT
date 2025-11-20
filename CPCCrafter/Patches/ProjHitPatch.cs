using System;
using HarmonyLib;
using ModdingUtils.Utils;
using CPCCore.Extensions;
using UnboundLib;
using Photon.Pun;
using UnboundLib.Networking;
using UnityEngine;
using System.Collections.Generic;
using Photon.Pun.Simple;
using CPCComplex.MonoBehaviours;

namespace CPCCrafter.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "Hit")]
    class ProjHitPatch
    {
        // patch for enderman
        private static bool Prefix(ProjectileHit __instance, ref HitInfo hit, ref Player ___ownPlayer, ref float ___damage, bool forceCall = false)
        {
            if (hit.collider.GetComponent<HealthHandler>() != null && hit.collider.GetComponent<Player>().GetComponentInChildren<EndermenTeleport>() && (__instance.view.IsMine || PhotonNetwork.OfflineMode))
            {
                Player player = hit.collider.GetComponent<Player>();
                EndermenTeleport temp = player.GetComponentInChildren<EndermenTeleport>();
                if (temp.CanTrigger(___damage, player.playerID == ___ownPlayer.playerID))
                {
                    player.gameObject.GetOrAddComponent<RPCEndermanMono>();
                    player.data.view.RPC("RPCAEndermanTP", RpcTarget.All, player.playerID, ___damage, player.playerID == ___ownPlayer.playerID, __instance.view.ViewID);
                    return false;
                }
            }
            return true;
        }
    }
}