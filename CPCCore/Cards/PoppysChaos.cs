using System.Linq;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using Nullmanager;
using System.Collections;
using UnboundLib.GameModes;
using System.Numerics;
using UnityEditor;
using Photon.Realtime;
using CPCCore.Utilities;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.Extensions;
using MapEmbiggener.Controllers;
using MapEmbiggener.Controllers.Default;

namespace CPCCore.Cards
{
    class PoppysChaos : CustomCard
    {

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation"), RerollManager.instance.NoFlip };

            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            

            ChaosPoppycarsCardsCore.Instance.ExecuteAfterFrames(10, () => {
                //var scarce = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, ScarceCondition);
                var scarce = ModdingUtils.Utils.Cards.instance.DrawRandomCardWithCondition(ModdingUtils.Utils.Cards.instance.HiddenCards.ToArray(), player, gun, gunAmmo, data, health, gravity, block, characterStats, ScarceCondition);

                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, scarce, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, scarce, 3f);
            });
            CPCDebug.Log($"[{ChaosPoppycarsCardsCore.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        private bool ScarceCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(new CardCategory[] { ChaosPoppycarsCardsCore.CPCCoreCardCategories.PoppysChaosCategory } ).Any();
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "Poppys Chaos";
        }
        protected override string GetDescription()
        {
            return "The C H A O S, its now true";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCore.Bundle.LoadAsset<GameObject>("C_PoppysChaos");
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
                    stat = "",
                    amount = "<#FFFF00>+???</color>",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {

                    stat = "",
                    amount = "<#FFFF00>-???</color>",
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
