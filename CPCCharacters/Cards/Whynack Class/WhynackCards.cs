using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using BepInEx;
using System.Reflection;


namespace CPCCharacters.Cards
{
    class WhynackCards : ClassHandler
    {
        internal static string name = "Whynacks";
        internal static List<BaseUnityPlugin> plugins;

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(Whynack.Card)) yield return null;
            ClassesRegistry.Register(Whynack.Card, CardType.Entry | CardType.NonClassCard);

            //change to pickphase ver of cards VVVVVV
            ClassesRegistry.Register(WhynackForward.Card,  CardType.Card | CardType.NonClassCard, Whynack.Card);
            ClassesRegistry.Register(WhynackGoku.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackShamrock.Card, CardType.Card | CardType.NonClassCard, Whynack.Card);
            ClassesRegistry.Register(WhynackDoubleVision.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackArguing.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackAdrenaline.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackHarmony.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackMeditating.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackBlockMeditating.Card, CardType.Card | CardType.NonClassCard, Whynack.Card);
            ClassesRegistry.Register(WhynackUppercut.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
            ClassesRegistry.Register(WhynackVampire.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 3);
            ClassesRegistry.Register(WhynacksBlasting.Card, CardType.Card | CardType.NonClassCard, Whynack.Card, 1);
        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}