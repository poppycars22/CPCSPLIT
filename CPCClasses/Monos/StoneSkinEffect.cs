using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using UnboundLib.Networking;
using System.Collections;
using System.ComponentModel;
using ModdingUtils.Utils;

namespace CPCClasses.MonoBehaviours
{
    class StoneSkinEffect : MonoBehaviour
    {
        internal Player player;
        internal Gun gun;
        internal GunAmmo gunAmmo;
        internal Gravity gravity;
        internal HealthHandler health;
        internal CharacterData data;
        internal Block block;

        private Vector2 lastPosition;
        //private int count = 0;
        //private float distChange = 0.00f;
        private float timePass = 0.0f;
        private int secondCount = 0;

        private Vector2 damageAmount = Vector2.right;
        public float healRatio = 0.1f; //10  base hp/sec for base health of 100

        private void Start()
        {
            this.data = base.GetComponentInParent<CharacterData>();
            HealthHandler healthHandler = this.data.healthHandler;
            healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetStuff)); //Adds a reset to character on revive?
        }
        private void OnDestroy()
        {
            HealthHandler healthHandler = this.data.healthHandler;
            healthHandler.reviveAction = (Action)Delegate.Combine(healthHandler.reviveAction, new Action(this.ResetStuff)); //Adds a reset to character on revive?

        }
        public void Awake()
        {
            this.player = gameObject.GetComponent<Player>();
            this.gun = this.player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            this.data = this.player.GetComponent<CharacterData>();
            this.health = this.player.GetComponent<HealthHandler>();
            this.gravity = this.player.GetComponent<Gravity>();
            this.block = this.player.GetComponent<Block>();
            this.gunAmmo = this.gun.GetComponentInChildren<GunAmmo>();
            lastPosition = this.data.playerVel.position;
        }


        private void Update()
        {
            if ((this.data.input.direction == Vector3.zero || this.data.input.direction == Vector3.down || this.data.input.direction == Vector3.up) && PlayerStatus.PlayerAliveAndSimulated(player))
            {
                timePass += TimeHandler.deltaTime;
                if (timePass > 0.5f)  //every half second
                {
                    player.data.healthHandler.DoDamage(damageAmount * (1.5f+(float)Math.Pow(secondCount, 1.1f)), Vector2.down, Color.grey, null, null, false, true, true);
                    if (this.data.health <= 0)
                    {
                        secondCount = 0; //resets the exponential growth factor
                    }
                    timePass = 0.0f;
                    secondCount++;

                }
            }
            else
            {
                timePass = 0.0f;
                secondCount = 0;
            }
            if (this.data.health <= 0)
            {
                secondCount = 0;
                timePass = 0.0f;//resets the exponential growth factor
            }

        }
        private void ResetStuff()
        {
            timePass = 0.0f;
        }
        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }
    }
}