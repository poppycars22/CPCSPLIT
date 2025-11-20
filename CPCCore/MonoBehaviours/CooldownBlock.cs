using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Extensions;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;

namespace CPCCore.MonoBehaviours
{
    public class CooldownBlock : MonoBehaviour
    {
        public float duration = 0;
        Player player;
        PlayerActions playerActions;
        public void Awake()
        {
            this.player = this.GetComponentInParent<Player>();
            playerActions = this.player.data.playerActions;
        }
        public void Update()
        {

            if (playerActions != null && playerActions.ActionWasPressed("BlockMoveSwitch"))
            {
                player.data.stats.GetAdditionalData().blockPush = !player.data.stats.GetAdditionalData().blockPush;
                if(player.data.stats.GetAdditionalData().blockPush)
                {
                    ColorEffect effect = player.gameObject.GetOrAddComponent<ColorEffect>();
                    effect.SetColor(Color.red);
                    ChaosPoppycarsCardsCore.Instance.ExecuteAfterSeconds(0.5f, () =>
                    {
                        Destroy(gameObject.GetOrAddComponent<ColorEffect>());
                    });
                }
                else
                {
                    ColorEffect effect = player.gameObject.GetOrAddComponent<ColorEffect>();
                    effect.SetColor(Color.blue);
                    ChaosPoppycarsCardsCore.Instance.ExecuteAfterSeconds(0.5f, () =>
                    {
                        Destroy(gameObject.GetOrAddComponent<ColorEffect>());
                    });
                }
            }
            if (!(duration <= 0))
            {
                duration -= TimeHandler.deltaTime;
            }
        }

    }
}
