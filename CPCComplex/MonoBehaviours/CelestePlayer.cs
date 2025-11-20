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
using CPCCore.Extensions;
using ModdingUtils.MonoBehaviours;
using Photon.Realtime;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;


namespace CPCComplex.MonoBehaviours
{
    class CelesteDash : MonoBehaviour
    {

        Player player;
        PlayerActions playerActions;
        private int dashesLeft =1;
        private int dashesMax =1;
        private float cd;
        public void Awake()
        {
            this.player = this.GetComponentInParent<Player>();
            playerActions = this.player.data.playerActions;
            cd = 0;
        }
        public UnityEngine.Vector2 Speed;

        public void Update()
        {
            dashesMax = player.data.stats.GetAdditionalData().dashes;
            if(cd>0)
                cd -= TimeHandler.deltaTime;
            if (player.data.isGrounded && player.data.view.IsMine && playerActions!= null && !playerActions.ActionIsPressed("Dash"))
                dashesLeft = dashesMax;
            if(playerActions!=null && playerActions.ActionIsPressed("Dash") && dashesLeft>0 && player.data.view.IsMine && cd <=0)
            {
                //UnityEngine.Debug.Log(player.data.aimDirection.x + " aim x " + player.data.aimDirection.y + " aim y");
               
                float angle = Mathf.Rad2Deg*Mathf.Atan2(player.data.input.direction.y, player.data.input.direction.x);
                
                
                if (angle > -25 && angle <= 25)
                    Speed = new Vector2(1, 0);
                else if (angle > 25 && angle <= 75)
                    Speed = new Vector2(0.75f, 0.75f);
                else if (angle > 75 && angle <= 125)
                    Speed = new Vector2(0, 1);
                else if (angle > 125 && angle <= 170)
                    Speed = new Vector2(-0.75f, 0.75f);
                else if (angle > 170)
                    Speed = new Vector2(-1, 0);
                else if (angle <= -170)
                    Speed = new Vector2(-1, 0);
                else if (angle > -170 && angle <= -125)
                    Speed = new Vector2(-0.75f, -0.75f);
                else if (angle > -125 && angle <= -75)
                    Speed = new Vector2(0, -1);
                else
                    Speed = new Vector2(0.75f, -0.75f);
                //Speed = new Vector2(player.data.aimDirection.x*10, player.data.aimDirection.y*10);
                //player.data.movement.Move(Speed*50);
                player.GetComponent<PlayerVelocity>().SetFieldValue("velocity",Speed*125);
                dashesLeft-=1;
                cd = 0.30f;
                ChaosPoppycarsCardsComplex.Instance.ExecuteAfterFrames(2, () =>
                {
                    this.player.gameObject.GetOrAddComponent<CelesteGrav>();
                });
             
                //player.data.movement.Move(new Vector2(500,500));
            }
        }


    }

    internal class CelesteGrav : ReversibleEffect
    {
        private float duration = 0;
        private int timesJumped = 0;
        public override void OnStart()
        {
            this.player = this.GetComponentInParent<Player>();
            gravityModifier.gravityForce_mult = 0;
            timesJumped = 0;
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 0.15f;
            data.jump.JumpAction += OnJump;
            SetLivesToEffect(int.MaxValue);
        }
        private void OnJump()
        {
            if (timesJumped <= 0)
            {
                Vector2 velocitya = (Vector2)player.GetComponent<PlayerVelocity>().GetFieldValue("velocity");
                player.GetComponent<PlayerVelocity>().SetFieldValue("velocity", new Vector2(velocitya.x * 5f, velocitya.y * 2.5f));
                timesJumped++;
            }
        }
        public override void OnUpdate()
        {
            if (!(duration <= 0))
            {
                   duration -= TimeHandler.deltaTime;
            }
            else
            {
                ClearModifiers();
                timesJumped = 0;
                player.data.sinceGrounded = 0;
                Destroy(this);
            }
        }
        public override void OnOnDestroy()
        {
            data.jump.JumpAction -= OnJump;
            ClearModifiers();
        }
        public override void OnOnDisable()
        {
            duration = 0;
            ClearModifiers();
        }
    }
}