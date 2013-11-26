using System;
using System.Drawing;
using OpenTK.Graphics;
using System.Collections.Generic;

namespace PushEngine.Draw
{
    internal class TextLabel : Quad
    {
        internal string text = "none";
        internal Font font = SystemFonts.DefaultFont;
        internal Color4 backgroundColor = Color4.Transparent;
        internal Color4 foregroundColor = Color4.White;
        internal TextAlignment alignment = TextAlignment.Left;
        internal bool autoSize = false;

        public TextLabel()
        {
            hasTransparency = true;
        }

        internal string Text
        {
            get { return text; }
            set { text = value; }
        }

        public override void Create()
        {
            texture = TextureUtils.CreateTextTexture(text, font,  foregroundColor, backgroundColor, alignment);
            base.Create();
        }
    }
}
