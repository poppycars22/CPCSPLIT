using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class AcceleratedRejuvenationMono : ReversibleEffect
    {
        public override void OnStart()
        {
            healthHandlerModifier.regen_add = (Mathf.Abs(player.data.stats.movementSpeed*3));
            
            ApplyModifiers();
        }
        public override void OnUpdate()
        {
            healthHandlerModifier.regen_add = (Mathf.Abs(player.data.stats.movementSpeed*3));
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