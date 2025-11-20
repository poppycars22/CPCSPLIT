using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace CPCClassic.MonoBehaviours
{
    internal class JumpBurstEffect : ReversibleEffect
    {
        private float duration = 0;
        public override void OnOnDestroy()
        {
            data.jump.JumpAction -= OnJump;
        }
        private void OnJump()
        {
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 0.65f;
            
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(new Color(0.6f, 0f, 0.923f));
        }

        public override void OnStart()
        {

            gunStatModifier.bursts_add = 2;
            gunStatModifier.timeBetweenBullets_add = 0.1f;
            data.jump.JumpAction += OnJump;
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
