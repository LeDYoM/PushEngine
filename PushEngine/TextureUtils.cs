using System;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;

namespace PushEngine
{
    internal class TextureUtils
    {
        private static Bitmap bmp_t = new Bitmap(1, 1);
        private static Graphics gfx_t = Graphics.FromImage(bmp_t);

        internal static SizeF MeasureString(string text, Font font)
        {
            return gfx_t.MeasureString(text, font);
        }

        internal static Texture CreateFromBitmap(Bitmap bmp)
        {
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return new Texture(id, bmp);
        }

        internal static Texture CreateEmpty(Size size, Color4 color = default(Color4))
        {
            Bitmap bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.Clear((Color)color);
            gfx.Dispose();

            return CreateFromBitmap(bmp);
        }

        internal static Rectangle DrawString(string text, Font font, Color4 foreColor,
            Color4 backColor, Size size, TextAlignment alignment, ref Graphics gfx)
        {
            Size s = TextureUtils.MeasureString(text, font).ToSize();

            if (size.Width < 1 || size.Height < 1)
            {
                size = s;
            }

            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            gfx.Clear((Color)backColor);

            Point pointWhereDraw;

            switch (alignment)
            {
                case TextAlignment.Center:
                    pointWhereDraw = new Point(size.Width / 2 - s.Width / 2, size.Height / 2 - s.Height / 2);
                    break;
                default:
                case TextAlignment.Left:
                    pointWhereDraw = new Point(0, 0);
                    break;
                case TextAlignment.Right:
                    pointWhereDraw = new Point(size.Width - s.Width, size.Height - s.Height);
                    break;
            }
            gfx.DrawString(text, font, new SolidBrush((Color)foreColor), pointWhereDraw);
            Rectangle rect = new Rectangle(pointWhereDraw, s);
            return rect;

        }

        internal static Texture CreateTextTexture(string text, Font font, Color4 foreColor, Color4 backColor, TextAlignment alignment)
        {
            Size size = TextureUtils.MeasureString(text, font).ToSize();

            Bitmap bmp = new Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(bmp);

            DrawString(text, font, foreColor, backColor, size, alignment, ref gfx);

            gfx.Dispose();

            return TextureUtils.CreateFromBitmap(bmp);
        }

    }
}
