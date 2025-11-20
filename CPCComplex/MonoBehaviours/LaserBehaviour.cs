using Photon.Pun;
using RWF.Patches;
using System.Collections;
using System.Collections.Generic;
using UnboundLib;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{
    public class LaserBehaviour : MonoBehaviour
    {
        public SpriteRenderer sprite;
        public float damage;
        public Player player;
        public bool unExecuted = false;
        public bool col = true;
        public float speed = 1.5f;
        // Start is called before the first frame update
        void Start()
        {
            Color[] color =
            {
                new Color(0.75f, 0, 0),//Color.red,
                new Color(0, 0.75f, 0),//Color.green,
                new Color(0,0,0.75f),//Color.blue,
                new Color(0.75f, 0.7f, 0),//Color.yellow,
                new Color(0, 0.75f, 0.75f),//Color.cyan,
                new Color(0.75f, 0, 0.75f)//Color.magenta,
            };
            sprite.color = color[Random.Range(0,6)];
            //sprite.color = Color.red;
        }

        // Update is called once per frame
        void Update()
        {
            //scale the lazers
            this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y + 0.01f, this.gameObject.transform.localScale.z);
            //box.size.Set(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
            if (!unExecuted)
            {
                unExecuted = true;
                ChaosPoppycarsCardsComplex.Instance.ExecuteAfterSeconds(speed, () =>
                {
                    if (this.gameObject != null)
                    {
                        this.gameObject.transform.localScale = new Vector3(this.gameObject.transform.localScale.x, Screen.height + 18f, this.gameObject.transform.localScale.z);
                        //box.size.Set(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y);
                        Destroy(this.gameObject, speed);
                    }
                });
            }
            //UnityEngine.Debug.Log(player.playerID + " AAAAA ");
            Collider2D[] plays = null;
            if (col)
                plays = Physics2D.OverlapBoxAll(this.gameObject.transform.position, this.gameObject.transform.localScale, 0, LayerMask.GetMask("Player"));
            if(plays != null)
            {
                foreach (Collider2D play in plays)
                {
                    if (play.gameObject.GetComponent<Player>() != null && play.gameObject.GetComponent<Player>().playerID == player.playerID)
                    {
                        Player playT = play.gameObject.GetComponent<Player>();
                        if (playT.data.view.IsMine)
                        {
                            col = false;
                            playT.GetComponent<HealthHandler>().CallTakeDamage(new Vector2(0, damage), this.transform.position, null, null, true);
                            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.25f);
                            PhotonView photonView = playT.GetComponent<PhotonView>();
                            playT.gameObject.GetOrAddComponent<RPCMono>();
                            photonView.RPC("RPCASyncBlockThingy", RpcTarget.All, playT.playerID);
                        }
                    }
                }
            }

        }
    }
}
