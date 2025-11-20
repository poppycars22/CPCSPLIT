using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace CPCClassic.MonoBehaviours
{
    internal class MTDSodaEffect : ReversibleEffect
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
            characterStatModifiersModifier.movementSpeed_mult = 2f; //make it affect speed, jumpheight, and increase size
            characterStatModifiersModifier.sizeMultiplier_mult = 1.5f;
            characterStatModifiersModifier.jump_mult = 1.25f;
           
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
