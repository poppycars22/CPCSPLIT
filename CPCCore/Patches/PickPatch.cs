using System;
using HarmonyLib;
using CPCCore.Extensions;


namespace CPCCore.Patches
{
    /*
    [Serializable]
    [HarmonyPatch(typeof(CardChoice), nameof(CardChoice.StartPick))]
    public class CardChoicePatchStartPick
    {
        public static void Prefix(CardChoice __instance, ref int picksToSet, int pickerIDToSet)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(pickerIDToSet);
            if (player.data.stats.GetAdditionalData().ExtraPicks > 0)
                picksToSet += player.data.stats.GetAdditionalData().ExtraPicks;
            if(player.data.stats.GetAdditionalData().SCommonPicks > 0)
            {
                picksToSet += player.data.stats.GetAdditionalData().SCommonPicks;
                player.data.stats.GetAdditionalData().CommonPicks = player.data.stats.GetAdditionalData().SCommonPicks;
            }
        }
    }

    [Serializable]
    [HarmonyPatch(typeof(CardChoice), nameof(CardChoice.GetRanomCard))]
    public class TemporaryRanonPatchOfDontDoThis()
    {
        public static void Prefix(CardChoice __instance)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(__instance.pickrID);
            if (player.data.stats.GetAdditionalData().CommonPicks == __instance.picks)
            {
                player.data.stats.GetAdditionalData().CommonOnly = true;
                player.data.stats.GetAdditionalData().CommonPicks--;
            }
        }
        public static void Postfix(CardChoice __instance)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(__instance.pickrID);
            if (player.data.stats.GetAdditionalData().CommonOnly)
            {
                player.data.stats.GetAdditionalData().CommonOnly = false;
            }
        }
    }
    */
}