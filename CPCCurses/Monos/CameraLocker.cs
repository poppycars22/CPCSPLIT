using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Utils;
using System;
using System.Collections.Generic;
using Photon.Pun;

namespace CPCCurses.MonoBehaviours
{
    internal class CameraLocker : MonoBehaviour
    {
        Player player;
        public void Start()
        {
            player = this.GetComponent<Player>();
        }
        public void Update()
        {
            foreach (Camera camera in Camera.allCameras)
            {
                if ((PhotonNetwork.OfflineMode || player.data.view.IsMine))
                    camera.gameObject.transform.eulerAngles = new Vector3(Camera.main.gameObject.transform.rotation.eulerAngles.x, Camera.main.gameObject.transform.rotation.eulerAngles.y, 180f);
            }
        }
    }
}
