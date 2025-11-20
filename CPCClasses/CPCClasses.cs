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


namespace CPCClasses
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
        public class ChaosPoppycarsCardsClasses : BaseUnityPlugin
        {
            private const string ModId = "com.Poppycars.CPCClasses.Id";
            private const string ModName = "ChaosPoppycarsCardsClasses";
            public const string Version = "1.0.0"; // What version are we on (major.minor.patch)?
            public const string ModInitials = "CPCClasses";
            internal static List<BaseUnityPlugin> plugins;
            public static ChaosPoppycarsCardsClasses Instance { get; private set; }

            public static AssetBundle Bundle = null;
            void Awake()
            {
                Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpcclasses", typeof(ChaosPoppycarsCardsClasses).Assembly);
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
                ChaosPoppycarsCardsCore.RegisterCards(typeof(ChaosPoppycarsCardsClasses).Assembly, Bundle);
                

                ExtensionMethods.ExecuteAfterFrames(this, 60, delegate ()
                {
                    Enumerable.ToList<Card>(CardManager.cards.Values).ForEach(delegate (Card card)
                    {
                        this.AddMod(card);
                    });
                });
            }
            private void AddMod(Card card)
            {
                string text = "__Rarity-" + card.cardInfo.rarity;
                CardCategory cardCategory = CustomCardCategories.instance.CardCategory(text);
                CardCategory[] categories = CollectionExtensions.AddToArray<CardCategory>(card.cardInfo.categories, cardCategory);
                card.cardInfo.categories = categories;
            }
        }
}