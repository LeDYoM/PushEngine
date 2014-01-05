using System;
using System.Collections.Generic;
using System.Diagnostics;
using PushEngine.Containers;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
    public class Scene : ClientContainer, INamedObject, IDisposable
    {
        protected View perspectiveView = new View();

        public string Name
        {
            get;
            internal set;
        }

        public Scene(string name_):base()
        {
            Name = name_;
        }

        internal T GetNewDrawElement<T>() where T : LeafClientContainer, new()
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

        public override void Render()
        {
            renderer.ClearScreen();
            renderer.ResetAll();
            setPerspective();
            base.Render();
        }

        internal List<LeafClientContainer> ActiveElements
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
