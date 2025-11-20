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
using RarityLib.Utils;
using CPCCore.MonoBehaviours;
using CPCCore.Extensions;
using Photon.Pun;
using RWF;
using System.Collections;
using UnboundLib.GameModes;
using CPCComplex.MonoBehaviours;

namespace CPCComplex.Cards
{
    class BlockPusher : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            gun.attackSpeed = 1.5f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<CooldownBlock>();
            characterStats.GetAdditionalData().blockMoveStrength += 1;
            characterStats.GetAdditionalData().blockMover = true;
            characterStats.GetAdditionalData().blockPush = true;
            gun.reflects = 0;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<CooldownBlock>();
            UnityEngine.GameObject.Destroy(mono);
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Block Mover";
        }
        protected override string GetDescription()
        {
            return "Your bullets now move objects, press R (left dpad on controller) to switch the direction they move [Each move has a 3 second cooldown]";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_BlockPusher");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Divine");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
               new CardInfoStat()
               {
                    positive = true,
                    stat = "Block Moving Strength",
                    amount = "+1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               },
               new CardInfoStat()
               {
                    positive = false,
                    stat = "Bounces",
                    amount = "Reset",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               },
               new CardInfoStat()
               {
                    positive = false,
                    stat = "Attack Speed",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.TechWhite;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
