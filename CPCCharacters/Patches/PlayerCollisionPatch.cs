using System;
using HarmonyLib;
using UnityEngine;
using CPCCore.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;
using ModdingUtils.Utils;
using UnityScript.Lang;
using UnityEngine.UI;
using CPCCharacters.MonoBehaviours;

namespace CPCCharacters.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(PlayerCollision), "FixedUpdate")]
    class PlayerCollisionPatch
    {
        // patch for Whynack Uppercut
        private static void Prefix(PlayerCollision __instance)
        {
            CharacterData data = __instance.data;
            float num = __instance.cirCol.radius * data.gameObject.transform.localScale.x;
            float num2 = __instance.cirCol.radius * data.gameObject.transform.localScale.x * 0.75f;
            RaycastHit2D[] array2 = Physics2D.CircleCastAll(__instance.lastPos, num, (Vector2)data.gameObject.transform.position - __instance.lastPos, Vector2.Distance(data.gameObject.transform.position, __instance.lastPos), __instance.mask);
            for (int j = 0; j < array2.Length; j++)
            {
                if (array2[j].transform.root == data.gameObject.transform.root)
                {
                    continue;
                }
                NetworkPhysicsObject component = array2[j].transform.GetComponent<NetworkPhysicsObject>();
                Player componentInParent = array2[j].transform.GetComponentInParent<Player>();
                if (componentInParent != null && data.transform.gameObject.GetOrAddComponent<DamageCD>().duration <= 0f && data.stats.GetAdditionalData().whynackUpper)
                {
                    if (data.player.GetComponent<PlayerVelocity>().velocity.y > 75f && (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode))
                    {
                        //componentInParent.data.view.RPC("RPCA_SendTakeDamage", RpcTarget.All, new Vector2(data.player.GetComponent<PlayerVelocity>().velocity.y + (componentInParent.data.maxHealth / 25f), 0), data.transform.position, true, data.player.playerID);
                        componentInParent.GetComponent<HealthHandler>().DoDamage(new Vector2(data.player.GetComponent<PlayerVelocity>().velocity.y*0.85f, 0), data.transform.position, Color.red, null, data.player, true, true, false);
                        data.transform.gameObject.GetOrAddComponent<DamageCD>().duration = 0.25f;
                    }
                }
            }
        }
    }

}