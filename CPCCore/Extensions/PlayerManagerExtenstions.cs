using InControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPCCore.Extensions
{
    internal static class PlayerManagerExtensions
    {
        public static Player GetPlayerWithID(this PlayerManager instance, int playerID)
        {
            return instance.players.FirstOrDefault(p => p.playerID == playerID);
        }
        public static void ForEachPlayer(this PlayerManager instance, Action<Player> action)
        {
            foreach (Player player in instance.players)
            {
                action(player);
            }
        }
    }
}