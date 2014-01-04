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
        private Renderer renderer { get { return PushEngineCore.Instance.renderer; } }
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

        internal T GetNewDrawElement<T>() where T : SceneElement, new()
        {
            T obj = new T();
            elements.Add(obj);
            return obj;
        }

        public override void Render()
        {
            renderer.ClearScreen();
            renderer.ResetAll();


            base.Render();
        }

        internal List<ISceneElement> ActiveElements
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
