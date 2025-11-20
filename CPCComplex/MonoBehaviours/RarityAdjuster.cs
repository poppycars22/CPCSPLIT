using System;
using System.Collections.Generic;
using System.Linq;
using RarityLib.Utils;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{

    public class RarityAdjuster : MonoBehaviour
    {
        public float rarityScalar;
        public Player player;
        public Dictionary<Rarity, float> changes = new Dictionary<Rarity, float>();

        public void Start()
        {
            player = GetComponentInParent<Player>();
            var rareData = RarityUtils.GetRarityData(CardInfo.Rarity.Rare);
            foreach (var rarity in RarityUtils.Rarities.Values.Where(r => r.relativeRarity <= rareData.relativeRarity))
            {
                UpdateRarity(rarity, rarityScalar);
            }
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
