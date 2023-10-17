using UnityEngine;

namespace GlobalMap.Map
{
    public class ColorizeComponent
    {
        private Color startColor;
        private SpriteRenderer spriteRender;
        public ColorizeComponent(SpriteRenderer spr)
        {
            spriteRender = spr;
            startColor = spr.color;
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
