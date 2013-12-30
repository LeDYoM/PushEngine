using System;
using System.Collections.Generic;
using PushEngine.Containers;

namespace PushEngine.Draw
{
    public class SceneDirector : Container, IDisposable
    {
        private Stack<Scene> scenesStack = new Stack<Scene>();
        private List<Scene> scenes = new List<Scene>();
        private Scene currentScene = null;

        internal SceneDirector()
        {
        }

        public Scene CurrentScene { get { return currentScene; } }

        public void Push(Scene scene)
        {
            scenesStack.Push(scene);
            currentScene = scene;
        }

        public Scene Pop()
        {
            Scene tmp = scenesStack.Pop();
            currentScene = scenesStack.Peek();
            return tmp;
        }

        public Scene GetNew()
        {
            Scene tmp = new Scene();
            scenes.Add(tmp);
            return tmp;
        }

        public Scene GetNewAndPush()
        {
            Scene tmp = GetNew();
            Push(tmp);
            return tmp;
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

        public override bool OnEvent(PEEvent event_)
        {
            bool t = base.OnEvent(event_);
            if (CurrentScene != null)
            {
                return CurrentScene.OnEvent(event_);
            }
            else
            {
                Debug.Log("Received event without having a current Scene");
                return false;
            }
        }

        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);

            scenesStack.Clear();

            foreach (Scene scn in scenes)
            {
                scn.Dispose();
            }

            scenes.Clear();
        }
    }
}
