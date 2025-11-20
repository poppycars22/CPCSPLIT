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
using ClassesManagerReborn.Util;
using System.Collections;
using UnboundLib.GameModes;
using UnboundLib.Extensions;
using CPCCore.Extensions;
using RarityLib.Utils;

namespace CPCCrafter.Cards
{
    class AutoCrafter : CustomCard
    {
        internal static CardInfo Card = null;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been setup.");
            
            statModifiers.health = 1.25f;
            cardInfo.allowMultiple = false;
            
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCrafter.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Crafter";
        }
        protected override string GetDescription()
        {
            return "Causes your tools to automatically upgrade, the crafters arent crafting crafters";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("C_AutoCrafter");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Exotic");
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
        internal static IEnumerator UpgradeSword(IGameModeHandler gm)
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                player.data.stats.GetAdditionalData().everyOther = !(player.data.stats.GetAdditionalData().everyOther);
                if (player.data.stats.GetAdditionalData().everyOther == false && player.data.currentCards.Contains(AutoCrafter.Card))
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, StoneSword.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, StoneSword.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, StoneSword.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronSword.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronSword.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronSword.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondSword.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondSword.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondSword.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteSword.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteSword.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteSword.Card);
                    }
                }
            }
            yield break;
        }
        internal static IEnumerator UpgradeHoe(IGameModeHandler gm)
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                if (player.data.stats.GetAdditionalData().everyOther == false && player.data.currentCards.Contains(AutoCrafter.Card))
                {

                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, StoneHoe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, StoneHoe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, StoneHoe.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronHoe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronHoe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronHoe.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondHoe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondHoe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondHoe.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteHoe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteHoe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteHoe.Card);
                    }
                }
            }
            yield break;
        }
        internal static IEnumerator UpgradeAxe(IGameModeHandler gm)
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                if (player.data.stats.GetAdditionalData().everyOther == false && player.data.currentCards.Contains(AutoCrafter.Card))
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, StoneAxe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, StoneAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, StoneAxe.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronAxe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronAxe.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondAxe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondAxe.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteAxe.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteAxe.Card);
                    }
                }
            }
            yield break;
        }
        internal static IEnumerator UpgradeArmor(IGameModeHandler gm)
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                if (player.data.stats.GetAdditionalData().everyOther == false && player.data.currentCards.Contains(AutoCrafter.Card))
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ChainArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, ChainArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, ChainArmor.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronArmor.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondArmor.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteArmor.Card);
                    }
                }
            }
            yield break;
        }
    }
}
