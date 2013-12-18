using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;
using System.Collections.Generic;
using PushEngine.Input;

namespace PushEngine.Draw
{
    public delegate void DrawElementDelegate(SceneElement self);

    public class SceneElement : Object, IUpdateAndRender, IDisposable
    {
        // Delegates
        public PEEventReceiver OnEventReceived = null;

        protected Renderer renderer { get { return PushEngineCore.Instance.renderer; } }

        internal Material material;
        protected SizeF size_;

        // Private properties
        internal protected Vector3d[] vertex = null;
        protected Matrix4d matrix = Matrix4d.Identity;
        internal protected int numVertex = 0;

        internal SceneElement()
        {
        }

        internal virtual void Create(int nVertex)
        {
            numVertex = nVertex;
            resetVertex(nVertex);
        }

        public override void ReceiveEvent(PEEvent event_)
        {
            if (OnEventReceived != null)
            {
                OnEventReceived(event_);
            }
        }

        public double PositionX { 
            get { return matrix.M41; }
            set {
                matrix.M41 = value;
            }
        }

        public double PositionY
        {
            get { return matrix.M42; }
            set
            {
                matrix.M42 = value;
            }
        }

        protected virtual void SizeChanged()
        {
        }

        public float Width
        {
            get { return size_.Width; }
            set { size_.Width = value; SizeChanged(); }
        }

        public float Height {
            get { return size_.Height; }
            set { size_.Height = value; SizeChanged(); }
        }

        public double LeftPosition
        {
            get { return PositionX - (size_.Width / 2.0); }
            set { PositionX = value + (size_.Width / 2.0); }
        }

        public double TopPosition
        {
            get { return PositionY - (size_.Height / 2.0); }
            set { PositionY = value + (size_.Height / 2.0); }
        }

        public double RightPosition
        {
            get { return PositionX + (size_.Width / 2.0); }
            set { PositionX = value - (size_.Width / 2.0); }
        }

        public double BottomPosition
        {
            get { return PositionY + (size_.Height / 2.0); }
            set { PositionY = value - (size_.Height / 2.0); }
        }

        protected void resetVertex(int nVertex)
        {
            numVertex = nVertex;
            vertex = new Vector3d[numVertex];
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

        internal virtual void PreRender()
        {
        }

        internal virtual void RenderObject()
        {
        }

        internal virtual void RenderImpl()
        {
        }

        internal virtual void PostRender()
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            if (material != null)
            {
                material.Dispose();
                material = null;
            }
        }

        ~SceneElement()
        {
            Dispose();
        }
    }
}
