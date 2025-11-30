using ClassesManagerReborn;
using ClassesManagerReborn.Patchs;
using CPCCore.Extensions;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;


namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(ModdingUtils.Utils.Cards), "PlayerIsAllowedCard")]
    [HarmonyPriority(Priority.First)]
    class PlayerAlowedCardPatch
    {
        // patch for Mcnally [doesnt work with CMR currently, find fix later]
        private static bool Prefix(ModdingUtils.Utils.Cards __instance, ref bool __result, Player player, CardInfo card)
        {
            if(card != null)
            {
                if (player.data.stats.GetAdditionalData().Mcnally)
                {
                    
                    __result = true;
                    return false;
                }
            }
            return true;
        }
    }
}