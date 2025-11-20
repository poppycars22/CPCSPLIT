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
    [HarmonyPatch(typeof(Block), "TryBlock")]
    class BlockPatch
    {
        // patch for Trident
        private static void Prefix(Block __instance)
        {
           CharacterData data = __instance.data;
           if(data.stats.GetAdditionalData().hasTrident || true /*debug*/)
           {
                //.player.GetComponent<Gravity>().exponent = -0.1f;
                //UnityEngine.Debug.Log("yippie");
                return;
           }
        }
    }
}