using UnityEngine;
using HarmonyLib;
using ModdingUtils.MonoBehaviours;
using CPCCore.Extensions;
using UnboundLib;

namespace CPCCrafter.MonoBehaviours
{
    public class TotemEffect : ReversibleEffect
    {
        ColorFlash colorFlash = null;
        HealthHandler healthHandler;
        public float cd = 0;
        public override void OnStart()
        {
            base.SetLivesToEffect(int.MaxValue);
            healthHandler = player.data.healthHandler;
        }
        public void Update()
        {
            if (cd >= 0)
                cd -= Time.deltaTime;
        }
      
        public void UseMulligan()
        {
            // if there are no mulligans left or if its on cd, just return
            if (base.stats.GetAdditionalData().remainingTotems <= 0 || cd > 0)
            {
                return;
            }

            // force the player to block
            base.block.CallDoBlock(false, false, BlockTrigger.BlockTriggerType.Default);
            cd += 0.1f;
            ChaosPoppycarsCardsCrafter.Instance.ExecuteAfterFrames(5, () =>
            {
                this.player.gameObject.GetOrAddComponent<TotemRegenEffect>();
            });


            // stop DoT effects
            ((DamageOverTime)Traverse.Create(base.health).Field("dot").GetValue()).StopAllCoroutines();
            this.colorFlash = base.player.gameObject.GetOrAddComponent<ColorFlash>();
            this.colorFlash.SetNumberOfFlashes(1);
            this.colorFlash.SetDuration(0.25f);
            this.colorFlash.SetDelayBetweenFlashes(0.25f);
            this.colorFlash.SetColorMax(Color.white);
            this.colorFlash.SetColorMin(Color.white);

            // use up a single mulligan
            base.stats.GetAdditionalData().remainingTotems--;
        }
        public override void OnOnDestroy()
        {
        }
    }

}