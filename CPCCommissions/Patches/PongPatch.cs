using CPCCommissions.Extensions;
using CPCCommissions.MonoBehaviours;
using HarmonyLib;
using Photon.Pun;
using SimulationChamber;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnboundLib;
using UnityEngine;

namespace CPCCommissions.Patches
{
    [Serializable]
    [HarmonyPatch()]
    class PongPatch
    {
        // patch for forced move
        [HarmonyPatch(typeof(WeaponHandler), "Update")]
        private static void Postfix(WeaponHandler __instance)
        {
            CharacterData data = __instance.data;
            if (data.stats.GetAdditionalDataCPCCom().pong)
            {
                __instance.gun.numberOfProjectiles = 1;
                __instance.gun.bursts = 0;
                __instance.gun.destroyBulletAfter = 0;
                __instance.gun.reflects = 100;
            }
        }
        public static bool PongFunc(Gun gun)
        {
            Player player = gun.player;
            if(player != null && player.data.stats.GetAdditionalDataCPCCom().pong && gun.CheckIsMine())
            {
                PongMono[] bullets = (PongMono[])GameObject.FindObjectsOfType(typeof(PongMono));
                bullets = bullets.Where(b => b.GetComponentInParent<ProjectileHit>().ownPlayer.playerID == player.playerID).ToArray();
                return bullets.Count() == 0;
            }
            else
            {
                return gun.CheckIsMine();
            }
        }
        [HarmonyPatch(typeof(Gun), nameof(Gun.FireBurst), MethodType.Enumerator)]
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var code = instructions.ToList();
            var checkIsMine = typeof(Gun).GetMethod(nameof(Gun.CheckIsMine), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            for (int i = 0; i < code.Count; i++)
            {
                if (code[i].opcode == OpCodes.Call && code[i].Calls(checkIsMine))
                {
                    code[i] = CodeInstruction.Call(typeof(PongPatch), nameof(PongFunc));
                    break;
                }
            }
            return code;
        }
    }
}