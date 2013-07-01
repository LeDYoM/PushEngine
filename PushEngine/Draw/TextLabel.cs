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
        internal Color4 foregroundColor = Color4.White;
        internal TextAlignment alignment = TextAlignment.Left;
        internal bool autoSize = false;

        internal string Text
        {
            get { return text; }
            set
            {
                text = value;

                bool updated = false;

                if (initialized)
                {
                    SizeF s = TextureUtils.MeasureString(text, font);
                    if (autoSize)
                    {
                        if (s.Width != width || s.Height != height)
                        {
                            texture.Dispose();
                            texture = null;
                            PostInit();
                            updated = true;
                        }
                    }

                    if (!updated)
                    {
                        texture.Clear(backgroundColor);
                        texture.DrawString(text, font, foregroundColor, backgroundColor, new Size(-1, -1), alignment);
                        texture.UploadBitmap();
                    }
                }
            }
        }

        public TextLabel(): base()
        {
        }

        internal override void PostInit()
        {
            // Create a new text texture.
            texture = TextureUtils.CreateTextTexture(text, font, foregroundColor, backgroundColor, alignment);
            hasTransparency = true;
            base.PostInit();
        }
    }
}
