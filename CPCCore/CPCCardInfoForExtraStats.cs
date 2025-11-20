using System.Linq;
using Nullmanager;
using TMPro;
using UnityEngine;
using UnboundLib;
using CPCCore.Extensions;
using HarmonyLib;


namespace CPCCardInfostuffs
{
    public class CPCCardInfoForExtraStats : MonoBehaviour
    {
        [Header("CPC Settings")]
        //public bool Nullable = true;
        public float GunCritDamage2 = 0f;
        public float GunCritChance2 = 0f;
        public int GunCritBounces = 0;
        public float GunCritDamageOnBounce = 0f;
        public bool GunConsecutiveCrits = false;
        public float GunConsecutiveCritsDamage = 0f;
        public bool GunGuranteedCrits = false;
        public float GunCritSlow = 0f;
        public Color GunCritColor = Color.red;
        public Color GunDoubleCritColor = Color.cyan;
        public float GunCritBulletSpeed = 0f;
        public float GunCritSimulationSpeed = 0f;
        public bool GunUnblockableCrits = false;
        public float GunCritHeal = 0f;
        public float GunCritBlockCDReduction = 0f;
        public bool GunBlockingCrits = false;
        //public bool NeedsNull = false; 



        /*public void Setup()
        {
            if (!Nullable)
                GetComponent<CardInfo>().MarkUnNullable();
            if (NeedsNull)
                GetComponent<CardInfo>().NeedsNull();
        }*/
    }
    
    
   
}
