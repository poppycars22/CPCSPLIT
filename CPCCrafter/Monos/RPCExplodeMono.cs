using Photon.Pun;
using Sonigon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace CPCComplex.MonoBehaviours
{
    public class RPCExplodeMono : MonoBehaviour
    {
        [PunRPC]
        public void RPCAExplodeTest(int playerID, float damage, bool lethal)
        {
            Player player = PlayerManager.instance.GetPlayerWithID(playerID);
            if (damage > 0f)
                player.data.healthHandler.TakeDamage(damage * Vector2.up, base.transform.position, null, player, lethal, true);
            else if (damage < 0f && !lethal)
            {
                float healAmount = 0-damage;
                player.data.health += healAmount;
                player.data.health = Mathf.Clamp(player.data.health, float.NegativeInfinity, player.data.maxHealth);
            }
            else if(damage <0f && lethal)
            {
                float healAmount = 0-damage;
                player.data.healthHandler.Heal(healAmount);
            }
        }
    }
}
