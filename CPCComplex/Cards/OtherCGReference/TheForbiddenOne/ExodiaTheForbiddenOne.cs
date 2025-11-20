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
using Photon.Pun;
using UnboundLib.GameModes;
using System.Collections;
using Sonigon;
using RWF;
using RarityLib.Utils;
using CPCComplex.MonoBehaviours;
using CPCCore;


namespace CPCComplex.Cards
{
    class ExodiaTheForbiddenOne : CustomCard
    {
        internal static CardInfo Card = null;
        protected override GameObject GetCardBase()
        {
            return this.gameObject.GetComponent<CardInfo>().cardBase;
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (player.data.currentCards.Contains(LeftArmOfTheForbiddenOne.Card) && player.data.currentCards.Contains(RightArmOfTheForbiddenOne.Card) && player.data.currentCards.Contains(LeftLegOfTheForbiddenOne.Card) && player.data.currentCards.Contains(RightLegOfTheForbiddenOne.Card))
            {
                TriggerWin exodiaVictory = new TriggerWin();
                exodiaVictory.WinnerId(player.playerID);
                exodiaVictory.WinText("Exodia Victory!");
                GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, exodiaVictory.Win);
                GameModeManager.TriggerHook("Win");
                ChaosPoppycarsCardsComplex.Instance.ExecuteAfterSeconds(2, () =>
                {
                    GameModeManager.RemoveHook(GameModeHooks.HookPlayerPickEnd, exodiaVictory.Win);
                });
            }
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Exodia the Forbidden One";
        }
        protected override string GetDescription()
        {
            return "If you have \"Right Leg of the Forbidden One\", \"Left Leg of the Forbidden One\", \"Right Arm of the Forbidden One\" and \"Left Arm of the Forbidden One\" in addition to this card, you win the Duel.";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_ExodiaTheForbiddenOne");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Unique");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {

            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
        public override string GetModName()
        {
            return "CPC";
        }

    }
}
