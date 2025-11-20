using UnboundLib.Cards;
using UnityEngine;
using CPCCore.Utilities;
using RarityLib.Utils;

namespace CPCComplex.Cards
{
    class ReverseThruster : CustomCard
    {
        
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = true;
            
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
           

        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
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
            return "Reverse Thrusters";
        }
        protected override string GetDescription()
        {
            return "Team Rocket's blasting off again!";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_ReverseThrusters");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Legendary");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                
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
