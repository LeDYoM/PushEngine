using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;
using PushEngine.Input;

namespace PushEngine.Draw
{
    public delegate void DrawElementDelegate(DrawElement self);

    public class DrawElement : ObjectWithContext, IUpdateAndRender, IDisposable
    {
        // Delegates
        public PEEventReceiver OnEventReceived = null;
        public DrawElementDelegate OnCreationCompleted = null;

        // Private properties
        private DebugHelper dh = Debugger.getDH("DrawElement");
        internal protected Vector2d[] vertex = null;
        internal protected Color4[] color = null;
        internal protected Vector2d[] textureCoordinates = null;
        protected Vector2d position = new Vector2d();
        internal protected int numVertex = 0;
        internal Texture texture = null;

        protected bool hasTransparency = false;
        protected Color4 baseColor = Color.White;
        protected SizeF size = new SizeF(-1, -1);

        public bool HasTransparency { get { return hasTransparency; } }

        public override void ReceiveEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
            }
        }

        public double PositionX { 
            get { return position.X; }
            set {
                position.X = value;
            }
        }

        public double PositionY
        {
            get { return position.Y; }
            set
            {
                position.Y = value;
            }
        }

        public Color4 BaseColor {
            get { return baseColor; }
            set { baseColor = value; }
        }

        public float Width
        {
            get { return size.Width; }
            set { size.Width = value; }
        }

        public float Height {
            get { return size.Height; }
            set { size.Height = value; }
        }

        public double LeftPosition
        {
            get { return PositionX - (size.Width / 2.0); }
            set { PositionX = value + (size.Width / 2.0); }
        }

        public double TopPosition
        {
            get { return PositionY - (size.Height / 2.0); }
            set { PositionY = value + (size.Height / 2.0); }
        }

        public double RightPosition
        {
            get { return PositionX + (size.Width / 2.0); }
            set { PositionX = value - (size.Width / 2.0); }
        }

        public double BottomPosition
        {
            get { return PositionY + (size.Height / 2.0); }
            set { PositionY = value - (size.Height / 2.0); }
        }

        internal DrawElement() : base("DrawElement")
        {
        }

        internal DrawElement(string id_)
            : base(id_)
        {
        }
        protected void resetVertex(int nVertex)
        {
            dh.Assert(nVertex > 0);
            numVertex = nVertex;
            vertex = new Vector2d[numVertex];
            color = new Color4[numVertex];
            setColor(BaseColor);
            textureCoordinates = new Vector2d[numVertex];
        }

        internal void setColor(Color4 color_)
        {
            for (int i = 0; i < numVertex; ++i)
            {
                color[i] = color_;
            }
        }

        public void Update(Context context)
        {
        }

        public void Render(Context context)
        {
            PreRender();
            RenderObject();
            RenderImpl();
            PostRender();
        }

        internal void PreRender()
        {
            if (HasTransparency)
            {
                GL.Enable(EnableCap.Blend);
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            }

            if (texture != null)
            {
                GL.Enable(EnableCap.Texture2D);
                GL.BindTexture(TextureTarget.Texture2D, texture.Id);
            }

            GL.PushMatrix();
            GL.Translate(position.X, position.Y, 0);
        }

        internal void RenderObject()
        {
            GL.Begin(BeginMode.Polygon);
            for (int i = 0; i < numVertex; ++i)
            {
                DrawVertex(ref vertex[i], ref textureCoordinates[i], ref color[i]);
            }
            GL.End();
        }

        internal virtual void RenderImpl()
        {
        }

        internal void PostRender()
        {
            GL.PopMatrix();

            if (texture != null)
            {
                GL.Disable(EnableCap.Texture2D);
            }

            if (HasTransparency)
            {
                GL.Disable(EnableCap.Blend);
            }

        }

        internal protected void DrawVertex(ref Vector2d v, ref Vector2d t, ref Color4 c)
        {
            GL.TexCoord2(t);
            GL.Color4(c);
            GL.Vertex2(v);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);

            if (texture != null)
            {
                texture.Dispose();
            }
            texture = null;
        }

        ~DrawElement()
        {
            dh.WriteLine("Destructor from DrawElement called!");
            Dispose();
        }
    }
}
