using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using BepInEx;
using System.Reflection;


namespace CPCCommissions.Cards
{
    class WhynackGunCards : ClassHandler
    {
        internal static string name = "Gunack";
        internal static List<BaseUnityPlugin> plugins;

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(WhynackWithAGun.Card)) yield return null;
            ClassesRegistry.Register(WhynackWithAGun.Card, CardType.Entry | CardType.NonClassCard, 2);
            ClassesRegistry.Register(WhynackGunLaunch.Card, CardType.Card | CardType.NonClassCard, WhynackWithAGun.Card);
            ClassesRegistry.Register(WhynacksAcceleration.Card, CardType.Card | CardType.NonClassCard, WhynackWithAGun.Card);
            ClassesRegistry.Register(WhynacksGravityBullet.Card, CardType.Card | CardType.NonClassCard, WhynackWithAGun.Card);
            ClassesRegistry.Register(WhynackWantedHoming.Card, CardType.Card | CardType.NonClassCard, WhynackWithAGun.Card);
            ClassesRegistry.Register(WhynackWithADice.Card, CardType.Card | CardType.NonClassCard, WhynackWithAGun.Card, 1);
            ClassesRegistry.Register(WhynackWithAUkelele.Card, CardType.Card | CardType.NonClassCard, WhynackWithAGun.Card, 1);

        }
        public override IEnumerator PostInit()
        {
            yield break;
        }
    }
}