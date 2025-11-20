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

namespace CPCCharacters.MonoBehaviours
{
    class WhynackBlockMono : MonoBehaviour
    {
        internal Player player;
        internal CharacterData data;

        private Vector2 lastPosition;
        //private int count = 0;
        //private float distChange = 0.00f;
        private float timePass = 0.0f;
        private float secondCount = 0f;

        

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
            this.data = this.player.GetComponent<CharacterData>();
            lastPosition = this.data.playerVel.position;
        }


        //Should the Health Boost be exponential? Right now will be 1hp per second (if at base health of 100)
        private void Update()
        {
            if (this.data.input.direction == Vector3.zero || this.data.input.direction == Vector3.down || this.data.input.direction == Vector3.up && PlayerStatus.PlayerAliveAndSimulated(player))
            {
                timePass += TimeHandler.deltaTime;
                if (timePass > 0.1f)
                {
                    data.block.counter += 0.075f;
                    timePass = 0.0f;

                }
            }
            else
            {
                timePass = 0.0f;
            }
            if (this.data.health <= 0)
            {
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