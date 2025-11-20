using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace CPCClassic.MonoBehaviours
{
    internal class DRSodaEffect : ReversibleEffect
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
            effect.SetColor(Color.red);
        }

        public override void OnStart()
        {
            gunStatModifier.damage_mult = 1.5f;
            gunAmmoStatModifier.reloadTimeMultiplier_mult = 0.5f;
            gunStatModifier.attackSpeed_mult = 0.5f;
            
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
