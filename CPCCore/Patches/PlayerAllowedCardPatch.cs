using ClassesManagerReborn;
using ClassesManagerReborn.Patchs;
using CPCCore.Extensions;
using HarmonyLib;
using ModdingUtils.Patches;
using ModdingUtils.Utils;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;


namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(ModdingUtils.Utils.Cards), "PlayerIsAllowedCard")]
    [HarmonyPriority(int.MinValue)]
    class PlayerAlowedCardPatch
    {
        // patch for Mcnally
        private static void Postfix(ModdingUtils.Utils.Cards __instance, ref bool __result, Player player, CardInfo card)
        {
            if(card != null && player != null)
            {
                if (player.data.stats.GetAdditionalData().Mcnally)
                {
                    __result = true;
                }
            }
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(ModdingUtils.Utils.Cards), "GetRandomCardWithCondition")]
    class PlayerAlowedHiddenCardPatch
    {
        public static bool Prefix(ModdingUtils.Utils.Cards __instance, ref CardInfo __result, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats, Func<CardInfo, Player, Gun, GunAmmo, CharacterData, HealthHandler, Gravity, Block, CharacterStatModifiers, bool> condition)
        {
            if(player!=null && player.data.stats.GetAdditionalData().Mcnally)
            {
                //CardInfo[] cards = __instance.activeCards.AddRangeToArray(ModdingUtils.Utils.Cards.instance.HiddenCards.Where(c => ((DefaultPool)PhotonNetwork.PrefabPool).ResourceCache.ContainsKey(c.name)).ToArray()).Where(card => condition(card, player, gun, gunAmmo, data, health, gravity, block, characterStats)).ToArray();
                CardInfo[] cards = __instance.activeCards.Where(card => condition(card, player, gun, gunAmmo, data, health, gravity, block, characterStats) && __instance.PlayerIsAllowedCard(player, card)).ToArray();
                cards.AddRangeToArray(__instance.HiddenCards.Where(card => condition(card, player, gun, gunAmmo, data, health, gravity, block, characterStats) && ((DefaultPool)PhotonNetwork.PrefabPool).ResourceCache.ContainsKey(card.name)).ToArray());
                if (cards.Length == 0)
                {
                    __result = null;
                    return false;
                }
                else
                {
                    __result = CardChoicePatchGetRanomCard.OrignialGetRanomCard(cards).GetComponent<CardInfo>();
                    return false;
                }
            }
            return true;
        }
    }
}