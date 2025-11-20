using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BepInEx;
using CPCCrafter.Cards;
using CPCCore.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using System.Reflection;
using UnboundLib.Networking;
using System.Collections.ObjectModel;
using UnboundLib.Utils;
using ClassesManagerReborn.Util;
using CPCCrafter.MonoBehaviours;


namespace CPCCrafter.Cards
{
    class RegenPotion : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            cardInfo.allowMultiple = false;
            block.cdMultiplier = 1.25f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.categories = new CardCategory[] { ChaosPoppycarsCardsCrafter.CPCCardCategories.PotionCategory };
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<RegenEffect>();
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            var mono = player.gameObject.GetOrAddComponent<RegenEffect>();
            UnityEngine.GameObject.Destroy(mono);
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Regeneration Potion";
        }
        protected override string GetDescription()
        {
            return "When you block you get some regen for 3 seconds";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("C_RegenrationPotion");
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
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeLib.CardThemeLib.instance.CreateOrGetType("Minecraft Soil");
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
