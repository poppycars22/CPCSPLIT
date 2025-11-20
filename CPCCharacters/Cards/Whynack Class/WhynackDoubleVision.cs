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
using CPCCore.Extensions;
using RarityLib.Utils;

namespace CPCCharacters.Cards
{
    class WhynackDoubleVision : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            block.healing = -120;
            block.cdAdd = 1.75f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChaosPoppycarsCardsCharacters.Instance.ExecuteAfterFrames(10, () =>
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, Whynack.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, Whynack.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackForward.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackForward.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackGoku.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackGoku.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackShamrock.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackShamrock.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackArguing.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackArguing.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackAdrenaline.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackAdrenaline.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackHarmony.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackHarmony.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackMeditating.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackMeditating.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackBlockMeditating.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackBlockMeditating.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackUppercut.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackUppercut.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynackVampire.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynackVampire.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WhynacksBlasting.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WhynacksBlasting.Card);
                block.forceToAddUp /= 2;
                block.forceToAdd /= 2;
            });
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Woah im so drunk I see so many Whynacks";
        }
        protected override string GetDescription()
        {
            return "1 whynack, 2 whynacks, 5 whynacks, 13?";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<GameObject>("C_Whynack");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Unique");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Heal on block",
                    amount = "-120",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Upwards block force",
                    amount = "halfed",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block force",
                    amount = "halfed",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+1.75s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeLib.CardThemeLib.instance.CreateOrGetType("Whynot Block Red");
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
