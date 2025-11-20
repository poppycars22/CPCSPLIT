using Photon.Pun;
using Photon.Realtime;
using Sonigon;
using Sonigon.Internal;
using System;
using UnityEngine;
using UnityEngine.UI;


namespace CPCCommissions.MonoBehaviours
{
    public class WhynackHoming : MonoBehaviour
    {
        [Header("Sound")]
        public SoundEvent soundHomingFound;

        private bool soundHomingCanPlay = true;

        [Header("Settings")]
        public float amount = 1f;

        public float scalingDrag = 1f;

        public float drag = 1f;

        public float spread = 1f;

        private MoveTransform move;

        private bool isOn;

        public RotSpring rot1;

        public RotSpring rot2;

        private FlickerEvent[] flicks;

        private PhotonView view;

        private void Start()
        {
            move = GetComponentInParent<MoveTransform>();
            flicks = GetComponentsInChildren<FlickerEvent>();
            view = GetComponentInParent<PhotonView>();
            GetComponentInParent<SyncProjectile>().active = true;
        }

        private void Update()
        {
            amount = 1.5f; //350
            scalingDrag = 1f; //0.05
            drag = 5f; //5
            spread = 2f; //2
            Player closestPlayer = null;
            //PlayerManager.instance.GetClosestPlayer
            float dist = float.PositiveInfinity;
            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                if (!PlayerManager.instance.players[i].data.dead && PlayerManager.instance.players[i].teamID != this.GetComponentInParent<SpawnedAttack>().spawner.teamID)
                {
                    float num2 = Vector2.Distance(base.transform.position, PlayerManager.instance.players[i].data.playerVel.position);
                    if (PlayerManager.instance.CanSeePlayer(base.transform.position, PlayerManager.instance.players[i]).canSee && num2 < dist)
                    {
                        dist = num2;
                        closestPlayer = PlayerManager.instance.players[i];
                    }
                }
            }

            //Sneaky Logic pt 1
            /*bool flag = false;
            float num3 = 1f;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(base.transform.position, base.transform.forward + base.transform.right * 0.3f, 3, LayerMask.GetMask("Default"));
            if ((bool)raycastHit2D && (bool)raycastHit2D.transform && !raycastHit2D.collider.GetComponent<Damagable>() && raycastHit2D.transform.gameObject.layer != 10)
            {
                flag = true;
            }
            bool flag2 = false;
            RaycastHit2D raycastHit2D2 = Physics2D.Raycast(base.transform.position, base.transform.forward + base.transform.right * (0f - 0.3f), 3, LayerMask.GetMask("Default"));
            if ((bool)raycastHit2D2 && (bool)raycastHit2D2.transform && !raycastHit2D2.collider.GetComponent<Damagable>() && raycastHit2D2.transform.gameObject.layer != 10)
            {
                flag2 = true;
            }
            if (flag && flag2 && raycastHit2D.transform == raycastHit2D2.transform)
            {
                if (raycastHit2D.distance < raycastHit2D2.distance)
                {
                    flag2 = false;
                }
                else
                {
                    flag = false;
                }
            }*/

            //Homing Logic
            if ((bool)closestPlayer)
            {
                Vector3 vector = closestPlayer.transform.position + base.transform.right * move.selectedSpread * Vector3.Distance(base.transform.position, closestPlayer.transform.position) * spread;
                float num = Vector3.Angle(base.transform.root.forward, vector - base.transform.position);
                if (num < 100f) //&& PlayerManager.instance.CanSeePlayer(base.transform.position, closestPlayer).canSee) //70
                {
                    move.velocity -= move.velocity * num * TimeHandler.deltaTime * scalingDrag;
                    //move.velocity -= move.velocity * TimeHandler.deltaTime * drag;
                    move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 1f) * TimeHandler.deltaTime * move.localForce.magnitude * 2f * amount;
                    move.velocity.z = 0f;
                    //move.velocity += Vector3.up * TimeHandler.deltaTime * move.gravity * move.multiplier;
                    if (!isOn)
                    {
                        move.simulateGravity++;
                        if (soundHomingCanPlay)
                        {
                            soundHomingCanPlay = false;
                            SoundManager.Instance.PlayAtPosition(soundHomingFound, SoundManager.Instance.GetTransform(), base.transform);
                        }
                    }
                    isOn = true;
                    for (int i = 0; i < flicks.Length; i++)
                    {
                        flicks[i].isOn = true;
                    }
                    rot1.target = 5f; //10
                    rot2.target = -5f; //-10
                }
                /*else if (num < 100f)
                {
                    if (!flag && !flag2)
                    {
                        move.velocity -= move.velocity * num * TimeHandler.deltaTime * scalingDrag;
                        move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 1f) * TimeHandler.deltaTime * move.localForce.magnitude * 2f * amount;
                        move.velocity.z = 0f;
                    }
                    if(flag)
                    {
                        move.velocity -= move.velocity * num * TimeHandler.deltaTime * scalingDrag;
                        move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 1f) * TimeHandler.deltaTime * move.localForce.magnitude * 2f * amount + (Vector3)raycastHit2D.normal * 20f * move.velocity.magnitude * num3 * TimeHandler.deltaTime;
                        move.velocity.z = 0f;
                    }
                    if(flag2)
                    {
                        move.velocity -= move.velocity * num * TimeHandler.deltaTime * scalingDrag;
                        move.velocity += Vector3.ClampMagnitude(vector - base.transform.position, 1f) * TimeHandler.deltaTime * move.localForce.magnitude * 2f * amount + (Vector3)raycastHit2D2.normal * 20f * move.velocity.magnitude * num3 * TimeHandler.deltaTime;
                        move.velocity.z = 0f;
                    }
                    if (!isOn)
                    {
                        move.simulateGravity++;
                        if (soundHomingCanPlay)
                        {
                            soundHomingCanPlay = false;
                            SoundManager.Instance.PlayAtPosition(soundHomingFound, SoundManager.Instance.GetTransform(), base.transform);
                        }
                    }
                    isOn = true;
                    for (int i = 0; i < flicks.Length; i++)
                    {
                        flicks[i].isOn = true;
                    }
                    rot1.target = 10f;
                    rot2.target = -10f;
                }*/
                else
                {
                    if (isOn)
                    {
                        move.simulateGravity--;
                        soundHomingCanPlay = true;
                    }
                    isOn = false;
                    for (int j = 0; j < flicks.Length; j++)
                    {
                        flicks[j].isOn = false;
                    }
                    rot1.target = 50f; //50
                    rot2.target = -50f; //-50
                }
            }
            else
            {
                if (isOn)
                {
                    move.simulateGravity--;
                    soundHomingCanPlay = true;
                }
                isOn = false;
                for (int k = 0; k < flicks.Length; k++)
                {
                    flicks[k].isOn = false;
                }
                rot1.target = 50f; //50
                rot2.target = -50f; //-50
            }

            //Sneaky Logic prt 2
            /*float magnitude = move.velocity.magnitude;
            if (flag)
            {
                move.velocity += (Vector3)raycastHit2D.normal * 20f * move.velocity.magnitude * num3 * TimeHandler.deltaTime;
            }
            if (flag2)
            {
                move.velocity += (Vector3)raycastHit2D2.normal * 20f * move.velocity.magnitude * num3 * TimeHandler.deltaTime;
            }
            move.velocity = move.velocity.normalized * magnitude;*/
        }
    }

}
