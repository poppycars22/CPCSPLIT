using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;


namespace CPCCore.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(PlayerVelocity), "AddForce", new Type[2] {typeof(Vector2), typeof(ForceMode2D)})]
    class ForcePatch
    {
        // patch for funnies
        private static void Prefix(PlayerVelocity __instance, ref Vector2 force, ForceMode2D forceMode)
        {
            if (__instance.data.stats.GetAdditionalData().rotatedForce)
            {
                float tempY = force.y;
                float tempX = force.x;
                force = new Vector2(tempY, tempX);
            }

        }
    }

    [Serializable]
    [HarmonyPatch(typeof(PlayerVelocity), "FixedUpdate")]
    class ForcePatch2
    {
        // patch for funnies
        private static bool Prefix(PlayerVelocity __instance)
        {
            if (__instance.data.stats.GetAdditionalData().cursorFear != 0)
            {
                if (__instance.data.isPlaying)
                {
                    if (__instance.isKinematic)
                    {
                        __instance.velocity *= 0f;
                    }

                    if (__instance.simulated && !__instance.isKinematic)
                    {
                        __instance.velocity += /*Vector2.down*/(Vector2)__instance.data.aimDirection * TimeHandler.fixedDeltaTime * TimeHandler.timeScale * 20f * (__instance.data.stats.GetAdditionalData().cursorFear*-1);
                        __instance.data.gameObject.transform.position += TimeHandler.fixedDeltaTime * TimeHandler.timeScale * (Vector3)__instance.velocity;
                        __instance.data.gameObject.transform.position = new Vector3(__instance.data.gameObject.transform.position.x, __instance.data.gameObject.transform.position.y, 0f);
                    }
                }
                return false;
            }
            return true;
        }
    }
}