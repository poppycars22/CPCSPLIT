using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using BepInEx;
using System.Reflection;


namespace CPCClasses.Cards
{
    class SpeedClass : ClassHandler
    {
        internal static string name = "Speedrunner";
        internal static List<BaseUnityPlugin> plugins;

        public override IEnumerator Init()
        {
            plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(SpeedDemon.Card && MomentumShots.Card && SpeedyHands.Card && TriggerFinger.Card && Swifter.Card && Stretches.Card && LegDay.Card && SpeedstersGun.Card && AirHops.Card && NitroBoost.Card && BoostedBlock.Card && AcceleratedRejuvenation.Card)) yield return null;
            ClassesRegistry.Register(SpeedDemon.Card, CardType.Entry);
            ClassesRegistry.Register(MomentumShots.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(SpeedyHands.Card, CardType.Card, SpeedDemon.Card);
            if (plugins.Exists(plugin => plugin.Info.Metadata.GUID == "com.Poppycars.CPCCharacters.Id"))
            {
                ClassesRegistry.Register(ModdingUtils.Utils.Cards.instance.GetCardWithObjectName("__CPC__Tricky"), CardType.Card, SpeedDemon.Card);
            }
            ClassesRegistry.Register(TriggerFinger.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(Swifter.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(Stretches.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(LegDay.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(SpeedstersGun.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(AirHops.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(NitroBoost.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(BoostedBlock.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(TimeWarp.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(TimeBomb.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(AcceleratedRejuvenation.Card, CardType.Card, SpeedDemon.Card);
        }
        public override IEnumerator PostInit()
        {
            ClassesRegistry.Get(SpeedyHands.Card).Blacklist(TriggerFinger.Card);
            ClassesRegistry.Get(TriggerFinger.Card).Blacklist(SpeedyHands.Card);
            yield break;
        }
    }
}