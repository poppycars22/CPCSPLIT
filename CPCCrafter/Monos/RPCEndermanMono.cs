using Photon.Pun;
using Sonigon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace CPCComplex.MonoBehaviours
{
    public class RPCEndermanMono : MonoBehaviour
    {
        [PunRPC]
        public void RPCAEndermanTP(int playerID, float damage, bool selfDmg, int bulId)
        {
            GameObject bullet = PhotonNetwork.GetPhotonView(bulId).gameObject;
            Player player = PlayerManager.instance.GetPlayerWithID(playerID);
            StartCoroutine(bullet.GetComponent<ProjectileHit>().HoldPlayer(player.data.healthHandler));
            player.GetComponentInChildren<EndermenTeleport>().Trigger(damage, selfDmg, bullet.GetComponent<MoveTransform>().velocity);
            bullet.GetComponent<MoveTransform>().velocity *= -1f;
        }
    }
}
