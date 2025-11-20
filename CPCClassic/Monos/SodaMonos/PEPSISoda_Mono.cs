using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace CPCClassic.MonoBehaviours
{
    internal class PEPSodaEffect : ReversibleEffect
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
            effect.SetColor(Color.blue);

        }

        public override void OnStart()
        {
            characterStatModifiersModifier.movementSpeed_mult = 1.25f;
            characterStatModifiersModifier.secondsToTakeDamageOver_add = 3;
            characterDataModifier.maxHealth_mult = 1.25f;
            
            
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
