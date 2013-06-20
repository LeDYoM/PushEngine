using System;
using System.Drawing;
using OpenTK.Graphics;

namespace PushEngine.Draw
{
    internal class TextLabel : Quad
    {
        public string text = "none";
        public Font font = SystemFonts.DefaultFont;
        Color4 backgroundColor = Color4.Red;
        Color4 foregroundColor = Color4.Blue;

        internal TextLabel(): base()
        {
        }

        internal override void PostInit()
        {
            // Create a new text texture.

            texture = Texture.CreateTextTexture(text, font, foregroundColor, backgroundColor);
            base.PostInit();

        }
    }
}
