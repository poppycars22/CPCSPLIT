using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;


namespace CPCClasses.MonoBehaviours
{
    public class AmmoCrits : GenericCritMono
    {
        public override void OnCritAction(GameObject obj, int crits)
        {
            if (crits > 0)
            {
                gun.gunAmmo.currentAmmo += 1 * crits;
                gun.gunAmmo.SetActiveBullets();
                gun.isReloading = false;
            }
        }
    }
}





