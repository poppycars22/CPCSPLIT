using CPCCore;
using CPCCore.Extensions;
using CPCCore.Utilities;
using LuckLib;
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

namespace CPCComplex.Cards
{
    class TwentyOutcomePC : CustomCard
    {
        public override bool GetEnabled()
        {
            return false;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.gameObject.GetOrAddComponent<Luck>().LuckAdd = -50;
            cardInfo.categories = new CardCategory[] { ChaosPoppycarsCardsCore.CPCCoreCardCategories.PoppysChaosCategory };
            
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            DrawNCards.DrawNCards.SetPickerDraws(player.playerID, 20);
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            DrawNCards.DrawNCards.SetPickerDraws(player.playerID, DrawNCards.DrawNCards.numDraws);
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "Twenty";
        }
        protected override string GetDescription()
        {
            return "Pick from TWENTY cards";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Legendary");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
               {
                    positive = false,
                    stat = "Luck",
                    amount = "-50",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               }
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
