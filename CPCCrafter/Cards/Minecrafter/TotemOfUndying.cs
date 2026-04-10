using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using CPCCrafter.MonoBehaviours;
using ClassesManagerReborn.Util;
using RarityLib.Utils;
using CPCCore.Extensions;


namespace CPCCrafter.Cards
{
    public class TotemOfUndying : CustomCard
    {
        internal static CardInfo Card = null;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = true;
            
            
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            player.gameObject.GetOrAddComponent<TotemEffect>();
            characterStats.GetAdditionalData().totems += 1;
            characterStats.GetAdditionalData().remainingTotems = characterStats.GetAdditionalData().totems;


        }
        public override void OnRemoveCard()
        {
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Totem Of Undying";
        }
        protected override string GetDescription()
        {
            return "Survive one fatal blow and gain regen afterwards";
        }

        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("C_TotemOfUndying");
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Exotic");
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                positive = true,
                stat = "Totems",
                amount = "+1",
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