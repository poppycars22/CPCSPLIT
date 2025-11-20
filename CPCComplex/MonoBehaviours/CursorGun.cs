using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using System.Linq;

public class CursorGun : MonoBehaviour
{
        Player player;
        Gun gun;
        float cd = 0f;
        // A list of guns created for this mono saved here.
        // Ideally you'll make a pool of guns for your mod to use.
        public SimulatedGun[] savedGuns = new SimulatedGun[1];
    

    public static GameObject _stopRecursionObj = null;

    public static GameObject StopRecursionObj
    {
        get
        {
            if (_stopRecursionObj == null)
            {
                _stopRecursionObj = new GameObject("A_StopRecursion", typeof(StopRecursion));
                DontDestroyOnLoad(_stopRecursionObj);
            }
            return _stopRecursionObj;
        }
    }

    public static ObjectsToSpawn[] StopRecursionSpawn
    {
        get
        {
            return new ObjectsToSpawn[] { new ObjectsToSpawn() { AddToProjectile = StopRecursionObj } };
        }
    }

    public void Start()
    {
            this.player = this.GetComponentInParent<Player>();
            this.gun = this.player.data.weaponHandler.gun;
            this.gun.ShootPojectileAction += this.OnShootProjectileAction;

            // Checks to see if we have a saved gun already, if not, we make one.
            if (savedGuns[0] == null)
            {
                // We spawn a new object since this allows us manipulate the gun object's position without messing with the player's gameobjest.
                savedGuns[0] = new GameObject("Cursor_One").AddComponent<SimulatedGun>();
            
            }
        
    }
    public void OnShootProjectileAction(GameObject obj)
    {
        if (obj.GetComponentsInChildren<StopRecursion>().Length > 0)
        {
            return;
        }
        SimulatedGun Cursor1 = savedGuns[0];
        Cursor1.CopyGunStatsExceptActions(this.gun);
        Cursor1.CopyAttackAction(this.gun);
        Cursor1.CopyShootProjectileAction(this.gun);
        Cursor1.ShootPojectileAction -= this.OnShootProjectileAction;
        Cursor1.numberOfProjectiles = 1;
        Cursor1.bursts = 0;
        Cursor1.damage *= 0.5f;
        Cursor1.objectsToSpawn = Cursor1.objectsToSpawn.Concat(StopRecursionSpawn).ToArray();

        if (!(player.data.view.IsMine || PhotonNetwork.OfflineMode))
        {
            return;
        }
        if (cd <= 0)
        {
            cd += 0.15f;
            Cursor1.SimulatedAttack(this.player.playerID, new Vector3(MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).x, MainCam.instance.cam.ScreenToWorldPoint(Input.mousePosition).y, 0), new Vector3(player.data.input.aimDirection.x, player.data.input.aimDirection.y, 0), 1f, 1);
        }
    }
    public void Update()
    {
        if (cd >= 0)
        {
            cd -= TimeHandler.deltaTime;
        }
    }
    public void OnDestroy()
        {
            // Remove our action when the mono is removed
            gun.ShootPojectileAction -= OnShootProjectileAction;
        UnityEngine.GameObject.Destroy(savedGuns[0]);
    }
    }
