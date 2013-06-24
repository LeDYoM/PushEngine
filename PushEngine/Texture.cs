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
            Debug.Assert(id_ > 0);
            Debug.Assert(original != null);

            id = id_;
            originalTexture = original;
            gfx = Graphics.FromImage(originalTexture);
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
            Bitmap bmp = new Bitmap(size.Width, size.Height);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.Clear((Color)color);
            gfx.Dispose();

            return CreateFromBitmap(bmp);
            
        }

        private void UploadBitmap()
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

            return CreateFromBitmap(bmp);
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

        private static Bitmap bmp_t = new Bitmap(1, 1);
        private static Graphics gfx_t = Graphics.FromImage(bmp_t);

        private static SizeF MeasureString(string text, Font font)
        {
            return gfx_t.MeasureString(text, font);
        }

        internal static Texture CreateTextTexture(string text, Font font, Color4 foreColor, Color4 backColor, Size size, TextAlignment alignment)
        {
            if (size.Width < 1 || size.Height < 1)
            {
                size = MeasureString(text, font).ToSize();
            }

            Bitmap bmp = new Bitmap((int)size.Width, (int)size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics gfx = Graphics.FromImage(bmp);
            gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Color tr = Color.Transparent;
//            tr = Color.FromArgb(128, 0, 0, 0);
            gfx.Clear((Color)backColor);

            Point pointWhereDraw;
            Size s = MeasureString(text, font).ToSize();

            switch (alignment)
            {
                case TextAlignment.Center:
                    pointWhereDraw = new Point(size.Width / 2, size.Height / 2);
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
            gfx.Dispose();

            return CreateFromBitmap(bmp);
        }

        public void Dispose()
        {
            if (id > -1)
            {
//                GL.DeleteTexture(id);
                Debug.WriteLine("Texture " + id + " disposed");
            }
            else
            {
                Debug.WriteLine("Called dispose from empty texture");
            }
            if (originalTexture != null)
                originalTexture.Dispose();

            if (gfx != null)
                gfx.Dispose();

            GC.SuppressFinalize(this);
        }

        ~Texture()
        {
            Debug.WriteLine("Destructor from texture called!");
            Dispose();
        }
    }
}
