using BepInEx;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using CPCCardInfostuffs;
using CPCCore.Extensions;
using CPCCore.MonoBehaviours;
using CPCCore.Patches;
using CPCCore.Utilities;
using CPCTabInfoSTATS;
using HarmonyLib;
using Jotunn.Utils;
using MapEmbiggener.Controllers;
using ModdingUtils;
using Photon.Realtime;
using RarityLib.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using UnboundLib.Utils;
using UnityEngine;
using WillsWackyManagers.Utils;
using static CPCCore.Utilities.CardUtils;




namespace CPCCore
{

    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.root.projectile.size.patch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Root.Null", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.attacklevelPatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rounds.willuwontu.ActionHelper", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.PSA.Id", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.root.player.time", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.tabinfo", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rsmind.rounds.fancycardbar", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("pykess.rounds.plugins.mapembiggener", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCardsCore : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPCCore.Id";
        private const string ModName = "ChaosPoppycarsCardsCore";
        public const string Version = "1.0.5"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPCCore";
        public static Harmony harmony;
        internal static List<BaseUnityPlugin> plugins;
        public static ChaosPoppycarsCardsCore Instance { get; private set; }

        public static AssetBundle Bundle = null;

        void Awake()
        {
            Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpccore", typeof(ChaosPoppycarsCardsCore).Assembly);

            
            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Evergreen", new CardThemeColor() { bgColor = new UnityEngine.Color(0.09f, 0.23f, 0.11f), targetColor = new UnityEngine.Color(0.28f, 0.80f, 0.32f) });

            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Whynot Block Red", new CardThemeColor() { bgColor = new UnityEngine.Color(0.3f, 0.0f, 0.0f), targetColor = new UnityEngine.Color(0.28f, 0.0f, 0.0f) });

            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Minecraft Soil", new CardThemeColor() { bgColor = new UnityEngine.Color(77f/255f, 40f/255f, 0.0f), targetColor = new UnityEngine.Color(0.1f, 0.85f, 0.1f) });

            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Critical Cerise", new CardThemeColor() { bgColor = new UnityEngine.Color(0.87f*0.25f, 0.19f*0.25f, 0.38f * 0.25f), targetColor = new UnityEngine.Color(0.87f, 0.19f, 0.38f) }); //rgb(222, 49, 97)

            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Ancient Pumpkin Orange", new CardThemeColor() { bgColor = new Color32(229 *1/2, 127*1/2, 0, 200), targetColor = new Color32(229 * 3 / 4, 127 * 3 / 4, 0, 200) });
            //CardThemeLib.CardThemeLib.instance.CreateOrGetType("Pumpkin Orange", new CardThemeColor() { bgColor = new UnityEngine.Color(0.93f, 0.67f, 0.3f), targetColor = new UnityEngine.Color(0.93f, 0.67f, 0.3f) }); //rgb(239, 172, 78)

            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Geese Gray", new CardThemeColor() { bgColor = new UnityEngine.Color(0.24f, 0.21f, 0.19f), targetColor = new UnityEngine.Color(0.24f, 0.21f, 0.19f) }); //rgb(63, 56, 49)


            // Use this to call any harmony patch files your mod may have
            harmony = new Harmony(ModId);

            harmony.PatchAll();

            //var TESTIG = Bundle.LoadAsset<GameObject>("ModCards");

            //var TESTIG2 = TESTIG.GetComponent<CardHolder>();

            //TESTIG2.RegisterCards();

            //Bundle.LoadAllAssets();

        }
        //REGISTER CURSES
        public static void RegisterCards(Assembly asemble, AssetBundle Bundle)
        {
            var assests = Bundle.LoadAllAssets<GameObject>();
            List<Type> types = asemble.GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(CustomCard))).ToList();
            foreach (var type in types)
            {
                try
                {
                    var cards = assests.Where(a => a is GameObject && a.GetComponent<CustomCard>() != null && a.GetComponent<CustomCard>().GetType() == type);
                    foreach (var card in cards)
                    {
                        if (!DateTools.WeekOf(new System.DateTime(System.DateTime.UtcNow.Year, 4, 1)) && card.name == "Tree")
                        {
                            continue;
                        }
                        try
                        {
                            type.GetField("Card", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
                            card.GetComponent<CustomCard>().BuildUnityCard(cardInfo => type.GetField("Card", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).SetValue(null, cardInfo));
                        }
                        catch
                        {
                            card.GetComponent<CustomCard>().BuildUnityCard(null);
                        }
                        if (card.GetComponent<Curse>() != null)
                        {
                            CurseManager.instance.RegisterCurse(card.GetComponent<CardInfo>());
                        }
                        if (card.GetComponent<CPCCardInfo>() is CPCCardInfo cpccardinfo)
                        {
                            cpccardinfo.Setup();
                        }
                        if (!card.GetComponent<CustomCard>().GetEnabled())
                        {
                            ModdingUtils.Utils.Cards.instance.AddHiddenCard(card.GetComponent<CardInfo>());
                        }
                    }
                }
                catch (Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
        }

        private void Start()
        {
            plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            Instance = this;
            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);



            if (plugins.Exists(plugin => plugin.Info.Metadata.GUID == "com.willuwontu.rounds.tabinfo"))
            {
                TabinfoInterface.Setup();
            }
            if (plugins.Exists(plugin => plugin.Info.Metadata.GUID == "pykess.rounds.plugins.mapembiggener"))
            {
                CameraZoomHandlerPatchPatch.Patch();
            }
            //ChaosPoppycarsCardsCore.ArtAssets = AssetUtils.LoadAssetBundleFromResources("cpccore", typeof(ChaosPoppycarsCardsCore).Assembly);
            RegisterCards(typeof(ChaosPoppycarsCardsCore).Assembly, Bundle);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, PointEnd);
            //GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, (gm) => ExtraPicks());
            //  GameModeManager.AddHook(GameModeHooks.HookBattleStart, LightSaberRangeReset);
            // make cards mutually exclusive
            this.ExecuteAfterFrames(10, () =>
            {

            });
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
        IEnumerator PointEnd(IGameModeHandler gm)
        {
            foreach (var player in PlayerManager.instance.players)
            {
                
                player.data.stats.GetAdditionalData().firstHit = true;
                player.data.stats.GetAdditionalData().firstDamage = true;
                //player.data.stats.GetAdditionalData().damageMult = player.data.stats.GetAdditionalData().damageMultMax;
            }
            yield break;
        }
        IEnumerator GameStart(IGameModeHandler gm)
        {
            // Runs at start of match
            foreach (var player in PlayerManager.instance.players)
            {
                //ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);

                //ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.GeeseCategory);
            }
            yield break;

        }
        public static class CPCCoreCardCategories
        {
            public static CardCategory PoppysChaosCategory = CustomCardCategories.instance.CardCategory("PoppysChaosCards");
        }
        /* private IEnumerator LightSaberRangeReset(IGameModeHandler gm)
         {
             yield return LightSaber.RangeResetTruth(gm);
         } */

        internal static AssetBundle ArtAssets;
    }
}
