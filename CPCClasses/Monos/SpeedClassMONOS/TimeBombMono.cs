using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;
using PlayerTimeScale;

namespace CPCClasses.MonoBehaviours
{
    internal class TimeBombMono : MonoBehaviour
    {
        Player player;
        PlayerTimeScale.PlayerTimeScale timeScale;
        public void Start()
        {
            this.player = this.GetComponentInParent<Player>();
            timeScale = player.ApplyTimeScale(1.75f);
        }
        public void OnDestroy()
        {
            Destroy(timeScale);
        }

    }
}
