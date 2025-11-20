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
using System.Reflection;
using UnboundLib.Networking;
using System.Collections.ObjectModel;
using UnboundLib.Utils;
using CPCClassic.MonoBehaviours;
using RarityLib.Utils;

namespace CPCClassic.Cards
{
    class JumpShrink : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            
            CPCDebug.Log($"[{ChaosPoppycarsCardsClassic.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<JumpShrinkEffect>();
            CPCDebug.Log($"[{ChaosPoppycarsCardsClassic.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsClassic.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            var mono = player.gameObject.GetOrAddComponent<JumpShrinkEffect>();
            UnityEngine.GameObject.Destroy(mono);
            //Run when the card is removed from the player
        }
       
        protected override string GetTitle()
        {
            return "Shrinking Jumps";
        }
        protected override string GetDescription()
        {
            return "When you jump you get smaller";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsClassic.Bundle.LoadAsset<GameObject>("C_ShrinkingJumps");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Exotic");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    
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
