using CPCCore.Extensions;
using HarmonyLib;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;
using System.Reflection;
using CPCCore.MonoBehaviours;


namespace CPCCore.Patches
{
    [HarmonyPatch(typeof(ExtraPlayerSkins))]
    public class PoppyTeam
    {
        static PlayerSkin Skin;
        static readonly PlayerSkin PoppySkin = new PlayerSkin
        {
            color = new Color(1f*0.75f, 0.835f*0.6f, 0f, 0.75f),
            backgroundColor = new Color(1f*0.75f,0.835f*0.75f,0f, 0.75f),
            winText = new Color(1f * 0.75f, 0.835f *0.6f, 0f, 0.5f),
            particleEffect = new Color(1f * 0.75f, 0.835f*0.6f, 0f, 0f)
        };
        const int TeamID = 68;
        [HarmonyPatch(nameof(ExtraPlayerSkins.GetTeamColorName))]
        [HarmonyPostfix]
        public static void PatchName(int teamID, ref string __result)
        {
            if (teamID == TeamID) __result = "Poppy [she/her]";
        }
        [HarmonyPatch(nameof(ExtraPlayerSkins.GetPlayerSkinColors))]
        [HarmonyPrefix]
        public static bool PatchGetSkin(int colorID, ref PlayerSkin __result)
        {
            if (colorID != TeamID) return true;
            if (Skin == null)
            {
                PlayerSkin skin = ((PlayerSkinBank)typeof(PlayerSkinBank).GetProperty("Instance", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null, null))?.skins[colorID % 4].currentPlayerSkin;
                PlayerSkin newSkin = Object.Instantiate(skin).gameObject.GetComponent<PlayerSkin>();
                Object.DontDestroyOnLoad(newSkin);
                PlayerSkin skinToSet = PoppySkin;
                newSkin.color = skinToSet.color;
                newSkin.backgroundColor = skinToSet.backgroundColor;
                newSkin.winText = skinToSet.winText;
                newSkin.particleEffect = skinToSet.particleEffect;
                PlayerSkinParticle newSkinPart = newSkin.GetComponentInChildren<PlayerSkinParticle>();
                ParticleSystem part = newSkinPart.GetComponent<ParticleSystem>();
                ParticleSystem.MainModule main = part.main;
                ParticleSystem.MinMaxGradient startColor = main.startColor;
                startColor.colorMin = skinToSet.backgroundColor;
                startColor.colorMax = skinToSet.color;
                main.startColor = startColor;
                newSkinPart.SetFieldValue("startColor1", skinToSet.backgroundColor);
                newSkinPart.SetFieldValue("startColor2", skinToSet.color);
                Skin = newSkin;
            }
            __result = Skin;
            return false;
        }
    }
}