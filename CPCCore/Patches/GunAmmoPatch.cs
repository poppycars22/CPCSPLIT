using System;
using HarmonyLib;
using CPCCore.Extensions;
using UnityEngine;


namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(GunAmmo), "Shoot")]
    class GunUSEAMMOPatch
    {
        // patch for Wormhole Clip
        private static void Postfix(GunAmmo __instance, GameObject projectile)
        {
            if(!__instance.gun.player.data.stats.GetAdditionalData().useAmmo)
            {
                __instance.gun.isReloading = false;
                __instance.currentAmmo = __instance.maxAmmo;
                __instance.SetActiveBullets();
            }
        }
    }
}