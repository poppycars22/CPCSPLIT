using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;

namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(GeneralInput), "Update")]
    [HarmonyPriority(Priority.First)]
    class GeneralInputUpdatePatch
    {
        // patch for forced move
        private static void Postfix(GeneralInput __instance)
        {
            if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().forcedMoveEnabled || (__instance.GetComponent<Player>().data.stats.GetAdditionalData().whynackAd && !__instance.GetComponent<Player>().data.stats.GetAdditionalData().whynackHarmony))
            { 
                if(Mathf.Abs(__instance.direction.x) <0.5f)
                {
                    __instance.direction = new Vector3(__instance.GetComponent<Player>().data.stats.GetAdditionalData().forcedMove, __instance.direction.y);
                }
                else
                {
                    __instance.GetComponent<Player>().data.stats.GetAdditionalData().forcedMove = __instance.direction.x;
                }
            }
        }
    }
}