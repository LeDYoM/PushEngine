using System;
using System.Collections.Generic;
using System.Diagnostics;
using PushEngine.Containers;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
    public class Scene : ContainerDelegate, INamedObject, IDisposable
    {
        protected View perspectiveView = new View();
        protected View modelView = new View();

        public View ModelView { get { return modelView; } }

        public string Name
        {
            get;
            internal set;
        }

        public Scene(string name_):base()
        {
            Name = name_;
        }

        internal T GetNewDrawElement<T>() where T : Container, new()
        {
            T obj = new T();
            elements.Add(obj);
            return obj;
        }

        private void setPerspective()
        {
            Matrix4d temp = perspectiveView.updateMatrixFromView();
            renderer.SetPerspective(ref temp);
        }

        public override void StartContainer()
        {
			modelView.TopLeft = new Vector2d (-200, 150);
			modelView.DownRight = new Vector2d (200 - 150);
            renderer.ClearScreen();
            renderer.ResetAll();
            setPerspective();
            base.StartContainer();
        }

        public override void FinishContainer()
        {
            base.FinishContainer();
            renderer.PopPerspective();
        }
        internal List<Container> ActiveElements
        {
            get { return elements; } 
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        ~Scene()
        {
            Dispose();
        }

    }
}
