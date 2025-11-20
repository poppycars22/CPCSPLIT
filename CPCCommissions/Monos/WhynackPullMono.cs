using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnboundLib;
using ModdingUtils.MonoBehaviours;
using ModdingUtils.Utils;


namespace CPCCommissions.MonoBehaviours
{
    class WhynackPullMono : MonoBehaviour
    {
        Player player;
        Vector2 center = new Vector2(0,0);
        float strength = 1;
        public void Start()
        {
            player = GetComponentInParent<Player>();
            if (player != null)
            {
                strength = player.data.maxHealth/250f;
                float time = GetComponent<SpawnedAttack>().spawner.data.weaponHandler.gun.damage *2f;
                center = this.gameObject.transform.position;
                Destroy(this.gameObject, time);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void Update()
        {
            if (player != null)
            {
                if (PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    Vector2 playerpos = player.gameObject.transform.position;
                    Vector2 pull = center - playerpos;
                    if (Vector2.Distance(center, playerpos) >= 1)
                        player.GetComponent<PlayerVelocity>().SetFieldValue("velocity", (Vector2)player.GetComponent<PlayerVelocity>().GetFieldValue("velocity") + (pull * 1 / (Vector2.Distance(center, playerpos) / 1.5f) * strength));
                    else
                        player.GetComponent<PlayerVelocity>().SetFieldValue("velocity", (Vector2)player.GetComponent<PlayerVelocity>().GetFieldValue("velocity") + (pull * strength));
                    if (Vector2.Distance(center, playerpos) <= 6)
                        player.data.sinceGrounded = 0;
                }
                else
                {
                    Destroy(this.gameObject);
                }
            }
        }
        public void Remove()
        {
        }
    }
}