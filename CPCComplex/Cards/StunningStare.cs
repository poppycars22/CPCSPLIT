using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using CPCCore.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.MonoBehaviours;
using CPCCore.Extensions;
namespace CPCComplex.Cards
{
    class StunningStare : CustomCard
    {
        private Player player;
        private CharacterStatModifiers characterStats;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            statModifiers.health = 0.85f;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().StunningStares += 1;
            InConeEffect newEffect = player.gameObject.AddComponent<InConeEffect>();

            newEffect.SetCenterRay(new Vector2(1f, 0f));
            newEffect.SetOtherColor(Color.magenta);
            newEffect.SetNeedsLineOfSight(true);
            newEffect.SetApplyToSelf(false);
            newEffect.SetApplyToOthers(true);
            newEffect.SetCheckEnemiesOnly(true);
            newEffect.SetOtherEffectFunc(this.stunningstare);
            newEffect.SetPeriod(6f); //10f
            newEffect.SetRange(10f * ((characterStats.GetAdditionalData().StunningStares + 1) /2));
            //newEffect.SetAngle(90f);
            this.player = player;
            this.characterStats = characterStats;

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
            return "Stunning Stare";
        }
        protected override string GetDescription()
        {
            return "People in your vision within a certain range get stunned (getting this card more then once increases the range)";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("C_StunningStare");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
               new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "-15%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }
        public List<MonoBehaviour> stunningstare(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            List<MonoBehaviour> effects = new List<MonoBehaviour>();

            ReversibleEffect effect = player.gameObject.AddComponent<ReversibleEffect>();
            
            var addedObj = Instantiate((ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("StunningStareEffect")), player.transform.position, player.transform.rotation, player.transform);

            effect.stats.objectsAddedToPlayer.Add(addedObj);
            effects.Add(effect);


            return effects;

        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
