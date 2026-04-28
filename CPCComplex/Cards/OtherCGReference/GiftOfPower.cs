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
using RarityLib.Utils;
using CPCComplex.MonoBehaviours;
using CPCCore.Extensions;
using PickPhaseImprovements;

namespace CPCComplex.Cards
{
    class GiftOfPower : CustomCard
    {
        protected override GameObject GetCardBase()
        {
            return this.gameObject.GetComponent<CardInfo>().cardBase;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            PickManager.RegisterShuffleCard(cardInfo, 4, false, condition: card => !PickManager.IsShuffleCard(card) && (RarityUtils.GetRarityData(card.rarity).relativeRarity >= RarityUtils.GetRarityData(CardInfo.Rarity.Common).relativeRarity));
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickManager.GiveConditionalPick(player, 4, false, condition: card => !PickManager.IsShuffleCard(card) && (RarityUtils.GetRarityData(card.rarity).relativeRarity >= RarityUtils.GetRarityData(CardInfo.Rarity.Common).relativeRarity));
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickManager.RemoveConditionalPick(player, new PickManager.ShuffleData() { Condition = card => !PickManager.IsShuffleCard(card) && (RarityUtils.GetRarityData(card.rarity).relativeRarity >= RarityUtils.GetRarityData(CardInfo.Rarity.Common).relativeRarity), HandSize = 4, Relative = false });
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override void OnReassignCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickManager.GiveConditionalPick(player, 4, false, condition: card => !PickManager.IsShuffleCard(card) && (RarityUtils.GetRarityData(card.rarity).relativeRarity >= RarityUtils.GetRarityData(CardInfo.Rarity.Common).relativeRarity));
        }

        protected override string GetTitle()
        {
            return "Gift of Power";
        }
        protected override string GetDescription()
        {
            return "Get an extra pick with four common (or lower) cards";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_GiftOfPower");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Rare");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
