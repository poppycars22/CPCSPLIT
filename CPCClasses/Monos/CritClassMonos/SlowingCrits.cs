using UnityEngine;
using CPCCore.Extensions;


namespace CPCClasses.MonoBehaviours
{
    public class SlowingCrits : GenericCritMono
    {
        public override void OnCritAction(GameObject obj, int crits)
        {
            if(crits > 0)
            {
                ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
                bullet.movementSlow += gun.GetAdditionalData().CritSlow * crits;
            }
        }
    }
}





