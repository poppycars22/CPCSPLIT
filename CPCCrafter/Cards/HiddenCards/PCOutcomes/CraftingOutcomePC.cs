using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using Photon.Pun;
using BepInEx;
using UnboundLib.Utils;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using Nullmanager;
using System.Collections;
using UnboundLib.GameModes;
using System.Numerics;
using UnityEditor;
using Photon.Realtime;
using CPCCore.Utilities;
using CPCCore;


namespace CPCCrafter.Cards
{
    class CraftingOutcomePC : CustomCard
    {
        public override bool GetEnabled()
        {
            return false;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { ChaosPoppycarsCardsCore.CPCCoreCardCategories.PoppysChaosCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChaosPoppycarsCardsCrafter.Instance.ExecuteAfterFrames(10, () =>
            {
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, CraftingTable.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, CraftingTable.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WoodenAxe.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WoodenAxe.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WoodenSword.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WoodenSword.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WoodenHoe.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WoodenHoe.Card);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, LetherArmor.Card, addToCardBar: true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, LetherArmor.Card);
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
            return "Crafting";
        }
        protected override string GetDescription()
        {
            return "You must mine and craft";
        }
        protected override GameObject GetCardArt()
        {
            return null;
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
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
