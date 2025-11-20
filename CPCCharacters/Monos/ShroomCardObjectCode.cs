using Sonigon;
using UnityEngine;
using UnboundLib;
using HarmonyLib;
using ModdingUtils.Utils;
using AmplifyColor;
using Photon.Realtime;
using UnityEngine.SocialPlatforms;
using CPCCore.Extensions;
using CPCCharacters.Shaders;
using CPCCharacters;
using Photon.Pun;



public class ShroomCardObjectCode : MonoBehaviour
{

	[Header("Settings")]
	public float damage = 25f;

	public bool lethal = true;

	public Player owner;

	private void Start()
	{

	}
	public void Go()
	{
		owner = GetComponentInParent<SpawnedAttack>().spawner;
		//target = GetComponentInParent<PlayerInRangeTrigger>().target;
		//target = PlayerManager.instance.GetOtherPlayer(target);
		foreach (Player target in ModdingUtils.Utils.PlayerStatus.GetOtherPlayers(owner))
        {
			if (PlayerManager.instance.CanSeePlayer(base.transform.position, target).canSee && Vector3.Distance(base.transform.position, target.transform.position) < GetComponentInParent<PlayerInRangeTrigger>().range * base.transform.root.localScale.x && !target.data.dead)
			{
				if (target != null && owner != null && target.teamID == owner.teamID)
				{
					target.data.healthHandler.Heal((damage/2) + (owner.data.stats.GetAdditionalData().shroomsAmt * 0.2f * damage));
					if(PhotonNetwork.OfflineMode || target.data.view.IsMine)
					{
                        var a = Camera.main.gameObject.GetOrAddComponent<PixelateEffect>();
						a.player = target;
                        if ((target.data.view.IsMine || PhotonNetwork.OfflineMode) && ChaosPoppycarsCardsCharacters.ShroomAccess.Value)
                            a.Material = ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<Material>("ShaderTest 1");
                        else if (target.data.view.IsMine || PhotonNetwork.OfflineMode)
                            a.Material = ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<Material>("ShaderTest");
                        Destroy(a, 5f);
                    }
				}
				else if (target != null)
				{
					target.data.healthHandler.TakeDamage((damage + (owner.data.stats.GetAdditionalData().shroomsAmt * 0.2f *damage)) * Vector2.up, Vector2.down, Color.magenta, null, owner, lethal, true);
                    if (PhotonNetwork.OfflineMode || target.data.view.IsMine)
                    {
                        var a = Camera.main.gameObject.GetOrAddComponent<PixelateEffect>();
                        a.player = target;
                        if ((target.data.view.IsMine || PhotonNetwork.OfflineMode) && ChaosPoppycarsCardsCharacters.ShroomAccess.Value)
                            a.Material = ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<Material>("ShaderTest 1");
                        else if (target.data.view.IsMine || PhotonNetwork.OfflineMode)
                            a.Material = ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<Material>("ShaderTest");
                        Destroy(a, 5f);
                    }
                }
			}
        }
        if (owner != null && PlayerManager.instance.CanSeePlayer(base.transform.position, owner).canSee && Vector3.Distance(base.transform.position, owner.transform.position) < GetComponentInParent<PlayerInRangeTrigger>().range * base.transform.root.localScale.x && !owner.data.dead)
        {
            owner.data.healthHandler.Heal((damage/2) + (owner.data.stats.GetAdditionalData().shroomsAmt * 0.2f * damage));
			if (PhotonNetwork.OfflineMode || owner.data.view.IsMine)
			{
				var a = Camera.main.gameObject.GetOrAddComponent<PixelateEffect>();
				a.player = owner;
				if((owner.data.view.IsMine || PhotonNetwork.OfflineMode) && ChaosPoppycarsCardsCharacters.ShroomAccess.Value)
					a.Material = ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<Material>("ShaderTest 1");
				else if (owner.data.view.IsMine || PhotonNetwork.OfflineMode)
                    a.Material = ChaosPoppycarsCardsCharacters.Bundle.LoadAsset<Material>("ShaderTest");
                Destroy(a, 5f);
			}
        }
        //owner.data.healthHandler.TakeDamage(damage * Vector2.up, Vector2.down, Color.magenta, null, null, lethal, true);
        //target.data.healthHandler.TakeDamage(damage * Vector2.up, Vector2.down, Color.magenta, null, null, lethal, true);

    }
}
