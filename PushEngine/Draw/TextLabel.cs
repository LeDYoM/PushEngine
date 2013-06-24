using System;
using System.Drawing;
using OpenTK.Graphics;

namespace PushEngine.Draw
{
    internal class TextLabel : Quad
    {
        internal string text = "none";
        internal Font font = SystemFonts.DefaultFont;
        internal Color4 backgroundColor = Color4.Transparent;
        internal Color4 foregroundColor = Color4.Blue;
        internal TextAlignment alignment = TextAlignment.Left;

        internal TextLabel(): base()
        {
        }

        internal override void PostInit()
        {
            // Create a new text texture.
            texture = Texture.CreateTextTexture(text, font, foregroundColor, backgroundColor, new Size((int)width, (int)height), alignment);
            hasTransparency = true;
            base.PostInit();
        }
    }
}
