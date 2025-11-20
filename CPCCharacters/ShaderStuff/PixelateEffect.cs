using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CPCCharacters.Shaders
{
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class PixelateEffect : MonoBehaviour
    {
        public Material Material;
        public Player player;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (Material == null)
            {
                Graphics.Blit(src, dest);
                return;
            }

            //var temp = RenderTexture.GetTemporary(src.width, src.height);

            Graphics.Blit(src, dest, Material, 0);

            //Graphics.Blit(temp, dest);

            //RenderTexture.ReleaseTemporary(temp);

            if (player != null && player.data.dead)
                Destroy(this);
        }
    }
}