using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;

namespace CPCCrafter.MonoBehaviours
{
    internal class InvisEffect : ReversibleEffect
    {
        private float duration = 0;
        private bool IgnoreWallStorage = false;
        public override void OnOnDestroy()
        {
            data.block.BlockAction -= OnBlock;
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (stats.GetAdditionalData().Glowstone <= 0)
            {
                gunStatModifier.projectileColor = Color.clear;
            }
            else
            {
                gunStatModifier.projectileColor = gun.projectileColor;
            }
            if (stats.GetAdditionalData().Glowstone >= 1 && duration <= 0)
            {
                IgnoreWallStorage = gun.ignoreWalls;
                gun.ignoreWalls = true;
            }
            if (!(player.data.view.IsMine) && stats.GetAdditionalData().Glowstone >= 1)
            {
                gunStatModifier.projectileColor = Color.clear;
            }
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 3f + (stats.GetAdditionalData().Redstone * 1.5f);
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.clear);
        }

        public override void OnStart()
        {
            if (player.data.view.IsMine)
            {
                if (ChaosPoppycarsCardsCrafter.MC_Particles.Value)
                {
                    characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCardsCrafter.Bundle.LoadAsset<GameObject>("PotionMCParticle_Invisibility"));
                }
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
                if (stats.GetAdditionalData().Glowstone >= 1)
                {
                    gun.ignoreWalls = IgnoreWallStorage;
                }
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
