using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalMap.Map
{
    [System.Serializable]
    public class ColorizeComponent
    {
        private Color colorHover;
        private Color startColor;

        private SpriteRenderer spriteRender;
        public ColorizeComponent(SpriteRenderer spr, Color clHover)
        {
            spriteRender = spr;
            startColor = spr.color;
            colorHover = clHover;
        }

        public void OnHover()
        {
            spriteRender.color = colorHover;
        }

        public void OnPaint(Color color) 
        {
            spriteRender.color = color;
        }

        public void PaintStartColor()
        {
            spriteRender.color = startColor;
        }
    }
}
