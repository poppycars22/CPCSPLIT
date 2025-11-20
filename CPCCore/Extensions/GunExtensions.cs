using System;
using System.Runtime.CompilerServices;
using CPCCardInfostuffs;
using HarmonyLib;
using UnityEngine;

namespace CPCCore.Extensions
{

    // ADD FIELDS TO GUN
    [Serializable]
    public class GunAdditionalData
    {

        public float criticalHitChance1 = 0f;
        public float criticalHitDamage1 = 1f;
        public int criticalHitBounces = 0;
        public float criticalHitDamageOnBounce = 0f;
        public bool consecutiveCrits = false;
        public bool guranteedCrits = false;
        public float consecutiveCritsDamage = 0f;
        public float CritSlow = 0f;
        public Color CritColor = Color.red;
        public Color DoubleCritColor = Color.cyan;
        public float criticalBulletSpeed = 1f;
        public float criticalSimulationSpeed = 1f;
        public bool unblockableCrits = false;
        public float criticalHeal = 0f;
        public float criticalBlockCDReduction = 0f;
        public bool BlockingCrits = false;


        public GunAdditionalData()
        {

            criticalHitChance1 = 0f;

            criticalHitDamage1 = 1f;

            criticalHitBounces = 0;

            criticalHitDamageOnBounce = 0f;

            consecutiveCrits = false;

            guranteedCrits = false;

            consecutiveCritsDamage = 0f;

            CritSlow = 0f;

            CritColor = Color.red;

            DoubleCritColor = Color.cyan;

            criticalBulletSpeed = 1f;

            criticalSimulationSpeed = 1f;

            unblockableCrits = false;

            criticalHeal = 0f;

            criticalBlockCDReduction = 0f;

            BlockingCrits = false;

        }
    }
    public static class GunExtension
    {
        public static readonly ConditionalWeakTable<Gun, GunAdditionalData> data = new ConditionalWeakTable<Gun, GunAdditionalData>();

        public static GunAdditionalData GetAdditionalData(this Gun gun)
        {
         
            return data.GetOrCreateValue(gun);
        }

        public static void AddData(this Gun gun, GunAdditionalData value)
        {
          
            try
            {
                data.Add(gun, value);
             
            }
            catch (Exception) {  }
        }
       

    }
    // reset extra gun attributes when resetstats is called
    [HarmonyPatch(typeof(Gun), "ResetStats")]
    class GunPatchResetStats
    {
        private static void Prefix(Gun __instance)
        {

            __instance.GetAdditionalData().criticalHitChance1 = 0f;

            __instance.GetAdditionalData().criticalHitDamage1 = 1f;

            __instance.GetAdditionalData().criticalHitBounces = 0;

            __instance.GetAdditionalData().criticalHitDamageOnBounce = 0f;

            __instance.GetAdditionalData().consecutiveCrits = false;

            __instance.GetAdditionalData().consecutiveCritsDamage = 0f;

            __instance.GetAdditionalData().guranteedCrits = false;

            __instance.GetAdditionalData().CritSlow = 0f;

            __instance.GetAdditionalData().CritColor = Color.red;

            __instance.GetAdditionalData().DoubleCritColor = Color.cyan;

            __instance.GetAdditionalData().criticalBulletSpeed = 1f;

            __instance.GetAdditionalData().criticalSimulationSpeed = 1f;

            __instance.GetAdditionalData().unblockableCrits = false;

            __instance.GetAdditionalData().criticalHeal = 0f;

            __instance.GetAdditionalData().criticalBlockCDReduction = 0f;

            __instance.GetAdditionalData().BlockingCrits = false;
        }
    }
    [HarmonyPatch(typeof(ApplyCardStats), "ApplyStats")]

    internal class ApplyCardStatsPatch
    {
        private static void Postfix(ApplyCardStats __instance, Player ___playerToUpgrade)
        {
            var THINGaa = __instance.GetComponent<CPCCardInfoForExtraStats>();

           
            if (THINGaa != null)
            {


                if (___playerToUpgrade.data.weaponHandler.gun != null)
                {

                    // UnityEngine.Debug.Log($"[ criticalHitDamage {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1}, criticalHitChance {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1} ]");
                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1 += THINGaa.GunCritDamage2;

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1 += THINGaa.GunCritChance2;

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitBounces += THINGaa.GunCritBounces;

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamageOnBounce += THINGaa.GunCritDamageOnBounce;

                    if (___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().consecutiveCrits == false)
                    {
                        ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().consecutiveCrits = THINGaa.GunConsecutiveCrits;
                    }

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().consecutiveCritsDamage += THINGaa.GunConsecutiveCritsDamage;

                    if (___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().guranteedCrits == false)
                    {
                        ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().guranteedCrits = THINGaa.GunGuranteedCrits;
                    }

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().CritSlow += THINGaa.GunCritSlow;

                    if (THINGaa.GunCritColor != Color.red & ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().CritColor != Color.clear)
                    {
                        ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().CritColor = THINGaa.GunCritColor;
                    }

                    if (THINGaa.GunDoubleCritColor != Color.cyan & ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().CritColor != Color.clear)
                    {
                        ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().DoubleCritColor = THINGaa.GunDoubleCritColor;
                    }

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalBulletSpeed += THINGaa.GunCritBulletSpeed;

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalSimulationSpeed += THINGaa.GunCritSimulationSpeed;

                    if (___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().unblockableCrits == false)
                    {
                        ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().unblockableCrits = THINGaa.GunUnblockableCrits;
                    }

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHeal += THINGaa.GunCritHeal;

                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalBlockCDReduction += THINGaa.GunCritBlockCDReduction;

                    if (___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().BlockingCrits == false)
                    {
                        ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().BlockingCrits = THINGaa.GunBlockingCrits;
                    }
                    //UnityEngine.Debug.Log($"[crit chance {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1}, crit damage {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1} ]");
                }
            }
        }
    }
}