using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;
using CPCCore.MonoBehaviours;

namespace CPCComplex.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    [HarmonyPriority(Priority.LowerThanNormal)]
    class HealtHandlerPatchDoDamage
    {
        // patch for Totem and Damage Reduction
        private static bool Prefix(HealthHandler __instance, ref Vector2 damage, Vector2 position, Color blinkColor, GameObject damagingWeapon, Player damagingPlayer, bool healthRemoval, bool lethal, bool ignoreBlock)
        {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            Player player = data.player;
            if (!data.isPlaying)
            {
                return false;
            }

            if (data.dead)
            {
                return false;
            }

            if (__instance.isRespawning)
            {
                return false;
            }
            if (damagingPlayer != null && damagingPlayer.data.stats.GetAdditionalData().DamageAmpDamage > 0)
            {
                damage += damage.normalized * damagingPlayer.data.stats.GetAdditionalData().DamageAmpDamage;
            }
            if (player.data.stats.GetAdditionalData().BlackHole || player.data.currentCards.Contains(ModdingUtils.Utils.Cards.instance.GetCardWithObjectName("__CPC__Black Hole")))
            {
                player.data.stats.GetAdditionalData().BlackHole = true;
                Vector2 center = new Vector2(0, 0);
                Vector2 playerpos = player.gameObject.transform.position;
                damage /= (Mathf.Clamp((1/Mathf.Clamp(Vector2.Distance(center, playerpos),0.001f,9999)*10), 1, 10));
            }
            return true;
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "Heal")]
    class ExtraHealingPatch
    {
        private static void Prefix(HealthHandler __instance, ref float healAmount)
        {
            Player player = (Player)__instance.GetFieldValue("player");
            if(player.data.stats.GetAdditionalData().WhiteHole || player.data.currentCards.Contains(ModdingUtils.Utils.Cards.instance.GetCardWithObjectName("__CPC__White Hole")))
            {
                player.data.stats.GetAdditionalData().WhiteHole = true;
                Vector2 center = new Vector2(0, 0);
                Vector2 playerpos = player.gameObject.transform.position;
                healAmount *= Mathf.Clamp((Vector2.Distance(center, playerpos) / 10f), 1, 5f);
            }
        }
    }
}