using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;

namespace CPCCrafter.MonoBehaviours
{
    internal class RegenEffect : ReversibleEffect
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
                healthHandlerModifier.regen_add = (((0.2f + (stats.GetAdditionalData().Glowstone / 15f)) * player.data.maxHealth) / 3f);
                ApplyModifiers();
            }
            duration = 3f + (stats.GetAdditionalData().Redstone * 1.5f);
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.magenta);
        }

        public override void OnStart()
        {
            if (!stats.GetAdditionalData().InvisPot)
            {

                if (ChaosPoppycarsCardsCrafter.MC_Particles.Value)
                {
                    characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("PotionMCParticle_Regen"));
                }

            }
            else if (stats.GetAdditionalData().InvisPot && data.view.IsMine && ChaosPoppycarsCardsCrafter.MC_Particles.Value)
            {
                characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("PotionMCParticle_Regen"));
            }

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
            Destroy(gameObject.GetOrAddComponent<ColorEffect>());
            ClearModifiers();
        }
    }
}
