using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using ModdingUtils.Utils;


namespace CPCComplex.MonoBehaviours
{
    class CenterPullMono : MonoBehaviour
    {
        Player player;
        public void Start()
        {
            player = GetComponentInParent<Player>();
        }
        public void Update()
        {
                if (PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    Vector2 center = new Vector2(0, 0);
                    Vector2 playerpos = player.gameObject.transform.position;
                    Vector2 pull = center - playerpos;
                    if (Vector2.Distance(center, playerpos) >= 1)
                        player.GetComponent<PlayerVelocity>().SetFieldValue("velocity", (Vector2)player.GetComponent<PlayerVelocity>().GetFieldValue("velocity") + (pull * 1/(Vector2.Distance(center, playerpos)/1.5f)));
                    else
                        player.GetComponent<PlayerVelocity>().SetFieldValue("velocity", (Vector2)player.GetComponent<PlayerVelocity>().GetFieldValue("velocity") + (pull));
                    if (Vector2.Distance(center, playerpos) <= 6)
                        player.data.sinceGrounded = 0;
                }
        }
        public void Remove()
        {
        }
    }
}