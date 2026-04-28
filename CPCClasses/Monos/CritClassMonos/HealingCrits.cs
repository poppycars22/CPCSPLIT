using UnityEngine;
using CPCCore.Extensions;


namespace CPCClasses.MonoBehaviours
{
    public class HealingCrits : GenericCritMono
    {
        public override void OnCritAction(GameObject obj, int crits)
        {
            if(crits > 0)
            {
                HealthHandler h = player.data.healthHandler;
                h.Heal(gun.GetAdditionalData().criticalHeal * crits);
            }
        }
    }
}





