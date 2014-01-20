using System;
using System.Collections.Generic;
using PushEngine.Containers;
using PushEngine.Events;

namespace PushEngine.Draw
{
    public class SceneDirector : TContainer<Scene>, IDisposable
    {
        private Stack<Scene> scenesStack = new Stack<Scene>();
        private Scene currentScene = null;

        internal SceneDirector()
        {
        }

        public Scene CurrentScene { get { return currentScene; } }

        public T GetNewScene<T>(string name_) where T : Scene, new()
        {
            T obj = new T();
            obj.Name = name_;
            elements.Add(obj);
            return obj;
        }

        public T GetNewSceneAndPush<T>(string name_) where T : Scene, new()
        {
            return Push(GetNewScene<T>(name_)) as T;
        }

        private Scene Push(Scene scene)
        {
            scenesStack.Push(scene);
            currentScene = scene;
            return scene;
        }

        private Scene Pop()
        {
            Scene tmp = scenesStack.Pop();
            currentScene = scenesStack.Peek();
            return tmp;
        }

		public override void  OnStart()
        {
            PEDebug.Assert(currentScene != null, "There is no scene on Start");
            currentScene.OnStart();
        }

		public override void OnKey(KeyEventData kev_)
        {
            PEDebug.Assert(currentScene != null, "There is no scene on Key");
            currentScene.OnKey(kev_);
        }

        public override void Update()
        {
            if (currentScene != null)
                currentScene.Update();
        }

        public override void Render()
        {
            if (currentScene != null)
                currentScene.Render();
        }

        public Scene getByName(string name_)
        {
            return elements.Find(x => x.Name.Equals(name_));
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            scenesStack.Clear();

            foreach (Scene scn in elements)
            {
                scn.Dispose();
            }

            elements.Clear();
        }
    }
}
