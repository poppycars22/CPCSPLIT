using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;

namespace CPCCrafter.MonoBehaviours
{
    internal class JumpEffect : ReversibleEffect
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
                characterDataModifier.numberOfJumps_add = 3 + stats.GetAdditionalData().Glowstone;
                ApplyModifiers();
            }
            duration = 3f + (stats.GetAdditionalData().Redstone * 1.5f);
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.green);
        }
        public override void OnStart()
        {
            characterStatModifiersModifier.jump_add = 0.5f;
            gravityModifier.gravityForce_mult = 0.5f;

            if (!stats.GetAdditionalData().InvisPot)
            {

                if (ChaosPoppycarsCardsCrafter.MC_Particles.Value)
                {
                    characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("PotionMCParticle_Jump"));
                }

            }
            else if (stats.GetAdditionalData().InvisPot && data.view.IsMine && ChaosPoppycarsCardsCrafter.MC_Particles.Value)
            {
                characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("PotionMCParticle_Jump"));
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
                UnityEngine.GameObject.Destroy(this.gameObject.GetOrAddComponent<ColorEffect>());
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
