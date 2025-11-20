using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using System.Linq;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using CPCCore.Utilities;
using CPCCore.MonoBehaviours;

namespace CPCComplex.MonoBehaviours
{
    public class BlockBullets : MonoBehaviour
    {
        Block block;
        Player player;
        Gun gun;
        float cd = 0;
        private PhotonView photonView;
        GameObject test;
        public SimulatedGun[] savedGuns = new SimulatedGun[1];


        public void OnDestroy()
        {
            block.BlockAction -= OnBlock;
            UnityEngine.GameObject.Destroy(savedGuns[0]);
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (cd <= 0)
            {
                test = Object.Instantiate(ChaosPoppycarsCardsComplex.Bundle.LoadAsset<GameObject>("TestObj"));
                test.AddComponent<Destroyer>();
                SimulatedGun BlockGun = savedGuns[0];
                BlockGun.CopyGunStatsExceptActions(this.gun);
                BlockGun.CopyAttackAction(this.gun);
                BlockGun.CopyShootProjectileAction(this.gun);
                BlockGun.objectsToSpawn = BlockGun.objectsToSpawn.Concat(StopRecursionMono.StopRecursionSpawn).ToArray();
                BlockGun.bursts = 3;
                BlockGun.numberOfProjectiles = 1;
                BlockGun.timeBetweenBullets = 0.15f;
                BlockGun.damage /= 2.5f;
                Vector3 temp = player.transform.position;
                Vector3 aim = new Vector3(player.data.input.aimDirection.x, player.data.input.aimDirection.y, 0);
                test.transform.position = temp;
                Vector3 Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - test.transform.position;
                float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 45;
                test.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                test.GetComponent<SpriteRenderer>().color = player.GetTeamColors().color;
                if (player.data.view.IsMine)
                {
                    photonView.RPC("SyncDir", RpcTarget.Others, angle);
                }
                ChaosPoppycarsCardsComplex.Instance.ExecuteAfterSeconds(1.5f, () =>
                {
                    BlockGun.SimulatedAttack(this.player.playerID, temp, aim, 1f, 1);
                    //Destroy(test, 1.5f);
                });
                cd += 1.5f;
            }
        }
        public void Start()
        {
            photonView = GetComponent<PhotonView>();
            this.player = gameObject.GetComponent<Player>();
            this.gun = this.player.data.weaponHandler.gun;
            this.block = this.player.GetComponent<Block>();
            if (savedGuns[0] == null)
            {
                savedGuns[0] = new GameObject("BlockBullets").AddComponent<SimulatedGun>();
            }
            block.BlockAction += OnBlock;
        }
        public void Update()
        {
            if (cd > 0)
                cd -= TimeHandler.deltaTime;
        }
        [PunRPC]
        public void SyncDir(float angle)
        {
            if (test!=null)
                test.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
