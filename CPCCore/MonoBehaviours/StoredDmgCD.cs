using CPCCore.Extensions;
using UnityEngine;

namespace CPCCore.MonoBehaviours
{
    /*public class StoredDmgCD : MonoBehaviour
    {
        public float duration = 0;
        Player player;
        public void Awake()
        {
            this.player = this.GetComponentInParent<Player>();
        }
        public void Update()
        {
            if (!(duration <= 0) && !player.data.stats.GetAdditionalData().takeStoredDamage)
            {
                duration -= TimeHandler.deltaTime;
            }
            else if (duration <= 0)
            {
                if (player.data.stats.GetAdditionalData().storedDamage.magnitude > 0)
                {
                    player.data.stats.GetAdditionalData().takeStoredDamage = true;
                    player.gameObject.GetComponent<HealthHandler>().TakeDamage(new Vector2(0.1f, 0), player.transform.position, null, null, true, true);
                }
                
                UnityEngine.Debug.Log("a");
            }
        }

    }   */
}
