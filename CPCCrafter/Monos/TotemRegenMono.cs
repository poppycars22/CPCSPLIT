using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
namespace CPCCrafter.MonoBehaviours
{
    internal class TotemRegenEffect : ReversibleEffect
    {
        private float duration = 0;
        private float HealAmt = 0;
        public override void OnStart()
        {

            HealAmt = (player.data.maxHealth * 0.4f);
            healthHandlerModifier.regen_add = (HealAmt / 3f);
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 3f;
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.magenta);
            SetLivesToEffect(int.MaxValue);
        }
        public override void OnUpdate()
        {
            if (!(duration <= 0))
            {
               // data.healthHandler.Heal(HealAmt * TimeHandler.deltaTime);
                duration -= TimeHandler.deltaTime;
            }
            else
            {
                ClearModifiers();
                Destroy(gameObject.GetOrAddComponent<ColorEffect>());
                Destroy(this);
            }
        }
        public override void OnOnDisable()
        {
            duration = 0;
            Destroy(gameObject.GetOrAddComponent<ColorEffect>());
            ClearModifiers();
        }
    }
}
