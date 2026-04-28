using System;
using HarmonyLib;
using CPCCore.Extensions;
using MapEmbiggener.Patches;
using System.Linq;
using UnityEngine;
using MapEmbiggener.Controllers;
using Photon.Pun;


namespace CPCCore.Patches
{
                    
    [Serializable]
    [HarmonyPatch(typeof(MapEmbiggener.Controllers.MapController), "OnPointEnd")]
    class MapSizePatch
    {
        // patch for Map size increase
        private static void Postfix(MapEmbiggener.Controllers.MapController __instance)
        {
            float increase = 0;
            foreach (Player player in PlayerManager.instance.players)
            {
                increase += player.data.stats.GetAdditionalData().mapSizeI;
            }
            increase = Mathf.Clamp(MapEmbiggener.MapEmbiggener.setSize + increase, 0.25f, 7);
            __instance.MapSize = increase;
        }
    }
}