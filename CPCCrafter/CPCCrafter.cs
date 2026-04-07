using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HarmonyLib;
using BepInEx;
using UnboundLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using Jotunn.Utils;
using System.Linq;
using System.Reflection;
using UnboundLib.Utils;
using CPCCore;
using CPCCrafter.Cards;
using UnboundLib.GameModes;
using BepInEx.Configuration;
using System;
using UnboundLib.Utils.UI;
using ModdingUtils.Utils;


namespace CPCCrafter
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.root.projectile.size.patch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.PSA.Id", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.CPCCore.Id", BepInDependency.DependencyFlags.HardDependency)]
        // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

        // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCardsCrafter : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPCCrafter.Id";
        private const string ModName = "ChaosPoppycarsCardsCrafter";
        public const string Version = "1.0.2"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPCCrafter";
        internal static List<BaseUnityPlugin> plugins;
        public static ChaosPoppycarsCardsCrafter Instance { get; private set; }

        public static ConfigEntry<bool> MC_Particles;

        public static AssetBundle Bundle = null;
        void Awake()
        {
            Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpccrafter", typeof(ChaosPoppycarsCardsCrafter).Assembly);
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
            var TESTIG = Bundle.LoadAsset<GameObject>("ModCards");
            var TESTIG2 = TESTIG.GetComponent<CardHolder>();
            TESTIG2.RegisterCards();
            MC_Particles = base.Config.Bind<bool>(ModId, "Minecraft_Particles", true, "Enable Minecraft Particles");
        }
            

        private void Start()
        {
            plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            Instance = this;
            ChaosPoppycarsCardsCore.RegisterCards(typeof(ChaosPoppycarsCardsCrafter).Assembly, Bundle);

            ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => toolFunc(player, cardinfo));

            Unbound.RegisterMenu(ModName, delegate () { }, new Action<GameObject>(this.NewGUI), null, false);

            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);

            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeAction);

            ExtensionMethods.ExecuteAfterFrames(this, 60, delegate ()
            {
                Enumerable.ToList<Card>(CardManager.cards.Values).ForEach(delegate (Card card)
                {
                    this.AddMod(card);
                });
            });
        }

        private bool toolFunc(Player player, CardInfo cardInfo)
        {
            Dictionary<CardInfo, CardInfo> tree = new Dictionary<CardInfo, CardInfo>{ 
                { LetherArmor.Card, NetheriteArmor.Card },
                { WoodenAxe.Card, NetheriteAxe.Card },
                { WoodenHoe.Card, NetheriteHoe.Card },
                { WoodenSword.Card, NetheriteSword.Card },
            };
            if (tree.Keys.Contains(cardInfo)){
                foreach (CardInfo start in tree.Keys)
                {
                if (player.data.currentCards.Contains(start) && !player.data.currentCards.Contains(tree[start]))
                    return false;
                }
            }
            return true;
        }
        private void AddMod(Card card)
        {
            string text = "__Rarity-" + card.cardInfo.rarity;
            CardCategory cardCategory = CustomCardCategories.instance.CardCategory(text);
            CardCategory[] categories = CollectionExtensions.AddToArray<CardCategory>(card.cardInfo.categories, cardCategory);
            card.cardInfo.categories = categories;
        }
        private void NewGUI(GameObject menu)
        {
            MenuHandler.CreateText(ModName, menu, out _, 60, false, null, null, null, null);

            MenuHandler.CreateToggle(MC_Particles.Value, "Enable Minecraft Particles (only effects potions right now)", menu, value => MC_Particles.Value = value);

            MenuHandler.CreateText("", menu, out _);
        }
        private IEnumerator UpgradeAction(IGameModeHandler gm)
        {
            yield return AutoCrafter.UpgradeSword(gm);
            yield return AutoCrafter.UpgradeHoe(gm);
            yield return AutoCrafter.UpgradeAxe(gm);
            yield return AutoCrafter.UpgradeArmor(gm);
        }
        IEnumerator GameStart(IGameModeHandler gm)
        {
            // Runs at start of match
            foreach (var player in PlayerManager.instance.players)
            {
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);
            }
            yield break;
        }
        public static class CPCCardCategories
        {
            public static CardCategory PotionCategory = CustomCardCategories.instance.CardCategory("UltimatePotion");
        }
    }
}