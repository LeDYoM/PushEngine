using System;
using System.Collections.Generic;
using System.Diagnostics;
using PushEngine.Containers;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
	public class Scene : Container, INamedObject, IDisposable
    {
        protected View perspectiveView = new View();

        public View SceneView { get { return perspectiveView; } }

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
