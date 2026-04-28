using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace CPCClasses.MonoBehaviours
{
    public class GenericCritMono : PreventMultipleObj
    {
        public Player player;
        public NEWCritHitBehaviour critBehaviour;
        public Gun gun;
        public void Start()
        {
            this.player = this.GetComponentInParent<Player>();
            this.gun = this.player.data.weaponHandler.gun;
            this.critBehaviour = player.GetComponentInChildren<NEWCritHitBehaviour>();
            critBehaviour.CritHitAction += this.OnCritAction;
            this.OnStart();
        }
        public virtual void OnStart() { }
        public virtual void OnCritAction(GameObject obj, int crits) { }
        public void OnDestroy()
        {
            critBehaviour.CritHitAction -= this.OnCritAction;
            this.Destroy();
        }
        public virtual void Destroy() { }
    }
}
