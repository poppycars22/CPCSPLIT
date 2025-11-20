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
using CPCCore.Utilities;
using CPCCore.MonoBehaviours;
using ModdingUtils.MonoBehaviours;
using Photon.Realtime;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;
using Photon.Pun;
using ModdingUtils.GameModes;


namespace CPCComplex.MonoBehaviours
{
    class ScreenWarpMono : MonoBehaviour, IPointStartHookHandler
    {

        Player player;
        private int warpsLeft =5;
        private float cd;
        bool warping = false;
        Vector2 pos;
        OutOfBoundsHandler handler;
        public void Awake()
        {
            player = this.GetComponentInParent<Player>();
            cd = 0;
        }
        public void OnPointStart()
        {
            warpsLeft = player.data.stats.GetAdditionalData().maxWarps;
        }
        public void Start()
        {
            OutOfBoundsHandler[] outOfBoundsHandlers = UnityEngine.GameObject.FindObjectsOfType<OutOfBoundsHandler>();
            foreach (OutOfBoundsHandler outOfBounds in outOfBoundsHandlers)
            {
                if (((CharacterData)Traverse.Create(outOfBounds).Field("data").GetValue()).player.playerID == ((CharacterData)Traverse.Create(player).Field("data").GetValue()).player.playerID)
                {
                    handler = outOfBounds;
                    return;
                }
            }
            warpsLeft = player.data.stats.GetAdditionalData().maxWarps;
        }

        public void Update()
        {
            if (handler != null)
                pos = ModdingUtils.Extensions.OutOfBoundsHandlerExtensions.BoundsPointFromWorldPosition(handler, player.data.transform.position);
            else
            {
                OutOfBoundsHandler[] outOfBoundsHandlers = UnityEngine.GameObject.FindObjectsOfType<OutOfBoundsHandler>();
                foreach (OutOfBoundsHandler outOfBounds in outOfBoundsHandlers)
                {
                    if (((CharacterData)Traverse.Create(outOfBounds).Field("data").GetValue()).player.playerID == ((CharacterData)Traverse.Create(player).Field("data").GetValue()).player.playerID)
                    {
                        handler = outOfBounds;
                    }
                }
            }
            if(!warping && player!= null && ModdingUtils.Utils.PlayerStatus.PlayerAliveAndSimulated(player))
            {
                WarpX(pos);
                WarpY(pos);
            }
            if(cd >=0)
                cd -= TimeHandler.deltaTime;
        }
        public void OnEnable()
        {
            warpsLeft = player.data.stats.GetAdditionalData().maxWarps;
        }

        private void WarpX(Vector3 pos)
        {
            if (cd <= 0)
            {
                bool flag = false;
                if (pos.x >= 0.99 || pos.x <= 0.01)
                {
                    flag = true;
                    pos.x = 1 - pos.x;
                }
                if (!flag)
                {
                    warping = false;
                }

                if (handler != null && flag && !warping && warpsLeft > 0 && !Physics2D.OverlapCircle(ModdingUtils.Extensions.OutOfBoundsHandlerExtensions.WorldPositionFromBoundsPoint(handler, pos), 0.15f, (LayerMask)player.GetComponentInParent<CharacterData>().GetFieldValue("groundMask")))
                {
                    if (!warping) { ChaosPoppycarsCardsComplex.Instance.ExecuteAfterSeconds(0.1f, () => { warping = false; warpsLeft--; }); }
                    warping = true;
                    player.GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(3);
                    cd = 0.1f;
                    player.transform.SetXPosition(ModdingUtils.Extensions.OutOfBoundsHandlerExtensions.WorldPositionFromBoundsPoint(handler, pos).x);
                }
            }
        }
        private void WarpY(Vector3 pos)
        {
            if (cd <= 0)
            {
                bool flag = false;
                if (pos.y >= 0.989 || pos.y <= 0.011)
                {
                    flag = true;
                    pos.y = 1 - pos.y;
                }
                if (!flag)
                {
                    warping = false;
                }

                if (handler != null && flag && !warping && warpsLeft > 0 && !Physics2D.OverlapCircle(ModdingUtils.Extensions.OutOfBoundsHandlerExtensions.WorldPositionFromBoundsPoint(handler, pos), 0.15f))
                {
                    if (!warping) { ChaosPoppycarsCardsComplex.Instance.ExecuteAfterSeconds(0.1f, () => { warping = false; warpsLeft--; }); }
                    warping = true;
                    player.GetComponentInParent<PlayerCollision>().IgnoreWallForFrames(3);
                    cd = 0.1f;
                    player.transform.SetYPosition(ModdingUtils.Extensions.OutOfBoundsHandlerExtensions.WorldPositionFromBoundsPoint(handler, pos).y);
                }
            }
        }
    }
}