using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class BoostedBlockMono : ReversibleEffect
    {
        public override void OnStart()
        {
            blockModifier.cdMultiplier_mult = Mathf.Clamp(1f / (Mathf.Abs(player.data.stats.movementSpeed) / 3f), 0.2f, 1.25f);
            ApplyModifiers();
        }
        public override void OnUpdate()
        {
            blockModifier.cdMultiplier_mult = Mathf.Clamp(1f / (Mathf.Abs(player.data.stats.movementSpeed) / 3f), 0.2f, 1.25f);
            ApplyModifiers();
        }
        public override void OnOnDisable()
        {
            ClearModifiers();
            Destroy();
        }
        public override void OnOnDestroy()
        {
            ClearModifiers();
            base.OnOnDestroy();
        }
    }
}