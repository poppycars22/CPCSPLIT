using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;
using BepInEx;
using CPCCore.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.RoundsEffects;
using System.ComponentModel;
using ModdingUtils.GameModes;
using ModdingUtils.MonoBehaviours;
using CPCCore.Extensions;

namespace CPCComplex.MonoBehaviours
{
   public class RainbowLeaf : MonoBehaviour, IGameStartHookHandler, IPointStartHookHandler
    {
        CharacterStatModifiers characterStatModifiers;
         Player player;
        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);

            player = this.GetComponentInParent<Player>();
            characterStatModifiers = this.GetComponentInParent<CharacterStatModifiers>();
        }

        public void OnPointStart()
        {
            player.data.maxHealth += 25;
            player.data.health += 25;
            characterStatModifiers.GetAdditionalData().RainbowLeafHealth += 25;
            CPCDebug.Log($"[{ChaosPoppycarsCardsComplex.ModInitials}]{characterStatModifiers.GetAdditionalData().RainbowLeafHealth}");
        }
        public void OnGameStart()
        {
            characterStatModifiers.GetAdditionalData().RainbowLeafHealth = 0;
            UnityEngine.GameObject.Destroy(this);
        }
        public void OnDestroy()
        {

            //characterStatModifiers.GetAdditionalData().RainbowLeafHealth = 0;
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
        }
        
    }
}
