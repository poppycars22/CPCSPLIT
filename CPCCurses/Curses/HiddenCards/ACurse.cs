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
using CPCCurses.MonoBehaviours;
using RarityLib.Utils;
using UnityEngine.UI;

namespace CPCCurses.Cards
{
    class ACurse : CustomCard {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory, ChaosPoppycarsCardsCurses.CPCCardCategories.LetterCategory, ChaosPoppycarsCardsCurses.CPCCardCategories.IgnoreLetterCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override bool GetEnabled()
        {
            return false;
        }
        protected override string GetTitle()
        {
            string letter = this.GetComponent<LetterComponent>().letter;
            return letter.ToUpper() + letter.ToLower();
        }
        protected override string GetDescription()
        {
            return GetTitle();
        }
        protected override GameObject GetCardArt()
        {
            /*GameObject a = Instantiate(ChaosPoppycarsCardsCurses.Bundle.LoadAsset<GameObject>("C_Letter"));
            a.GetComponentInChildren<Text>().text = GetTitle();
            DontDestroyOnLoad(a);
            return a;*/
            return ChaosPoppycarsCardsCurses.Bundle.LoadAsset<GameObject>("C_Letter");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            string rarity = this.GetComponent<LetterComponent>().rarity;
            return RarityUtils.GetRarity(rarity);
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
    }
}
