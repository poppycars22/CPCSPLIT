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
using ModdingUtils.MonoBehaviours;
using CPCCore.Extensions;
using RarityLib.Utils;

namespace CPCClassic.Cards
{
    class NanoMachines : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = 1.15f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsClassic.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().NanoMachines += 1;
            HealthBasedEffect Above90 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above90.healthHandlerModifier.regen_add += 0.1f * ((characterStats.GetAdditionalData().NanoMachines + 1)/2);
            Above90.SetPercThresholdMax(1f);
            Above90.SetPercThresholdMin(0.9f);
            HealthBasedEffect Above80 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above80.healthHandlerModifier.regen_add += 0.5f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above80.SetPercThresholdMax(0.9f);
            Above80.SetPercThresholdMin(0.8f);
            HealthBasedEffect Above70 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above70.healthHandlerModifier.regen_add += 1f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above70.SetPercThresholdMax(0.8f);
            Above70.SetPercThresholdMin(0.7f);
            HealthBasedEffect Above60 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above60.healthHandlerModifier.regen_add += 2f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above60.SetPercThresholdMax(0.7f);
            Above60.SetPercThresholdMin(0.6f);
            HealthBasedEffect Above50 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above50.healthHandlerModifier.regen_add += 4f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above50.SetPercThresholdMax(0.6f);
            Above50.SetPercThresholdMin(0.5f);
            HealthBasedEffect Above40 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above40.healthHandlerModifier.regen_add += 6f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above40.SetPercThresholdMax(0.5f);
            Above40.SetPercThresholdMin(0.4f);
            HealthBasedEffect Above30 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above30.healthHandlerModifier.regen_add += 8f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above30.SetPercThresholdMax(0.4f);
            Above30.SetPercThresholdMin(0.3f);
            HealthBasedEffect Above20 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above20.healthHandlerModifier.regen_add += 10f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above20.SetPercThresholdMax(0.3f);
            Above20.SetPercThresholdMin(0.2f);
            HealthBasedEffect Above10 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above10.healthHandlerModifier.regen_add += 12f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above10.SetPercThresholdMax(0.2f);
            Above10.SetPercThresholdMin(0.1f);
            HealthBasedEffect Above0 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above0.healthHandlerModifier.regen_add += 15f * ((characterStats.GetAdditionalData().NanoMachines + 1) / 2);
            Above0.SetPercThresholdMax(0.1f);
            Above0.SetPercThresholdMin(0f);
            CPCDebug.Log($"[{ChaosPoppycarsCardsClassic.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsClassic.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Nano Machines";
        }
        protected override string GetDescription()
        {
            return "Nanomachines, son. The lower your health gets the higher your regen gets (getting this card more then once increases the regen)";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsClassic.Bundle.LoadAsset<GameObject>("C_NanoMachines");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Exotic");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
               new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
