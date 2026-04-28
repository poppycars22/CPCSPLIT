using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using CPCCore;
using CPCCore.Extensions;
using CPCCore.Utilities;
using HarmonyLib;
using LuckLib;
using ModdingUtils.Utils;
using Nullmanager;
using PickPhaseImprovements;
using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.Extensions;
using WillsWackyManagers.Utils;

namespace CPCComplex.Cards
{
    class ReforgeShuffle : CustomCard
    {
        public override bool GetEnabled()
        {
            return false;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.NeedsNull();
            cardInfo.MarkUnNullable();
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { ChaosPoppycarsCardsCore.CPCCoreCardCategories.PoppysChaosCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int nullcount = player.GetNullCount();
            characterStats.GetAdditionalData().nullData.nullShuffles += 1;
            characterStats.GetAdditionalData().nullData.nullCurses += 1;
            for (int i = 0; i < nullcount; i++)
            {
                PickManager.QueueShuffleForPicker(player);
                CurseManager.instance.CursePlayer(player, (curse) =>
                {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
            }
            ChaosPoppycarsCardsCore.UpdateNullStatsForPlayer(player);
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override void OnReassignCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().nullData.nullShuffles += 1;
            characterStats.GetAdditionalData().nullData.nullCurses += 1;
        }

        protected override string GetTitle()
        {
            return "Reforge Shuffle";
        }
        protected override string GetDescription()
        {
            return "Nulls give you +1 Shuffle and +1 Curse";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_NullPlayers");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Divine");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return "CPC";
        }
        
    }
}
