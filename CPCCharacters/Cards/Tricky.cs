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
using ClassesManagerReborn.Util;
using RarityLib.Utils;

namespace CPCCharacters.Cards
{
    public class Tricky : CustomCard
    {
        public static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been setup.");

            statModifiers.movementSpeed = 16f;
            statModifiers.sizeMultiplier = 0.75f;
            cardInfo.allowMultiple = false;
            
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = "Speedster";
        }
        protected override string GetTitle()
        {
            return "Tricky";
        }
        protected override string GetDescription()
        {
            return "The speed god blessith, not all can contain his power";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<GameObject>("C_Tricky");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Mythical");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+1500%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Size",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeLib.CardThemeLib.instance.CreateOrGetType("Evergreen");
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
