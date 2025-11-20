using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace CPCClassic.MonoBehaviours
{
    internal class JumpSpeedEffect : ReversibleEffect
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
            effect.SetColor(new Color(0.2f, 0.1f, 1f));
        }

        public override void OnStart()
        {

            characterStatModifiersModifier.movementSpeed_mult = 2f;
            

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
