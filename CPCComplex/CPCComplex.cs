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
using InControl;
using PlayerActionsHelper;
using UnboundLib.GameModes;
using ModdingUtils.Utils;
using CPCComplex.Cards;
using RarityLib.Utils;
using CPCCore.Extensions;


namespace CPCComplex
{
        [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.rounds.willuwontu.ActionHelper", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.willuwontu.rounds.simulationChamber", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("io.olavim.rounds.rwf", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.Root.Null", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.Poppycars.PSA.Id", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("com.Poppycars.CPCCore.Id", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("pykess.rounds.plugins.pickncards", BepInDependency.DependencyFlags.HardDependency)]
        [BepInDependency("Systems.R00t.PickPhaseImprovements", BepInDependency.DependencyFlags.HardDependency)]
        // Declares our mod to Bepin
        [BepInPlugin(ModId, ModName, Version)]

        // The game our mod is associated with
        [BepInProcess("Rounds.exe")]
        public class ChaosPoppycarsCardsComplex : BaseUnityPlugin
        {
            private const string ModId = "com.Poppycars.CPCComplex.Id";
            private const string ModName = "ChaosPoppycarsCardsComplex";
            public const string Version = "1.0.4"; // What version are we on (major.minor.patch)?
            public const string ModInitials = "CPCComplex";
            internal static List<BaseUnityPlugin> plugins;
            public static ChaosPoppycarsCardsComplex Instance { get; private set; }

            public static AssetBundle Bundle = null;
            void Awake()
            {
                Instance = this;
                Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpccomplex", typeof(ChaosPoppycarsCardsComplex).Assembly);

                RarityLib.Utils.RarityUtils.AddRarity("Geese", 1f, new Color32(172, 172, 172, 255), new Color32(60, 60, 60, 255));
                if (!PlayerActionManager.RegisteredActions.ContainsKey("Dash"))
                {
                    PlayerActionManager.RegisterPlayerAction(new ActionInfo("Dash", new MouseBindingSource(Mouse.MiddleButton),
                        new DeviceBindingSource(InputControlType.RightBumper)));
                }
                if (!PlayerActionManager.RegisteredActions.ContainsKey("BlockMoveSwitch"))
                {
                    PlayerActionManager.RegisterPlayerAction(new ActionInfo("BlockMoveSwitch", new KeyBindingSource(Key.R),
                        new DeviceBindingSource(InputControlType.DPadLeft)));
                }
                if (!PlayerActionManager.RegisteredActions.ContainsKey("BlockPhaseAction"))
                {
                    PlayerActionManager.RegisterPlayerAction(new ActionInfo("BlockPhaseAction", new KeyBindingSource(Key.V),
                        new DeviceBindingSource(InputControlType.LeftStickButton)));
                }
                var harmony = new Harmony(ModId);

                harmony.PatchAll();

                var TESTIG = Bundle.LoadAsset<GameObject>("ModCards");

                var TESTIG2 = TESTIG.GetComponent<CardHolder>();

                TESTIG2.RegisterCards();

            }
            

            private void Start()
            {
                plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
                GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);
                ChaosPoppycarsCardsCore.RegisterCards(typeof(ChaosPoppycarsCardsComplex).Assembly, Bundle);

                ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => !cardinfo.rarity.Equals(RarityLib.Utils.RarityUtils.GetRarity("Geese")) || PlayerManager.instance.players.Any(p => player.teamID != p.teamID && p.data.currentCards.Contains(GeeseSwarm.Card)));
                ModdingUtils.Utils.Cards.instance.AddCardValidationFunction((player, cardinfo) => ExpansionCheck(player, cardinfo));

            }
            
            public bool ExpansionCheck(Player player, CardInfo card)
            {
                if (!card.name.Equals("__CPC__Expansion"))
                    return true;
                float map = 0;
                foreach (Player play in PlayerManager.instance.players)
                    map += player.data.stats.GetAdditionalData().mapSizeI;
                map += MapEmbiggener.MapEmbiggener.setSize;
                return map + 0.5f <= 7;
            }

            IEnumerator GameStart(IGameModeHandler gm)
            {
            // Runs at start of match
                RarityUtils.SetCardRarityModifier(KnifeGoose.Card, 0.5f);
                RarityUtils.SetCardRarityModifier(GoldGoose.Card, 0.01f);
                foreach (var player in PlayerManager.instance.players)
                {
                    //ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);

                    //ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.GeeseCategory);
                }
                yield break;

            }
            public static class CPCCardCategories
            {
                public static CardCategory GeeseCategory = CustomCardCategories.instance.CardCategory("GeeseCategory");
            }
    }
}