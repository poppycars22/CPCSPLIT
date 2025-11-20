using System;
using HarmonyLib;
using CPCCrafter.MonoBehaviours;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;

namespace CPCCrafter.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    class HealtHandlerPatchDoDamage
    {
        // patch for Totem and Damage Reduction
        private static void Prefix(HealthHandler __instance, ref Vector2 damage, Vector2 position, Color blinkColor, GameObject damagingWeapon, Player damagingPlayer, bool healthRemoval, ref bool lethal, bool ignoreBlock)
        {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            Player player = data.player;
            if (!data.isPlaying)
            {
                return;
            }
            if (data.dead)
            {
                return;
            }
            if (__instance.isRespawning)
            {
                return;
            }
            if (lethal && data.health < damage.magnitude && data.stats.GetAdditionalData().remainingTotems > 0)
            {
                if (player.GetComponent<TotemEffect>() != null && player.GetComponent<TotemEffect>().cd <=0)
                { player.GetComponent<TotemEffect>().UseMulligan(); }
                else { return; }

                lethal = false;
            }
           
        }
    }
}