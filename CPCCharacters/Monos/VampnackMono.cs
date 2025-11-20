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
using CPCComplex.MonoBehaviours;



public class VampnackMono : MonoBehaviour
{

	[Header("Settings")]
	public bool lethal = true;

	public Player owner;

	private void Start()
	{
        owner = this.gameObject.GetComponent<SpawnedAttack>().spawner;
        Destroy(this.gameObject, 1.5f);
	}
	private void Update()
	{
        owner = this.gameObject.GetComponent<SpawnedAttack>().spawner;
        if (owner != null && (owner.data.view.IsMine || PhotonNetwork.OfflineMode))
        {
            Collider2D[] plays = null;
            plays = Physics2D.OverlapCircleAll(owner.transform.position, owner.transform.localScale.z + 0.2f, LayerMask.GetMask("Player"));
            if (plays != null)
            {
                foreach (Collider2D play in plays)
                {
                    if (play.gameObject.GetComponent<Player>() != null && play.gameObject.GetComponent<Player>().playerID != owner.playerID)
                    {
                        Player player = play.gameObject.GetComponent<Player>();
                        player.GetComponent<HealthHandler>().CallTakeDamage(new Vector2(0, player.data.maxHealth * 0.15f), owner.transform.position, null, null, true);
                        PhotonView photonView = owner.GetComponent<PhotonView>();
                        owner.gameObject.GetOrAddComponent<VampnackRPCMono>();
                        photonView.RPC("RPCASyncHeal", RpcTarget.All, owner.playerID, player.data.maxHealth * 0.15f);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }
}
