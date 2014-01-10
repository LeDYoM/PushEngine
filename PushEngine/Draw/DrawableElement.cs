using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using PushEngine.Containers;

namespace PushEngine.Draw
{
	public class DrawableElement : Container
    {
        protected Material material;
        protected SizeF size_;

        // Private properties
        protected Vector3d[] vertex = null;
        protected int numVertex = 0;

        public Container ParentContainer
        {
            internal set;
            get;
        }

        internal DrawableElement()
        {
        }

        internal virtual void Create(int nVertex)
        {
            numVertex = nVertex;
            resetVertex(nVertex);
        }

        public double PositionX
        {
            get { return matrix.M41; }
            set
            {
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

        public float Height
        {
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

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
            if (material != null)
            {
                material.Dispose();
                material = null;
            }
        }

        ~DrawableElement()
        {
            Dispose();
        }

    }
}
