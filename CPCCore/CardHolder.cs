using UnityEngine;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using System.Collections.Generic;
using CPCCardInfostuffs;

public class CardHolder : MonoBehaviour
{
    public List<GameObject> Cards;
    public List<GameObject> HiddenCards;

    public void RegisterCards()
    {
        foreach (var Card in Cards)
        {
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
