using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;
using ModdingUtils.Utils;
using Photon.Realtime;

namespace CPCCharacters.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "Heal")]
    class RegenerationPatch
    {
        // patch for Whynack Meditating
        private static void Prefix(HealthHandler __instance, ref float healAmount)
        {
            CharacterData data = __instance.data;
            if ((data.input.direction == Vector3.zero || data.input.direction == Vector3.down) && PlayerStatus.PlayerAliveAndSimulated(data.player) && data.stats.GetAdditionalData().whynackMeditating)
            {
                healAmount *= 1.5f;
            }
        }
    }

}