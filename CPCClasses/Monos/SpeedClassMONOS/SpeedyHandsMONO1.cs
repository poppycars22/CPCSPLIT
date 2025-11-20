using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class SpeedyHandsMono : ReversibleEffect
    {
        public override void OnStart()
        {
            gunAmmoStatModifier.reloadTimeMultiplier_mult = (1f / (Mathf.Abs(player.data.stats.movementSpeed) / 2f) * 0.3f) * 3f;
            ApplyModifiers();
        }
        public override void OnUpdate()
        {
            gunAmmoStatModifier.reloadTimeMultiplier_mult = (1f / (Mathf.Abs(player.data.stats.movementSpeed) / 2f) * 0.3f) * 3f;
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