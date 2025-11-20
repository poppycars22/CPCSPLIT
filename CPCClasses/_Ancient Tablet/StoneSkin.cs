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
using PSA.Extensions;
using CPCClasses.MonoBehaviours;

namespace CPCClasses.Cards
{
    class StoneSkin : CustomCard {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
           
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCardsClasses.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (characterStats.GetAdditionalData().damageReductionFlat < 0.5f)
            {
                characterStats.GetAdditionalData().damageReductionFlat += 0.5f;
            }
            player.gameObject.AddComponent<StoneSkinEffect>();
            CPCDebug.Log($"[{ChaosPoppycarsCardsClasses.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<StoneSkinEffect>();
            UnityEngine.GameObject.Destroy(mono);
            CPCDebug.Log($"[{ChaosPoppycarsCardsClasses.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Stone Skin";
        }
        protected override string GetDescription()
        {
            return "Your skin becomes stone, <i>a hard stone</i>, try not to stay still";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsClasses.Bundle.LoadAsset<GameObject>("C_StoneSkin");
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
                    positive = true,
                    stat = "Damage Reduction",
                    amount = "+50%",
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
