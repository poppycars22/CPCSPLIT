using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{
    public class VampnackRPCMono : MonoBehaviour
    {
        [PunRPC]
        public void RPCASyncHeal(int playerID, float heal)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(playerID);
            player.GetComponent<HealthHandler>().Heal(heal);
        }
    }
}
