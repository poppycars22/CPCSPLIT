using Photon.Pun;
using Photon.Realtime;
using Sonigon;
using System;
using System.ComponentModel;
using UnboundLib;
using UnityEngine;


namespace CPCCommissions.MonoBehaviours
{
    public class PongMono : MonoBehaviour
    {
        ProjectileHit bullet;
        RayHitReflect bul;
        void Start()
        {
            bullet = GetComponentInParent<ProjectileHit>();
            bul = bullet.GetComponent<RayHitReflect>();
            bullet.GetComponent<RemoveAfterSeconds>().enabled = false;
        }
        void Update()
        {
            if(bul != null)
            {
                bul.reflects = 100;
                bullet.GetComponent<RemoveAfterSeconds>().enabled = false;
            }
            else
            {
                if (bullet == null)
                    bullet = GetComponentInParent<ProjectileHit>();
                bul = bullet.gameObject.GetComponent<RayHitReflect>();
            }
        }
    }

}
