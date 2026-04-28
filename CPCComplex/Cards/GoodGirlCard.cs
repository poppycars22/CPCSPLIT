using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using CPCComplex.MonoBehaviours;
using CPCCore.Extensions;
using CPCCore.Utilities;
using HarmonyLib;
using LuckLib;
using Photon.Realtime;
using RarityLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.Utils;



namespace CPCComplex.Cards
{
    class GoodGirlCard : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.gameObject.GetOrAddComponent<Luck>().LuckAdd = 100;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            DrawNCards.DrawNCards.SetPickerDraws(player.playerID, DrawNCards.DrawNCards.GetPickerDraws(player.playerID) + 2);
            /*if (CardChoice.instance.pickerType != 0 && player.playerID == CardChoice.instance.pickrID)
            {
                CardChoice.instance.picks++;
            }*/
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
                DrawNCards.DrawNCards.SetPickerDraws(player.playerID, DrawNCards.DrawNCards.GetPickerDraws(player.playerID) - 2);
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Good Girl";
        }
        protected override string GetDescription()
        {
            return "";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_GoodGirl");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Rare");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
               new CardInfoStat()
               {
                    positive = true,
                    stat = "Hand Size",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               },
               new CardInfoStat()
               {
                    positive = true,
                    stat = "Luck",
                    amount = "+100",
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
