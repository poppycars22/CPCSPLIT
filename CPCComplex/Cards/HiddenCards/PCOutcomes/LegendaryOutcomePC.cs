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
using ModdingUtils.Extensions;


namespace CPCComplex.Cards
{
    class LegendaryOutcomePC : CustomCard
    {
        public override bool GetEnabled()
        {
            return false;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { ChaosPoppycarsCardsCore.CPCCoreCardCategories.PoppysChaosCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        private bool LegendCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == RarityUtils.GetRarity("Legendary") && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any();
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChaosPoppycarsCardsCore.Instance.ExecuteAfterFrames(10, () => {
                var legend = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, LegendCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, legend, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, legend, 3f);
            });
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "Legendary";
        }
        protected override string GetDescription()
        {
            return "You get a legendary";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
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
