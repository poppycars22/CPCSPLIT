using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;

namespace CPCCharacters.MonoBehaviours
{
    internal class FlipGravEffect : ReversibleEffect
    {
        private float duration = 0;
        public override void OnOnDestroy()
        {
            data.block.BlockAction -= OnBlock;
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration += 2f;
            
        }

        public override void OnStart()
        {
            gravityModifier.gravityForce_mult = -1f;
            characterStatModifiersModifier.sizeMultiplier_mult = -1f;
            characterStatModifiersModifier.jump_mult = -1f;
            data.block.BlockAction += OnBlock;
            SetLivesToEffect(int.MaxValue);
        }
        public override void OnUpdate()
        {
            if (!(duration <= 0))
            {
                duration -= TimeHandler.deltaTime;
            }
            else
            {
                ClearModifiers();
                
            }
        }
        public override void OnOnDisable()
        {
            duration = 0;
            ClearModifiers();
        }
    }
}
