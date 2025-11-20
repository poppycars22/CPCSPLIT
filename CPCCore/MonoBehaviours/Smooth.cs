using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace CPCCore.MonoBehaviours
{
    public class Smooth : MonoBehaviour
    {
        public Vector3 currentPos;
        public Vector3 targetPos;

        public float speed;
        private float sinTime;
        void Start()
        {
            sinTime = 0;
        }
        void Update() 
        {
            if (this.currentPos != targetPos)
            {
                sinTime += TimeHandler.deltaTime * speed;
                sinTime = Mathf.Clamp(sinTime, 0, Mathf.PI);
                transform.position = Vector3.Lerp(currentPos, targetPos, 0.5f * Mathf.Sin(sinTime - Mathf.PI / 2) + 0.5f);
                Destroy(this, speed + 0.1f);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
