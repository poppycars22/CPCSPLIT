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
using CPCCharacters.MonoBehaviours;
using RarityLib.Utils;

namespace CPCCharacters.Cards
{
    class Anarkey : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.gravity = -1f;
            statModifiers.sizeMultiplier = -1f;
            statModifiers.jump = -1f;
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<FlipGravEffect>();
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<FlipGravEffect>();
            UnityEngine.GameObject.Destroy(mono);
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Anarkey";
        }
        protected override string GetDescription()
        {
            return "Total anarkey";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<GameObject>("C_Anarkey");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Legendary");
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
