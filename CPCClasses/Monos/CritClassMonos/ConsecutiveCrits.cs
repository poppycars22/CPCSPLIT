using UnityEngine;
using CPCCore.Extensions;
using ModdingUtils.Utils;

namespace CPCClasses.MonoBehaviours
{
    public class ConsecutiveCrits : GenericCritMono
    {
        int critsInARow = 0;
        int maxCrit = 1;
        public override void OnCritAction(GameObject obj, int crits)
        {
            if(crits >= maxCrit)
            {
                ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
                critsInARow++;
                maxCrit = crits;
                bullet.damage += (gun.GetAdditionalData().consecutiveCritsDamage * critsInARow * crits)/2;
            }
            else
            {
                critsInARow = 0;
                maxCrit = 1;
            }
        }
        public void Update()
        {
            if (!PlayerStatus.PlayerAliveAndSimulated(player))
            {
                critsInARow = 0;
            }
        }
    }
}





