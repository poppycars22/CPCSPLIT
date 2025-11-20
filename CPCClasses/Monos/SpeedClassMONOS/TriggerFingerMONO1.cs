using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class TriggerFingerMono : ReversibleEffect
    {
        public override void OnStart()
        {
            gunStatModifier.attackSpeed_mult = (1f / (Mathf.Abs(player.data.stats.movementSpeed) / 2.5f) * 0.5f) * 1.5f;
            ApplyModifiers();
        }
        public override void OnUpdate()
        {
            gunStatModifier.attackSpeed_mult = (1f / (Mathf.Abs(player.data.stats.movementSpeed) / 2.5f) * 0.5f) * 1.5f;
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