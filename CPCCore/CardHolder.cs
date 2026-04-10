using BepInEx;
using CPCCardInfostuffs;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace CPCCore
{
    public class CardHolder : MonoBehaviour
    {
        public List<GameObject> Cards;
        public List<GameObject> HiddenCards;

        public void RegisterCards()
        {
            //string modInitials = (string)new StackTrace().GetFrame(1).GetMethod().ReflectedType.GetField("ModInitials", BindingFlags.Static | BindingFlags.Public).GetValue(null);
            foreach (var Card in Cards)
            {
                //CustomCard.RegisterUnityCard(Card, $"CPC ({modInitials})", Card.GetComponent<CardInfo>().cardName, true, null);
                if (Card.gameObject.GetComponent<CPCCardInfo>() == null)
                {
                    CustomCard.RegisterUnityCard(Card, "CPC", Card.GetComponent<CardInfo>().cardName, true, null);
                }
                else
                {
                    CustomCard.RegisterUnityCard(Card, Card.gameObject.GetComponent<CPCCardInfo>().Tag, Card.GetComponent<CardInfo>().cardName, true, null);
                }
            }
            foreach (var Card in HiddenCards)
            {
                //CustomCard.RegisterUnityCard(Card, "CPC", Card.GetComponent<CardInfo>().cardName, false, null);
                //ModdingUtils.Utils.Cards.instance.AddHiddenCard(Card.GetComponent<CardInfo>());
            }
        }
    }
}
