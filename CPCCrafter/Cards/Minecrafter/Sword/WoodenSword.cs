using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CPCCrafter.Cards;
using CPCCore.Utilities;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ClassesManagerReborn.Util;
using UnboundLib.GameModes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using TMPro;
using UnityEngine.UI;
using UnboundLib.Utils;
using UnboundLib.Networking;
using Photon.Pun;
using ClassesManagerReborn;
using UnboundLib.Extensions;
using CPCCore.Extensions;

namespace CPCCrafter.Cards
{
    
    class WoodenSword : CustomCard
    {
        internal static CardInfo Card = null;
        

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;
            gun.damage = 1.1f;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");

           
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Wooden Sword";
        }
        protected override string GetDescription()
        {
            return "Gives a little damage";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("C_WoodenSword");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+10%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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
