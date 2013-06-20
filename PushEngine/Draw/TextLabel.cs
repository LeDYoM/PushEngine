using System;
using System.Drawing;
using OpenTK.Graphics;

namespace PushEngine.Draw
{
    internal class TextLabel : Quad
    {
        internal TextLabel(): base()
        {
        }

        internal override void PostInit()
        {
            // Create a new text texture.

            texture = Texture.CreateTextTexture("test agromenauer", SystemFonts.DefaultFont, Color4.Blue, Color4.Red);
            base.PostInit();

        }
    }
}
