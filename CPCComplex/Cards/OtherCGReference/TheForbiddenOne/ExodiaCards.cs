using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using BepInEx;
using System.Reflection;


namespace CPCComplex.Cards
{
    class ExodiaCards : ClassHandler
    {
        internal static string name = "Exodia";
        internal static List<BaseUnityPlugin> plugins;

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(ExodiaTheForbiddenOne.Card)) yield return null;
            ClassesRegistry.Register(ExodiaTheForbiddenOne.Card, CardType.Entry | CardType.NonClassCard);
            ClassesRegistry.Register(LeftArmOfTheForbiddenOne.Card,  CardType.Card | CardType.NonClassCard, ExodiaTheForbiddenOne.Card);
            ClassesRegistry.Register(LeftLegOfTheForbiddenOne.Card, CardType.Card | CardType.NonClassCard, ExodiaTheForbiddenOne.Card);
            ClassesRegistry.Register(RightArmOfTheForbiddenOne.Card, CardType.Card | CardType.NonClassCard, ExodiaTheForbiddenOne.Card);
            ClassesRegistry.Register(RightLegOfTheForbiddenOne.Card, CardType.Card | CardType.NonClassCard, ExodiaTheForbiddenOne.Card);
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}