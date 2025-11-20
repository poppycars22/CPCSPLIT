using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Utils;
using System;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine.UI;

namespace CPCCurses.MonoBehaviours
{
    internal class LetterArt : MonoBehaviour
    {
        public void Start()
        {
            string letter = GetComponentInParent<CardInfo>().GetComponent<LetterComponent>().letter;
            this.GetComponentInChildren<Text>().text = letter.ToUpper() + letter.ToLower();
        }
    }
}
