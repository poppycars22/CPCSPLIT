using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CPCCore.Utilities;
using CPCCore;
using CPCCore.Extensions;
using RarityLib.Utils;
using PickPhaseImprovements;

namespace CPCComplex.Cards
{
    class ChaosPickOutcome : CustomCard
    {
        public override bool GetEnabled()
        {
            return false;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            ModdingUtils.Extensions.CardInfoExtension.GetAdditionalData(cardInfo).canBeReassigned = false;
            PickManager.RegisterShuffleCard(cardInfo, 0, false, condition: card => card.gameObject.name.Equals("__CPC__Poppys Chaos"));
            cardInfo.categories = new CardCategory[] { ChaosPoppycarsCardsCore.CPCCoreCardCategories.PoppysChaosCategory };
            
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickManager.GiveConditionalPick(player, 0, false, condition: card => card.gameObject.name.Equals("__CPC__Poppys Chaos"));
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickManager.RemoveConditionalPick(player, new PickManager.ShuffleData() { Condition = card => card.gameObject.name.Equals("__CPC__Poppys Chaos"), HandSize = 0, Relative = false });
            //Run when the card is removed from the player
        }
        public override void OnReassignCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            PickManager.GiveConditionalPick(player, 0, false, condition: card => card.gameObject.name.Equals("__CPC__Poppys Chaos"));
        }

        protected override string GetTitle()
        {
            return "Chaotic Pick";
        }
        protected override string GetDescription()
        {
            return "Get an extra pick?";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Common");
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
