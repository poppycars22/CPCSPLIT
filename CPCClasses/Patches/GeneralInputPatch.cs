using System;
using HarmonyLib;
using CPCClasses.MonoBehaviours;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;

namespace CPCClasses.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(GeneralInput), "Update")]
    class GeneralInputUpdatePatch
    {
        // patch for forced move
        private static void Postfix(GeneralInput __instance)
        {
            if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().forcedMoveEnabled)
            { 
                if(Mathf.Abs(__instance.direction.x) <0.5f)
                {
                    __instance.direction = new Vector3(__instance.GetComponent<Player>().data.stats.GetAdditionalData().forcedMove, __instance.direction.y);
                }
                else
                {
                    __instance.GetComponent<Player>().data.stats.GetAdditionalData().forcedMove = __instance.direction.x;
                }
            }

            if (Mathf.Abs(__instance.direction.x) > 0.5f)
            {
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().speedyHands)
                    __instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<SpeedyHandsMono>();
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().triggerFinger)
                    __instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<TriggerFingerMono>();
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().acceleratedRejuvenation)
                    __instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<AcceleratedRejuvenationMono>();
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().boostedBlock)
                    __instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<BoostedBlockMono>();
            }
            else
            {
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().speedyHands)
                    UnityEngine.GameObject.Destroy(__instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<SpeedyHandsMono>());
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().triggerFinger)
                    UnityEngine.GameObject.Destroy(__instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<TriggerFingerMono>());
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().acceleratedRejuvenation)
                    UnityEngine.GameObject.Destroy(__instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<AcceleratedRejuvenationMono>());
                if (__instance.GetComponent<Player>().data.stats.GetAdditionalData().boostedBlock)
                    UnityEngine.GameObject.Destroy(__instance.GetComponent<Player>().transform.gameObject.GetOrAddComponent<BoostedBlockMono>());
            }
        }
    }
}