using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;

namespace CPCClassic.MonoBehaviours
{
    internal class SPRSodaEffect : ReversibleEffect
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
             ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.green);
        }

        public override void OnStart()
        {
            characterStatModifiersModifier.lifeSteal_add = 1f;
            characterStatModifiersModifier.sizeMultiplier_mult = 0.75f;
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
                Destroy(gameObject.GetOrAddComponent<ColorEffect>());
            }
        }
        public override void OnOnDisable()
        {
            duration = 0;
            ClearModifiers();
            Destroy(gameObject.GetOrAddComponent<ColorEffect>());
        }
        
    }
}
