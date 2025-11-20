using System;
using HarmonyLib;
using CPCCore.Extensions;
using MapEmbiggener.Patches;
using System.Linq;
using UnityEngine;
using MapEmbiggener.Controllers;
using Photon.Pun;


namespace CPCCore.Patches
{
    
    public class CameraZoomHandlerPatchPatch
    {
        public static void Patch()
        {
            ChaosPoppycarsCardsCore.harmony.Patch(typeof(CameraZoomHandler_Patch_Update).GetMethod("Postfix", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic),
                postfix: new HarmonyMethod(typeof(CameraZoomHandlerPatchPatch).GetMethod(nameof(Postfix), System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)));
        }
        // patch for camera stuffs
        public static void Postfix(object[] __args)
        {
            Camera[] ___cameras = (Camera[])__args[0];
            if (PlayerManager.instance != null && PlayerManager.instance.players.Any(player => player.data.stats.GetAdditionalData().cameraLock && (player.data.view.IsMine || PhotonNetwork.OfflineMode)))
            {
                for (int i = 0; i < ___cameras.Length; i++)
                {
                    ___cameras[i].transform.eulerAngles = new Vector3(___cameras[i].transform.rotation.eulerAngles.x, ___cameras[i].transform.rotation.eulerAngles.y, 180f);
                }
            }
        }
    }
}