using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class CritRPCMono : MonoBehaviour
    {
        ChildRPC child;
        public void Awake()
        {
            child = GetComponentInParent<ChildRPC>();
            child.childRPCsInt.Add("CallCritSync", CallCritSync);
            child.view = child.GetComponent<PhotonView>();
        }
        public void CallCritSync(int critamt)
        {
            Player player = child.GetComponent<ProjectileHit>().ownPlayer;
            NEWCritHitBehaviour critB = player.gameObject.GetComponentInChildren<NEWCritHitBehaviour>();
            critB.SyncCriticalHit(critamt, player, child.gameObject);
        }
        public void OnDestroy()
        {
            child.childRPCsInt.Remove("CallCritSync");
        }
    }
}
