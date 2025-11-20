using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;
using PlayerTimeScale;

namespace CPCClasses.MonoBehaviours
{
    internal class TimeWarpMono : MonoBehaviour
    {
        Player player;
        PlayerTimeScale.PlayerTimeScale timeScale;
        public void Start()
        {
            this.player = this.GetComponentInParent<Player>();
            timeScale = player.ApplyTimeScale(0.75f);
            this.GetComponent<TrailRenderer>().time = 0.25f;
            this.GetComponent<TrailRenderer>().startColor = player.GetTeamColors().color/2;
            this.GetComponent<TrailRenderer>().endColor = new Color(player.GetTeamColors().color.r, player.GetTeamColors().color.g, player.GetTeamColors().color.b, 0f);
        }
        public void OnDestroy()
        {
            Destroy(timeScale);
        }

    }
}
