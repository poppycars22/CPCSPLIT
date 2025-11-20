using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;

namespace CPCClassic.MonoBehaviours
{
    internal class COCSodaEffect : ReversibleEffect
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
            duration = 2f;
             ColorEffect effect = player.gameObject.GetOrAddComponent<ColorEffect>();
            effect.SetColor(Color.red);
        }

        public override void OnStart()
        {
            gravityModifier.gravityForce_mult = 0.5f;
            characterDataModifier.maxHealth_mult = 1.5f;
            healthHandlerModifier.regen_add = (50f / 5f);


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
                Destroy(player.gameObject.GetOrAddComponent<ColorEffect>());
            }
        }
        public override void OnOnDisable()
        {
            duration = 0;
            ClearModifiers();
            Destroy(player.gameObject.GetOrAddComponent<ColorEffect>());
        }
    }
}
