using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;


namespace CPCClasses.MonoBehaviours
{
    public class BouncyCrits : GenericCritMono
    {
        public override void OnCritAction(GameObject obj, int crits)
        {
            if(crits > 0)
            {
                if (!obj.GetComponent<RayHitReflect>())
                {
                    RayHitReflect rayHitReflect = obj.gameObject.AddComponent<RayHitReflect>();
                    rayHitReflect.reflects += (gun.GetAdditionalData().criticalHitBounces * crits) - 1;
                    rayHitReflect.dmgM = gun.dmgMOnBounce;
                    rayHitReflect.dmgM += (gun.GetAdditionalData().criticalHitDamageOnBounce*crits);
                    rayHitReflect.speedM = gun.speedMOnBounce;
                }
                else
                {
                    RayHitReflect rayHitReflect = obj.gameObject.GetComponent<RayHitReflect>();
                    rayHitReflect.reflects += (gun.GetAdditionalData().criticalHitBounces * crits);
                    rayHitReflect.dmgM += (gun.GetAdditionalData().criticalHitDamageOnBounce * crits);
                }
            }
        }
    }
}





