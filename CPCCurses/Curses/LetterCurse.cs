using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BepInEx;
using CPCCurses.Cards;
using CPCCore.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using Photon.Pun;
using CPCCurses.MonoBehaviours;
using RarityLib.Utils;
using UnboundLib.Utils;
using ModdingUtils.Extensions;

namespace CPCCurses.Cards
{
    class LetterCurse : CustomCard {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory, ChaosPoppycarsCardsCurses.CPCCardCategories.IgnoreLetterCategory, CustomCardCategories.instance.CardCategory("CardManipulation") };
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            ChaosPoppycarsCardsCurses.Instance.ExecuteAfterFrames(5, () =>
            {
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.RemoveAll(category => category == ChaosPoppycarsCardsCurses.CPCCardCategories.LetterCategory);
                CurseManager.instance.CursePlayer(player, (curse) =>
                {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(ChaosPoppycarsCardsCurses.CPCCardCategories.LetterCategory);
                }, (cardInfo, player2) => LetterCondition(cardInfo, player2, null, null, null, null, null, null, null));
            });
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Letter Curse";
        }
        protected override string GetDescription()
        {
            return "You seem to have gotten lingusitically challenged";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCurses.Bundle.LoadAsset<GameObject>("C_LetterCurse");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {

            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return "Curse";
        }
        private bool LetterCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Contains(ChaosPoppycarsCardsCurses.CPCCardCategories.LetterCategory);
        }
    }
}
