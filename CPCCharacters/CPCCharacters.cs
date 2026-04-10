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
using CPCCharacters.Shaders;
using UnboundLib.GameModes;
using BepInEx.Configuration;
using System;
using UnboundLib.Utils.UI;


namespace CPCCharacters
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.PSA.Id", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.CPCCore.Id", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCardsCharacters : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPCCharacters.Id";
        private const string ModName = "ChaosPoppycarsCardsCharacters";
        public const string Version = "1.0.4"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPCCharacters";
        internal static List<BaseUnityPlugin> plugins;
        public static ChaosPoppycarsCardsCharacters Instance { get; private set; }

        public static ConfigEntry<bool> ShroomAccess;

        public static AssetBundle Bundle = null;
        void Awake()
        {
            Instance = this;
            Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpccharacters", typeof(ChaosPoppycarsCardsCharacters).Assembly);

            var harmony = new Harmony(ModId);

            harmony.PatchAll();

            var TESTIG = Bundle.LoadAsset<GameObject>("ModCards");

            var TESTIG2 = TESTIG.GetComponent<CardHolder>();

            TESTIG2.RegisterCards();
            ShroomAccess = base.Config.Bind<bool>(ModId, "Shroom_Access", false, "Make Shrooms less throw up inducing");
        }
        private void Start()
        {
            plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            ChaosPoppycarsCardsCore.RegisterCards(typeof(ChaosPoppycarsCardsCharacters).Assembly, Bundle);

            Unbound.RegisterMenu(ModName, delegate () { }, new Action<GameObject>(this.NewGUI), null, false);

            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);

            ExtensionMethods.ExecuteAfterFrames(this, 60, delegate ()
            {
                Enumerable.ToList<Card>(CardManager.cards.Values).ForEach(delegate (Card card)
                {
                    this.AddMod(card);
                });
            });
        }
        private void NewGUI(GameObject menu)
        {
            MenuHandler.CreateText(ModName, menu, out _, 60, false, null, null, null, null);

            MenuHandler.CreateToggle(ShroomAccess.Value, "Make shrooms use the accessibility shader", menu, value => ShroomAccess.Value = value);

            MenuHandler.CreateText("", menu, out _);
        }
        private void AddMod(Card card)
        {
            string text = "__Rarity-" + card.cardInfo.rarity;
            CardCategory cardCategory = CustomCardCategories.instance.CardCategory(text);
            CardCategory[] categories = CollectionExtensions.AddToArray<CardCategory>(card.cardInfo.categories, cardCategory);
            card.cardInfo.categories = categories;
        }
        IEnumerator GameStart(IGameModeHandler gm)
        {
            // Runs at start of match
            foreach (var player in PlayerManager.instance.players)
            {
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.NeedsWhynackAdrenaline);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.NeedsWhynackArguing);
            }
            yield break;
        }
        public static class CPCCardCategories
        {
            public static CardCategory NeedsWhynackAdrenaline = CustomCardCategories.instance.CardCategory("NeedsWhynackAdrenaline");
            public static CardCategory NeedsWhynackArguing = CustomCardCategories.instance.CardCategory("NeedsWhynackArguing");
        }
    }
}