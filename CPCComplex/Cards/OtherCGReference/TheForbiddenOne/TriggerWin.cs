using Photon.Pun;
using RWF;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.GameModes;
using UnboundLib.Utils;
using UnboundLib;
using UnityEngine;

namespace CPCComplex.MonoBehaviours
{
    public class TriggerWin : MonoBehaviour
    {
        String victoryText = "";
        int playerId = 0;
        public void WinText(String text)
        {
            victoryText = text;
        }
        public void WinnerId(int id)
        {
            playerId = id;
        }
        public IEnumerator Win(IGameModeHandler gm)
        {
            RoundEndHandler instance = RWF.RoundEndHandler.instance;
            int winnerId = playerId;
            UIHandler.instance.DisplayScreenText(PlayerManager.instance.GetColorFromTeam(winnerId).winText, victoryText, 0.5f);

            yield return new WaitForSeconds(3.5f);
            instance.waitingForHost = true;
            PlayerManager.instance.RevivePlayers();
            PlayerManager.instance.InvokeMethod("SetPlayersVisible", false);
            if (PhotonNetwork.IsMasterClient || PhotonNetwork.OfflineMode)
            {
                var choices = new List<string>() { "CONTINUE", "REMATCH", "EXIT" };
                RWF.UI.PopUpMenu.instance.Open(choices, instance.OnGameOverChoose);
            }
            else
            {
                string hostName = PhotonNetwork.CurrentRoom.Players.Values.First(p => p.IsMasterClient).NickName;
                UIHandler.instance.ShowJoinGameText($"WAITING FOR {hostName}", PlayerSkinBank.GetPlayerSkinColors(1).winText);
            }

            MapManager.instance.LoadNextLevel(false, false);

            while (instance.waitingForHost)
            {
                yield return null;
            }

            UIHandler.instance.HideJoinGameText();
        }
    }
}
