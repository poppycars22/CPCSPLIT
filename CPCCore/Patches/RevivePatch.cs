using System;
using HarmonyLib;
using CPCCore.Extensions;


namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "Revive")]
    class HealtHandlerPatchRevive
    {
        // patch for Mulligan and PacPlayer
        private static void Prefix(HealthHandler __instance, bool isFullRevive = true)
        {
            if (isFullRevive)
            {
                ((CharacterData)Traverse.Create(__instance).Field("data").GetValue()).stats.GetAdditionalData().remainingTotems = ((CharacterData)Traverse.Create(__instance).Field("data").GetValue()).stats.GetAdditionalData().totems;
            }
        }
    }
}