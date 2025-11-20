using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;

namespace CPCClasses.Cards
{
    class AncientClass : ClassHandler
    {
        internal static string name = "Ancient";

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(CursedTablet.Card && UndeadTome.Card && StoneSkin.Card && ReducingDamage.Card && InvasiveThorns.Card && FleshFragments.Card && ThornFiend.Card)) yield return null;
            ClassesRegistry.Register(CursedTablet.Card, CardType.Entry);
            ClassesRegistry.Register(UndeadTome.Card, CardType.Card, CursedTablet.Card);
            ClassesRegistry.Register(StoneSkin.Card, CardType.Card, CursedTablet.Card);
            ClassesRegistry.Register(ReducingDamage.Card, CardType.Card, CursedTablet.Card);
            ClassesRegistry.Register(InvasiveThorns.Card, CardType.Gate, CursedTablet.Card, 2);
            ClassesRegistry.Register(FleshFragments.Card, CardType.Card, CursedTablet.Card);
            ClassesRegistry.Register(ThornFiend.Card, CardType.Card, InvasiveThorns.Card);
        }
        public override IEnumerator PostInit()
        {
            /*ClassesRegistry.Get(SpeedyHands.Card).Blacklist(TriggerFinger.Card);
            ClassesRegistry.Get(TriggerFinger.Card).Blacklist(SpeedyHands.Card);*/
            yield break;
        }
    }
}