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

        public bool addScenes(string[] names, int startIndex)
        {
            if (scenes.Count < 1 && scenesStack.Count < 1)
            {
                Debug.Assert(names != null, "Scene names list cannot be null");
                Debug.Assert(names.Length > 0, "Scene list is 0 length");
                Debug.Assert(startIndex < names.Length, "Start index is out of range");

                foreach (string name in names)
                {
                    GetNew(name);
                }
                Push(scenes[startIndex]);
                return true;
            }
            return false;
        }

        private void Push(Scene scene)
        {
            scenesStack.Push(scene);
            currentScene = scene;
        }

        private Scene Pop()
        {
            Scene tmp = scenesStack.Pop();
            currentScene = scenesStack.Peek();
            return tmp;
        }

        private Scene GetNew(string name_)
        {
            Scene tmp = new Scene(name_);
            scenes.Add(tmp);
            return tmp;
        }

        private Scene GetNewAndPush(string name_)
        {
            Scene tmp = GetNew(name_);
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

        public Scene getByName(string name_)
        {
            return scenes.Find(x => x.Name.Equals(name_));
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
