using System;
using System.Collections;
using System.Collections.Generic;
//using Monocle;
using UnityEngine;
//using Celeste;
//using static Celeste.Player;
using System.Runtime.CompilerServices;
using UnboundLib;
using HarmonyLib;
using ChaosPoppycarsCards.Extensions;
using ModdingUtils.MonoBehaviours;
using Photon.Realtime;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;
using Photon.Pun;
using ModdingUtils.GameModes;


namespace CPCComplex.MonoBehaviours
{
    class ShadowCloakMono : MonoBehaviour
    {
        Player player;
        PlayerActions playerActions;
        private float countDown = 0;
        private bool called = false;
        public void Awake()
        {
            player = this.GetComponentInParent<Player>();
            playerActions = this.player.data.playerActions;
            countDown = 0;
            called = false;
        }
        public void Start()
        {
        }

        public void Update()
        {
            if (playerActions != null && playerActions.ActionIsPressed("BlockPhaseAction") && countDown <= 3)
            {
                //UnityEngine.Debug.Log("pressing");
                player.GetComponentInParent<PlayerCollision>().enabled = false;
                if (Physics2D.OverlapCircle(player.data.transform.position, 0.05f, (LayerMask)player.GetComponentInParent<CharacterData>().GetFieldValue("groundMask")))
                {
                    countDown += TimeHandler.deltaTime;
                }
            }
            if (playerActions != null && !playerActions.ActionIsPressed("BlockPhaseAction") || countDown >= 3)
            {
                if (!player.GetComponentInParent<PlayerCollision>().enabled)
                {
                    player.GetComponentInParent<PlayerCollision>().SetFieldValue("lastPos", new Vector2(player.data.transform.position.x, player.data.transform.position.y));
                    player.GetComponentInParent<PlayerCollision>().enabled = true;
                }
                if (Physics2D.OverlapCircle(player.data.transform.position, 0.05f, (LayerMask)player.GetComponentInParent<CharacterData>().GetFieldValue("groundMask")))
                {
                    player.GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(5);
                    player.GetComponentInParent<PlayerCollision>().enabled = false;
                    player.GetComponent<PlayerVelocity>().SetFieldValue("velocity", (Vector2)player.GetComponent<PlayerVelocity>().GetFieldValue("velocity") + new Vector2(0, 25));
                }
            }
            if(countDown >= 3 && !called)
            {
                ChaosPoppycarsCardsComplex.Instance.ExecuteAfterSeconds(4.5f, () => { countDown = 0; called = false;});
                called = true;
            }
        }
        public void OnDisable()
        {
            countDown = 0;
            called = false;
        }
        public void OnEnable()
        {
            player.GetComponentInParent<PlayerCollision>().enabled = true;
        }
    }
}