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

namespace CPCCharacters.Cards
{
    class Spoon : CustomCard
    {
        public static CardInfo Card;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (gun.reflects > 0)
                gun.reflects *= 2;
            else
                gun.reflects += 2;
            gunAmmo.maxAmmo *= 2;
            gun.attackSpeed /= 2;
            data.maxHealth /= 2;
            gun.damage /= 2;
            health.regeneration /= 2;
            characterStats.movementSpeed *= 2;
            if (!player.data.currentCards.Contains(Spoon.Card))
            {
                GameObject hat = Instantiate(ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<GameObject>("TopHatObj"));
                hat.transform.parent = player.gameObject.transform;
                hat.transform.localPosition = new Vector3(-0.1f, 0.85f, 0);
                hat.transform.localScale = new Vector3(0.25f, 0.25f, 0);

                GameObject hat2 = Instantiate(ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<GameObject>("TopHatObj"));
                hat2.transform.parent = player.gameObject.transform;
                hat2.transform.localPosition = new Vector3(-0.22f, 1.85f, 0);
                hat2.transform.localScale = new Vector3(0.25f, 0.25f, 0);
            }
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            foreach(TopHat a in player.GetComponentsInChildren<TopHat>())
            {
                Destroy(a.gameObject);
            }
            CPCDebug.Log($"[{ChaosPoppycarsCardsCharacters.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Mira";
        }
        protected override string GetDescription()
        {
            return "awawa";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<GameObject>("C_Spoon");
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
                    positive = true,
                    stat = "Top Hats",
                    amount = "2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bounces",
                    amount = "Doubled",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Speed",
                    amount = "Doubled",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "Doubled",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Ammo",
                    amount = "Doubled",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Health",
                    amount = "Halved",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "Halved",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Regeneration",
                    amount = "Halved",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
