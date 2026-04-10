using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using CPCCore.Utilities;
using ModdingUtils.Extensions;
using Nullmanager;
using Photon.Realtime;
using RarityLib.Utils;
using System.Collections;
using System.Linq;
using System.Numerics;
using ToggleCardsCategories;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnityEditor;
using UnityEngine;
using WillsWackyManagers.Utils;

namespace CPCCore.Cards
{
    class Tree : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            UnityEngine.Diagnostics.Utils.ForceCrash(UnityEngine.Diagnostics.ForcedCrashCategory.Abort);
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "The Sacred Rubber Sappling";
        }
        protected override string GetDescription()
        {
            return "The progenator of the sacred tree. Plant at your own risk";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCore.Bundle.LoadAsset<GameObject>("C_Tree");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Trinket");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    stat = "",
                    amount = "+Tree",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
