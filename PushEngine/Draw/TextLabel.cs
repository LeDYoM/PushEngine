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
            initProperties.AddDefaults(new PENamedPropertyList()
                {
                    new PENamedProperty("transparent", true),
                    new PENamedProperty("text", "none"),
                    new PENamedProperty("fontIndex", -1),
                    new PENamedProperty("backgroundColor_R", 0),
                    new PENamedProperty("backgroundColor_G", 0),
                    new PENamedProperty("backgroundColor_B", 0),
                    new PENamedProperty("backgroundColor_A", 0),
                    new PENamedProperty("foregroundColor_R", 1),
                    new PENamedProperty("foregroundColor_G", 1),
                    new PENamedProperty("foregroundColor_B", 1),
                    new PENamedProperty("foregroundColor_A", 1),
                    new PENamedProperty("alignment", TextAlignment.Center),
                }
            );
        }

        internal string Text
        {
            get { return text; }
            set { text = value; }
        }
        /*
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
        */
        public override void initObject(PENamedPropertyList prop)
        {
            // Create a new text texture.
            if (getProperty<int>("fontIndex") == -1)
                font = SystemFonts.DefaultFont;


            texture = TextureUtils.CreateTextTexture(getProperty<string>("text"),
                font, 
                PENamedPropertyListUtils.getColor4("foregroundColor", initProperties.getList()),
                PENamedPropertyListUtils.getColor4("backgroundColor", initProperties.getList()),
                getProperty<TextAlignment>("alignment"));

            base.initObject(prop);
        }
    }
}
