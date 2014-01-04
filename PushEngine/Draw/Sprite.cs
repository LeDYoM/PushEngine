﻿using System;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK;
using PushEngine.Containers;

namespace PushEngine.Draw
{
    internal class Sprite : DrawableElement
    {
        public Sprite() : base()
        {
        }

        internal void CreateSprite(SizeF s, Color4? color)
        {
            Create(4);

            material = new Material(4, null, color);
            size_ = s;
            PostCreate();

        }

        internal void CreateSprite(Texture t, Color4? color)
        {
            Create(4);

            material = new Material(4, t, color);
            size_ = t.TextureSize;
            PostCreate();
        }

        protected override void SizeChanged()
        {
            base.SizeChanged();
            double w2 = Width / 2.0;
            double h2 = Height / 2.0;
            vertex[0] = new Vector3d(w2 * -1, h2, 0);
            vertex[1] = new Vector3d(w2, h2, 0);
            vertex[2] = new Vector3d(w2, h2 * -1, 0);
            vertex[3] = new Vector3d(w2 * -1, h2 * -1, 0);

        }

        protected void PostCreate()
        {
            SizeChanged();
        }

        public override void StartContainer()
        {
            material.startMaterialRenderer();
            renderer.PushMatrix(ref matrix);

        }

        internal override void RenderObject()
        {
            renderer.RenderQuad(vertex, material.textureCoordinates, material.color);
        }

        public override void FinishContainer()
        {
            renderer.PopMatrix();
            material.PostRender();
        }


    }
}
