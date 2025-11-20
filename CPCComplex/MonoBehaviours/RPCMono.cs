using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{
    public class RPCMono : MonoBehaviour
    {
        [PunRPC]
        public void RPCASyncBlockThingy(int playerID)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(playerID);
            player.data.block.counter = -0.25f;
        }
    }
}
