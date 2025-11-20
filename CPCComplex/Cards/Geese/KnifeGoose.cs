using UnboundLib.Cards;
using UnityEngine;
using CPCCore.Utilities;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using RarityLib.Utils;

namespace CPCComplex.Cards
{
    class KnifeGoose : CustomCard
    {


        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = true;
            cardInfo.categories = new CardCategory[] {CustomCardCategories.CanDrawMultipleCategory, ChaosPoppycarsCardsComplex.CPCCardCategories.GeeseCategory };

            gun.ammo = -1;
            gun.reloadTime = 1.2f;
            
            gun.damage = 1.15f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {


            RarityUtils.GetRarityData(RarityUtils.GetRarity("Geese")).calculatedRarity -= 0.2f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            RarityUtils.GetRarityData(RarityUtils.GetRarity("Geese")).calculatedRarity += 0.2f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Knife Goose";
        }
        protected override string GetDescription()
        {
            return "<i><size=200><b><color=#ff2020>Honk</b></color></size></i>";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_KnifeGoose");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Geese");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
               
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Damage",
                    amount = "+15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "-1",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Reload Time",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeLib.CardThemeLib.instance.CreateOrGetType("Geese Gray");
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
