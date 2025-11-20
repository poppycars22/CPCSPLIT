using ModdingUtils.GameModes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{
    public class LaserController : MonoBehaviour, IPointStartHookHandler
    {
        public float damage = 0;
        public Player player;
        public float timeBetweenLasers = 0.75f;
        public float timer = 0;
        public float laserSpeed = 1.5f;
        public float timer2 = 6;
        // Start is called before the first frame update
        void Start()
        {
            player = gameObject.GetComponentInParent<Player>();
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
        }
        public void OnPointStart()
        {
            laserSpeed = 1.5f;
            timeBetweenLasers = 0.75f;
            timer2 = 6f;
        }
        // Update is called once per frame
        void Update()
        {
            if (player.data.view.IsMine)
            {
                if (timer <= 0 && ModdingUtils.Utils.PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    timer = timeBetweenLasers + Random.Range(-0.25f, 0.25f);
                    GameObject laser = Instantiate(ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("LaserObj"), new Vector2(Random.Range(Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x, Camera.main.ViewportToWorldPoint(Vector2.right).x), Camera.main.ViewportToWorldPoint(Vector2.up).y), base.transform.rotation);
                    LaserBehaviour laserBehaviour = laser.GetComponent<LaserBehaviour>();
                    if (damage != 0)
                        laserBehaviour.damage = damage;
                    else
                        laserBehaviour.damage = player.data.maxHealth * 0.33f;
                    laserBehaviour.player = player;
                    laserBehaviour.speed = laserSpeed;
                }
                timer -= TimeHandler.deltaTime;
                if (timer2 <= 0 && ModdingUtils.Utils.PlayerStatus.PlayerAliveAndSimulated(player))
                {
                    if (laserSpeed > 0.25f)
                        laserSpeed -= 0.05f;
                    if (timeBetweenLasers > 0.25f *0.75f)
                        timeBetweenLasers -= 0.05f *0.75f;
                    timer2 = 4f;
                }
                timer2 -= TimeHandler.deltaTime;
            }
        }
    }
}
