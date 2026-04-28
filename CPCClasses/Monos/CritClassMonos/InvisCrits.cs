using UnityEngine;
using CPCCore.Extensions;


namespace CPCClasses.MonoBehaviours
{
    public class InvisCrits : GenericCritMono
    {
        public override void OnCritAction(GameObject obj, int crits)
        {
            if(crits > 0)
            {
                RayCastTrail trail = obj.GetComponent<RayCastTrail>();
                trail.mask = trail.ignoreWallsMask;
            }
        }
    }
}





