using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Photon.Realtime;
using PlayerTimeScale;
using UnityEngine;

namespace CPCCommissions.Extensions
{
    // ADD FIELDS TO GUN
    [Serializable]
    public class CharacterStatModifiersAdditionalDataCPCCom
    {
        public bool rngDmg;
        public bool splitDmg;
        public CharacterStatModifiersAdditionalDataCPCCom()
        {
            rngDmg = false;
            splitDmg = false;
        }
    }
    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalDataCPCCom> data = new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalDataCPCCom>();

        public static CharacterStatModifiersAdditionalDataCPCCom GetAdditionalDataCPCCom(this CharacterStatModifiers statModifiers)
        {
            var a = data.GetOrCreateValue(statModifiers);
            return a;
        }

        public static void AddData(this CharacterStatModifiers statModifiers, CharacterStatModifiersAdditionalDataCPCCom value)
        {
            try
            {
                data.Add(statModifiers, value);
            }
            catch (Exception) { }
        }
    }
    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    class CharacterStatModifiersPatchResetStats
    {
        private static void Prefix(CharacterStatModifiers __instance)
        {
            __instance.GetAdditionalDataCPCCom().rngDmg = false;
            __instance.GetAdditionalDataCPCCom().splitDmg = false;
        }
    }
}