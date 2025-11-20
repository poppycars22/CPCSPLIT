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
using CPCComplex.MonoBehaviours;
using CPCCore.Extensions;
using ModdingUtils.Extensions;
using WillsWackyManagers.Utils;

namespace CPCComplex.Cards
{
    class ElephantRave : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("cantEternity") };
            cardInfo.GetAdditionalData().canBeReassigned = false;
            statModifiers.health = 1.25f;
            statModifiers.movementSpeed = 1.25f;
            gun.damage = 1.25f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //var a = player.gameObject.GetOrAddComponent<LaserController>();
            ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(5, () =>
            {
                CardInfo rave = ModdingUtils.Utils.Cards.instance.GetCardWithObjectName("__CPC__Rave");
                foreach (Player player2 in ModdingUtils.Utils.PlayerStatus.GetOtherPlayers(player))
                {
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player2, rave, false, "", 2f, 2f, true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player2, rave, 3f);
                }
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, rave, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, rave, 3f);
            });
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
            return "Elephant Rave";
        }
        protected override string GetDescription()
        {
            return "The trials of the young elephant are set to resume after a long hibernation, a new challenge begins";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_ElephantRave");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Legendary");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
               new CardInfoStat()
               {
                    positive = true,
                    stat = "Health",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               },
               new CardInfoStat()
               {
                    positive = true,
                    stat = "Damage",
                    amount = "+25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
               },
               new CardInfoStat()
               {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+25%",
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
