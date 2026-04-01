using TabInfo.Utils;
using CPCCore.Extensions;

namespace CPCTabInfoSTATS
{
    public class TabinfoInterface
    {
        public static void Setup()
        {
            var OtherStats = TabInfoManager.RegisterCategory("Other Stats", 0);
            TabInfoManager.RegisterStat(OtherStats, "Max Dashes", (p) => p.data.stats.GetAdditionalData().dashes != 0, (p) => string.Format("{0:F0}", p.data.stats.GetAdditionalData().dashes));
            TabInfoManager.RegisterStat(OtherStats, "Extra Healing", (p) => p.data.stats.GetAdditionalData().ExtraHeal != 1, (p) => string.Format("{0:F0}%", (p.data.stats.GetAdditionalData().ExtraHeal-1f)*100));
            TabInfoManager.RegisterStat(OtherStats, "Blocks are", (p) => p.data.stats.GetAdditionalData().blockMover && !p.data.stats.GetAdditionalData().blockPush, (p) => "pulling");
            TabInfoManager.RegisterStat(OtherStats, "Blocks <b>are</b>", (p) => p.data.stats.GetAdditionalData().blockMover && p.data.stats.GetAdditionalData().blockPush, (p) => "pushing");
            var MCStats = TabInfoManager.RegisterCategory("Minecraft Stats", 1);
            TabInfoManager.RegisterStat(MCStats, "Totems", (p) => p.data.stats.GetAdditionalData().totems != 0, (p) => string.Format("{0:F0}", p.data.stats.GetAdditionalData().totems));
            TabInfoManager.RegisterStat(MCStats, "Remaining Totems", (p) => p.data.stats.GetAdditionalData().remainingTotems != 0, (p) => string.Format("{0:F0}", p.data.stats.GetAdditionalData().remainingTotems));
            TabInfoManager.RegisterStat(MCStats, "Glowstone", (p) => p.data.stats.GetAdditionalData().Glowstone > 0, (p) => string.Format("{0:F0}", p.data.stats.GetAdditionalData().Glowstone));
            TabInfoManager.RegisterStat(MCStats, "Redstone", (p) => p.data.stats.GetAdditionalData().Redstone > 0, (p) => string.Format("{0:F0}", p.data.stats.GetAdditionalData().Redstone));
            var CritStats = TabInfoManager.RegisterCategory("Critical Hit Stats", 5);
            TabInfoManager.RegisterStat(CritStats, "Crit Damage", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1 != 1f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1 -1f) * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Chance", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1 != 0, (p) => string.Format("{0:F0}%", p.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1 * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Slow", (p) => p.data.weaponHandler.gun.GetAdditionalData().CritSlow > 0, (p) => string.Format("{0:F0}%", p.data.weaponHandler.gun.GetAdditionalData().CritSlow * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Bounces", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitBounces != 0, (p) => string.Format("{0:F0}", p.data.weaponHandler.gun.GetAdditionalData().criticalHitBounces));
            TabInfoManager.RegisterStat(CritStats, "Damage on Bounce when you Crit", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamageOnBounce > 0, (p) => string.Format("{0:F0}%", p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamageOnBounce * 100));
            TabInfoManager.RegisterStat(CritStats, "Guranteed Crits", (p) => p.data.weaponHandler.gun.GetAdditionalData().guranteedCrits == true, (p) => "True");
            TabInfoManager.RegisterStat(CritStats, "Crit Bullet Speed", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalBulletSpeed > 1f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalBulletSpeed - 1f) * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Simulation Speed", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalSimulationSpeed > 1f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalSimulationSpeed - 1f) * 100));
            TabInfoManager.RegisterStat(CritStats, "Unblockable Crits", (p) => p.data.weaponHandler.gun.GetAdditionalData().unblockableCrits == true, (p) => "True");
            TabInfoManager.RegisterStat(CritStats, "Blocking Crits", (p) => p.data.weaponHandler.gun.GetAdditionalData().BlockingCrits == true, (p) => "True");
            TabInfoManager.RegisterStat(CritStats, "Crit Heal", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHeal > 0f, (p) => string.Format("{0:F0}", p.data.weaponHandler.gun.GetAdditionalData().criticalHeal));
            TabInfoManager.RegisterStat(CritStats, "Crit CD Reduction", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalBlockCDReduction > 0f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalBlockCDReduction)));
        }
    }
}