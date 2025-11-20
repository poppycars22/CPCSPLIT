using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;

namespace CPCCharacters.MonoBehaviours
{
    public class DamageCD : MonoBehaviour
    {
        public float duration = 0;
        Player player;
        public void Awake()
        {
            this.player = this.GetComponentInParent<Player>();
        }
        public void Update()
        {
            if (!(duration <= 0))
            {
                duration -= TimeHandler.deltaTime;
            }
        }

    }
}
