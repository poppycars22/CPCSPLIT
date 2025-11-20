using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using ModdingUtils.Utils;

namespace CPCCharacters.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(Block), "TryBlock")]
    class TryBlockPatch
    {
        // patch for Whynack Goku
        private static void Prefix(Block __instance)
        {
           CharacterData data = __instance.data;
           if(data.stats.GetAdditionalData().whynackBlockForce)
           {
                if (data.block.forceToAdd != 0f)
                {
                    data.player.GetComponent<HealthHandler>().TakeForce(data.hand.transform.forward * data.block.forceToAdd * data.playerVel.mass * 10f);
                }

                if (data.block.forceToAddUp != 0f)
                {
                    data.player.GetComponent<HealthHandler>().TakeForce(Vector3.up * data.block.forceToAddUp * data.playerVel.mass * 10f);
                }
                return;
           }
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(Block), "Update")]
    class BlockPatch
    {
        // patch for Whynack Adrenaline
        private static void Postfix(Block __instance)
        {
            CharacterData data = __instance.data;
            if ((!(__instance.counter < __instance.Cooldown())) && data != null && data.player != null && data.stats.GetAdditionalData().whynackAd && !data.stats.GetAdditionalData().whynackHarmony && PlayerStatus.PlayerAliveAndSimulated(data.player))
            {
                __instance.TryBlock();
            }
        }
    }
}