using Photon.Pun;
using Photon.Realtime;
using Sonigon;
using System;
using System.ComponentModel;
using UnityEngine;


namespace CPCCommissions.MonoBehaviours
{
    public class Accelerate : MonoBehaviour
    {
        private MoveTransform move;
        private RayCastTrail rayCast;
        private TrailRenderer componentInChildren;
        private float sizeChange = 0.5f;

        private void Start()
        {
            move = GetComponentInParent<MoveTransform>();
            rayCast = GetComponentInParent<RayCastTrail>();
            componentInChildren = rayCast.gameObject.GetComponentInChildren<TrailRenderer>(false);
        }

        private void FixedUpdate()
        {
            move.velocity += move.velocity * 1.05f * TimeHandler.fixedDeltaTime;
            rayCast.size += Mathf.Pow(sizeChange, 0.85f) * TimeHandler.fixedDeltaTime;
            if (componentInChildren!=null)
            {
                componentInChildren.widthMultiplier += (1f + sizeChange/55f) * TimeHandler.fixedDeltaTime;
            }
        }
    }

}
