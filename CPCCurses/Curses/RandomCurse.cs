using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BepInEx;
using CPCCurses.Cards;
using CPCCore.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using PSA.Extensions;
using Photon.Pun;

namespace CPCCurses.Cards
{
    class RandomCurse : CustomCard
    {
        private System.Random randomNumberGenerator;
        private PhotonView photonView;
        internal static CardInfo Card = null;
        public void Start()
        {
            photonView = GetComponent<PhotonView>();
            // If this is the master client, initialize the random number generator seed
            if (PhotonNetwork.IsMasterClient)
            {
                randomNumberGenerator = new System.Random((int)System.DateTime.Now.Ticks);
                photonView.RPC("InitializeRandomNumberGenerator", RpcTarget.Others, (int)System.DateTime.Now.Ticks);
            }
        }
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been setup.");
           

        }
        [PunRPC]
        public void InitializeRandomNumberGenerator(int seed)
        {
            randomNumberGenerator = new System.Random(seed);

        }
        void GenerateAndPerformEffect(Gun gun, Player player, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            int randomNumber = randomNumberGenerator.Next(1, 11);


            switch (randomNumber)
            {
                case 0:

                    //UnityEngine.Debug.Log("case 0");
                    //example case
                    break;
                case 1:

                    UnityEngine.Debug.Log("Random Curse case 1");
                    gun.damage *= 0.25f;
                    break;
                case 2:
                    UnityEngine.Debug.Log("Random Curse case 2");
                    gunAmmo.reloadTimeMultiplier *= 1.75f;
                    break;
                // ...
                case 3:
                    UnityEngine.Debug.Log("Random Curse case 3");
                    data.maxHealth *= 0.25f;
                    break;
                case 4:
                    UnityEngine.Debug.Log("Random Curse case 4");
                    gun.numberOfProjectiles = 1;

                    break;
                case 5:
                    gun.reflects = 0;
                    UnityEngine.Debug.Log("Random Curse case 5");
                    break;
                case 6:
                    characterStats.GetAdditionalData().damageReduction *= 0.5f;
                    UnityEngine.Debug.Log("Random Curse case 6");
                    break;
                case 7:
                    block.additionalBlocks -= 2;
                    block.cdMultiplier *= 1.75f;
                    UnityEngine.Debug.Log("Random Curse case 7");
                    break;
                case 8:
                    gunAmmo.maxAmmo = 1;
                    UnityEngine.Debug.Log("Random Curse case 8");
                    break;
                case 9:

                    gun.cos += 1f;
                    UnityEngine.Debug.Log("Random Curse case 9");
                    //example case
                    break;
                case 10:

                    characterStats.health *= 1.5f;
                    UnityEngine.Debug.Log("Random Curse case 10");
                    //example case
                    break;
                default:
                    UnityEngine.Debug.LogError("Unexpected random number value: " + randomNumber);
                    break;
            }
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            GenerateAndPerformEffect(gun, player, gunAmmo, data, health, gravity, block, characterStats);
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCardsCurses.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Chaotic Curse";
        }
        protected override string GetDescription()
        {
            return "Your punishment shall be decided by fate";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCardsCurses.Bundle.LoadAsset<GameObject>("C_RandomCurse");
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
                    positive = false,
                    stat = "???",
                    amount = "-",
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
            return "Curse";
        }
    }
}
