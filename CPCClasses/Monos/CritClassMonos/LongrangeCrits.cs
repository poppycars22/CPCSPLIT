using UnityEngine;
using CPCCore.Extensions;


namespace CPCClasses.MonoBehaviours
{
    public class LongrangeCrits : GenericCritMono
    {
        public override void OnCritAction(GameObject obj, int crits)
        {
            if(crits > 0)
            {
                MoveTransform move = obj.GetComponent<MoveTransform>();
                move.localForce *= gun.GetAdditionalData().criticalBulletSpeed * crits;
            }
        }
    }
}





