using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics;

namespace PushEngine
{
    internal class Texture : IDisposable
    {
        private int id = -1;
        private Bitmap originalTexture = null;
        private Graphics gfx;
        private Rectangle dirty_region = Rectangle.Empty;

        internal int Id { get { return id; } }

        internal Texture(int id_, Bitmap original)
        {
            id = id_;
            originalTexture = original;
            gfx = Graphics.FromImage(originalTexture);
        }

        internal void UpdateTexture(Bitmap bmp, Rectangle rect)
        {
            dirty_region.Intersect(rect);
            UploadBitmap();
        }

        internal void UploadBitmap()
        {
            if (dirty_region != RectangleF.Empty)
            {
                System.Drawing.Imaging.BitmapData data = originalTexture.LockBits(dirty_region,
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);
 	
                GL.BindTexture(TextureTarget.Texture2D, id);
                GL.TexSubImage2D(TextureTarget.Texture2D, 0,
                    dirty_region.X, dirty_region.Y, dirty_region.Width, dirty_region.Height,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                originalTexture.UnlockBits(data);
 	
                dirty_region = Rectangle.Empty;
            }
        }

        internal static Texture LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            Bitmap bmp = new Bitmap(filename);

            return TextureUtils.CreateFromBitmap(bmp);
        }

        internal Size TextureSize
        {
            get
            {
                if (originalTexture != null)
                {
                    return originalTexture.Size;
                }
                return Size.Empty;
            }
        }

        internal void Clear(Color4 color)
        {
            gfx.Clear((Color)color);
            dirtyAll();
        }

        internal void dirtyAll()
        {
            dirty_region = new Rectangle(0, 0, originalTexture.Width, originalTexture.Height);
        }

        internal void DrawString(string text, Font font, Color4 foreColor,
            Color4 backColor, Size size, TextAlignment alignment)
        {
            Rectangle rect = TextureUtils.DrawString(text, font, foreColor, backColor, size, alignment, ref gfx);
            dirtyAll();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (id > -1)
            {
                GL.DeleteTexture(id);
            }
            else
            {
            }

            if (originalTexture != null)
            {
                originalTexture.Dispose();
                originalTexture = null;
            }

            if (gfx != null)
            {
                gfx.Dispose();
                gfx = null;
            }
        }

        ~Texture()
        {
            Dispose();
        }
    }
}
