using System;
using System.Collections.Generic;
using System.Linq;
using RarityLib.Utils;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{

    public class RarityAdjusterCommons : MonoBehaviour
    {
        public float rarityScalar;
        public Player player;
        public Dictionary<Rarity, float> changes = new Dictionary<Rarity, float>();

        public void Start()
        {
            player = GetComponentInParent<Player>();
            if (!player.data.view.IsMine) return;
            UpdateRarity(RarityUtils.GetRarityData(CardInfo.Rarity.Common), rarityScalar);
        }

        public void UpdateRarity(Rarity rarity, float multiplier)
        {
            var delta = (rarity.calculatedRarity * multiplier) - rarity.calculatedRarity;
            rarity.calculatedRarity += delta;

            changes.Add(rarity, delta);
        }

        public void OnDestroy()
        {
            if (!player.data.view.IsMine) return;
            foreach (var rarity in changes.Keys)
            {
                rarity.calculatedRarity -= changes[rarity];
            }
        }
    }
}
