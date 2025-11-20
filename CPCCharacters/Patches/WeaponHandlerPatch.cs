using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;
using ModdingUtils.Utils;

namespace CPCCharacters.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(WeaponHandler), "Update")]
    class WeaponHandlerPatch
    {
        // patch for Whynack Adrenaline
        private static void Prefix(WeaponHandler __instance)
        {
            CharacterData data = __instance.data;
            if (PlayerStatus.PlayerAliveAndSimulated(data.player) && data.stats.GetAdditionalData().whynackAd && !data.stats.GetAdditionalData().whynackHarmony)
            {
                __instance.input.shootWasPressed = true;
                __instance.input.shootIsPressed = true;
            }
        }
    }

}