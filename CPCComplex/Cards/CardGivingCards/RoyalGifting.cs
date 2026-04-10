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
using UnboundLib.Utils;

namespace CPCComplex.Cards
{
    class RoyalGifting : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { RerollManager.instance.NoFlip, CurseManager.instance.curseSpawnerCategory, CustomCardCategories.instance.CardCategory("CardManipulation") };
           
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(10, () => {
                var rare = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, RareCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, rare, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, rare, 3f);
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
            });
            ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(11, () =>
            {
                var uncommon = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, UncommonCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, uncommon, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, uncommon, 3f);
                CurseManager.instance.CursePlayer(player, (curse) =>
                {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
            });
            ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(12, () =>
            {
                var common = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonCondition);
                var common2 = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, CommonCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, common, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, common, 3f);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, common2, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, common2, 3f);
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
            });
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        private bool ScarceCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == RarityUtils.GetRarity("Exotic") && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any();
        }
        private bool RareCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Rare && card.cardName != "Distill" && card.cardName != "Genie" && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any() ;
        }
        private bool UncommonCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Uncommon && card.cardName != "Copy" && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any();
        }
        private bool CommonCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Common && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Royal Gifting";
        }
        protected override string GetDescription()
        {
            return "gives you 2 commons, 1 uncommon, 1 rare, and 3 curses";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_RoyalGifitng");
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
                    positive = true,
                    stat = "Rare",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Uncommon",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Commons",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Curses",
                    amount = "+3",
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
        
    }
}
