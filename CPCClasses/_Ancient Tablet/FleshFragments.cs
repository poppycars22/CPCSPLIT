using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BepInEx;
using CPCCore.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using ModdingUtils.MonoBehaviours;
using ClassesManagerReborn.Util;
using CPCCore.Extensions;
using PSA.Extensions;

namespace CPCClasses.Cards
{
    class FleshFragments : CustomCard {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCardsClasses.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PSA.Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).damageMult += 0.25f;
            CPCCore.Extensions.CharacterStatModifiersExtension.GetAdditionalData(characterStats).damagingBullet = true;
            CPCDebug.Log($"[{ChaosPoppycarsCardsClasses.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsClasses.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Flesh Fragments";
        }
        protected override string GetDescription()
        {
            return "Whenever you get hit you release bits of your flesh as projectiles";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsClasses.Bundle.LoadAsset<GameObject>("C_FleshFragments");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = AncientClass.name;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage Taken",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeLib.CardThemeLib.instance.CreateOrGetType("Ancient Pumpkin Orange");
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
