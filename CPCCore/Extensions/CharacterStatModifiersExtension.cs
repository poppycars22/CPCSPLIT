using System;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Photon.Realtime;
using PlayerTimeScale;
using UnityEngine;

namespace CPCCore.Extensions
{
    // ADD FIELDS TO GUN
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {

        public float RainbowLeafHealth;
        public float DamageAmpDamage;
        public float HealthBouncesBounced;
        public bool useNewRespawnTime;
        public float newRespawnTime;
        public int GeeseSwarms;
        public int StunningStares;
        public int NanoMachines;
        public int remainingTotems;
        public int totems;
        public int Redstone;
        public int Glowstone;
        public bool InvisPot;
        public bool healthCase;
        public bool blockCase;
        public bool everyOther;
        public bool firstHit;
        public float damageMult;
        public float damageMultMax;
        public bool firstDamage;
        public bool reducingDmg;
        public float reducingDmgAmt;
        public float firstHitdmgReduction;
        public bool damagingBullet;
        public int dashes;
        public bool blockMover;
        public bool blockPush;
        public float blockMoveStrength;
        public float forcedMove;
        public bool forcedMoveEnabled;
        public bool speedyHands;
        public bool triggerFinger;
        public bool acceleratedRejuvenation;
        public bool boostedBlock;
        public int maxWarps;
        public int shroomsAmt;
        public bool hasTrident;
        public bool whynackBlockForce;
        public bool whynackAd;
        public bool whynackHarmony;
        public bool whynackMeditating;
        public bool rotatedForce;
        public bool cameraLock;
        public bool whynackUpper;
        public float rage;
        public float blockPierce;
        public float upwardsKnockback;
        public float cursorFear;
        public bool useAmmo;
        public bool Mcnally;
        public float ExtraHeal;
        //public int CommonShufflePerPick;
        public bool BlackHole;
        public bool WhiteHole;
        public float mapSizeI;
        //public int ShufflesPerPick;
        //public int shuffles;
        //public bool storeDamage;
        //public Vector2 storedDamage;
        //public bool takeStoredDamage;
        public CharacterStatModifiersAdditionalData()
        {
         //   RainbowLeafHealth = 0f;
            HealthBouncesBounced = 0f;
            useNewRespawnTime = false;
            newRespawnTime = 0f;
            GeeseSwarms = 0;
            StunningStares = 0;
            NanoMachines = 0;
            remainingTotems = 0;
            totems = 0;
            Redstone = 0;
            Glowstone = 0;
            InvisPot = false;
            healthCase = false;
            blockCase = false;
            everyOther = true;
            firstHit = true;
            firstHitdmgReduction = 1f;
            firstDamage = true;
            reducingDmg = false;
            damageMult = 1f;
            damageMultMax = 1f;
            reducingDmgAmt = 0f;
            damagingBullet = false;
            dashes = 0;
            blockMover = false;
            blockPush = false;
            blockMoveStrength = 0;
            forcedMove = 0;
            forcedMoveEnabled = false;
            speedyHands = false;
            triggerFinger = false;
            acceleratedRejuvenation = false;
            boostedBlock = false;
            maxWarps = 0;
            shroomsAmt = 0;
            hasTrident = false;
            whynackBlockForce = false;
            whynackAd = false;
            whynackHarmony = false;
            whynackMeditating = false;
            rotatedForce = false;
            cameraLock = false;
            whynackUpper = false;
            rage = 0f;
            blockPierce = 0f;
            upwardsKnockback = 0f;
            cursorFear = 0f;
            useAmmo = true;
            Mcnally = false;
            //shuffles = 0;
            //ShufflesPerPick = 0;
            ExtraHeal = 1f;
            BlackHole = false;
            WhiteHole = false;
            mapSizeI = 0f;
            //CommonShufflePerPick = 0;
            //storeDamage = true;
            //storedDamage = Vector2.zero;
            //takeStoredDamage = false;
        }
    }
    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data = new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers statModifiers)
        {
            var a = data.GetOrCreateValue(statModifiers);
            return a;
        }

        public static void AddData(this CharacterStatModifiers statModifiers, CharacterStatModifiersAdditionalData value)
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
            //__instance.GetAdditionalData().RainbowLeafHealth = 0f;
            __instance.GetAdditionalData().HealthBouncesBounced = 0f;
            __instance.GetAdditionalData().useNewRespawnTime = false;
            __instance.GetAdditionalData().newRespawnTime = 0f;
            __instance.GetAdditionalData().GeeseSwarms = 0;
            __instance.GetAdditionalData().StunningStares = 0;
            __instance.GetAdditionalData().NanoMachines = 0;
            __instance.GetAdditionalData().remainingTotems = 0;
            __instance.GetAdditionalData().totems = 0;
            __instance.GetAdditionalData().Redstone = 0;
            __instance.GetAdditionalData().Glowstone = 0;
            __instance.GetAdditionalData().InvisPot = false;
            __instance.GetAdditionalData().healthCase = false;
            __instance.GetAdditionalData().blockCase = false;
            __instance.GetAdditionalData().everyOther = true;
            __instance.GetAdditionalData().firstHit = true;
            __instance.GetAdditionalData().firstHitdmgReduction = 1f;
            __instance.GetAdditionalData().firstDamage = true;
            __instance.GetAdditionalData().reducingDmg = false;
            __instance.GetAdditionalData().damageMult = 1f;
            __instance.GetAdditionalData().damageMultMax = 1f;
            __instance.GetAdditionalData().reducingDmgAmt = 0f;
            __instance.GetAdditionalData().damagingBullet = false;
            __instance.GetAdditionalData().dashes = 0;
            __instance.GetAdditionalData().blockMover = false;
            __instance.GetAdditionalData().blockPush = false;
            __instance.GetAdditionalData().blockMoveStrength = 0f;
            __instance.GetAdditionalData().forcedMove = 0f;
            __instance.GetAdditionalData().forcedMoveEnabled = false;
            __instance.GetAdditionalData().speedyHands = false;
            __instance.GetAdditionalData().triggerFinger = false;
            __instance.GetAdditionalData().acceleratedRejuvenation = false;
            __instance.GetAdditionalData().boostedBlock = false;
            __instance.GetAdditionalData().maxWarps = 0;
            __instance.GetAdditionalData().shroomsAmt = 0;
            __instance.GetAdditionalData().hasTrident = false;
            __instance.GetAdditionalData().whynackBlockForce = false;
            __instance.GetAdditionalData().whynackAd = false;
            __instance.GetAdditionalData().whynackHarmony = false;
            __instance.GetAdditionalData().whynackMeditating = false;
            __instance.GetAdditionalData().rotatedForce = false;
            __instance.GetAdditionalData().cameraLock = false;
            __instance.GetAdditionalData().whynackUpper = false;
            __instance.GetAdditionalData().rage = 0f;
            __instance.GetAdditionalData().blockPierce = 0f;
            __instance.GetAdditionalData().upwardsKnockback = 0f;
            __instance.GetAdditionalData().cursorFear = 0f;
            __instance.GetAdditionalData().useAmmo = true;
            __instance.GetAdditionalData().Mcnally = false;
            __instance.GetAdditionalData().ExtraHeal = 1f;
            __instance.GetAdditionalData().BlackHole = false;
            __instance.GetAdditionalData().WhiteHole = false;
            __instance.GetAdditionalData().mapSizeI = 0f;
            //__instance.GetAdditionalData().CommonShufflePerPick = 0;
            //__instance.GetAdditionalData().ShufflesPerPick = 0;
            //__instance.GetAdditionalData().shuffles = 0;
            //__instance.GetAdditionalData().storeDamage = true;
            //__instance.GetAdditionalData().storedDamage = Vector2.zero;
            //__instance.GetAdditionalData().takeStoredDamage = false;
        }
    }
}