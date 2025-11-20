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
using CPCCurses.Cards;
using ModdingUtils.Patches;
using System;
using UnboundLib.GameModes;


namespace CPCCurses
{
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.PSA.Id", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.CPCCore.Id", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCardsCurses : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPCCurses.Id";
        private const string ModName = "ChaosPoppycarsCardsCurses";
        public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPCCurses";
        internal static List<BaseUnityPlugin> plugins;
        public static ChaosPoppycarsCardsCurses Instance { get; private set; }

        public static AssetBundle Bundle = null;
        void Awake()
        {
            Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpccurses", typeof(ChaosPoppycarsCardsCurses).Assembly);

            var harmony = new Harmony(ModId);

            harmony.PatchAll();

            var TESTIG = Bundle.LoadAsset<GameObject>("ModCards");

            var TESTIG2 = TESTIG.GetComponent<CardHolder>();

            TESTIG2.RegisterCards();

        }
        private void Start()
        {
            plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            Instance = this;
            ChaosPoppycarsCardsCore.RegisterCards(typeof(ChaosPoppycarsCardsCurses).Assembly, Bundle);

            ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => player.data.currentCards.Where(C => C.GetComponent<LetterComponent>() != null).All(C => cardinfo.cardName.ToUpper().Contains(C.GetComponent<LetterComponent>().letter.ToUpper())) || cardinfo.categories.Contains(CPCCardCategories.IgnoreLetterCategory));


            ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => Validation(player, cardinfo));
            //ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => !(cardinfo.GetComponent<LetterComponent>() is LetterComponent letterComponent) || (ModdingUtils.Utils.Cards.active.Any(c => player.data.currentCards.Where(C => C.GetComponent<LetterComponent>() != null).All(C => c.cardName.ToUpper().Contains(C.GetComponent<LetterComponent>().letter.ToUpper())) && c.cardName.ToUpper().Contains(letterComponent.letter.ToUpper()))));
            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);

            ExtensionMethods.ExecuteAfterFrames(this, 60, delegate ()
            {
                Enumerable.ToList<Card>(CardManager.cards.Values).ForEach(delegate (Card card)
                {
                    this.AddMod(card);
                });
            });
        }
        bool Validation(Player player, CardInfo cardinfo)
        {
            if (!(cardinfo.GetComponent<LetterComponent>() is LetterComponent letterComponent))
            {
                return true;
            }

            List<string> validLetters = new List<string>() { letterComponent.letter };

            var heldLetterCards = player.data.currentCards.Where(c2 => c2.GetComponent<LetterComponent>() != null);

            foreach(CardInfo cardinfo2 in heldLetterCards)
            {
                validLetters.Add(cardinfo2.GetComponent<LetterComponent>().letter.ToUpper());
            }

            return ModdingUtils.Utils.Cards.active.Any(card => (!validLetters.Any(letter => (!card.cardName.ToUpper().Contains(letter)))));
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
                //ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);

                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.LetterCategory);
            }
            yield break;

        }
        public static class CPCCardCategories
        {
            public static CardCategory LetterCategory = CustomCardCategories.instance.CardCategory("LetterCategory");
            public static CardCategory IgnoreLetterCategory = CustomCardCategories.instance.CardCategory("IgnoreLetterCategory");
        }
    }
}