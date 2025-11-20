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
using ModdingUtils.Extensions;
using RarityLib.Utils;

namespace CPCComplex.Cards
{
    class ScareJackpot : CustomCard
    {
        public static CardCategory[] noLotteryCategories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation"), CustomCardCategories.instance.CardCategory("NoRandom") };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation"), RerollManager.instance.NoFlip };
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
            ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(10, () => {
                var scarce = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, ScarceCondition);

                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, scarce, false, "", 2f, 2f, true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, scarce, 3f);
            });
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Exotic Jackpot";
        }
        protected override string GetDescription()
        {
            return "gives you a random <#0A32FF>Exotic</color> card";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_ScarceJackpot");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Exotic",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
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
        private bool ScarceCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == RarityUtils.GetRarity("Exotic") && card.cardName != "Peptide" && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any(); ;
            
        }
    }
}
