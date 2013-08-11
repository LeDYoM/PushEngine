﻿using System;
using System.Collections.Generic;

namespace PushEngine.Draw
{
    public class SceneDirector : IDisposable
    {
        private Stack<Scene> scenesStack = new Stack<Scene>();
        private List<Scene> scenes = new List<Scene>();
        private Scene currentScene = null;
        private Context context = null;

        internal SceneDirector(Context context_)
        {
            context = context_;
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
            Scene tmp = new Scene(context);
            scenes.Add(tmp);
            return tmp;
        }

        public Scene GetNewAndPush()
        {
            Scene tmp = GetNew();
            Push(tmp);
            return tmp;
        }

        public void Update()
        {
            if (currentScene != null)
                currentScene.Update();
        }

        public void Render()
        {
            if (currentScene != null)
                currentScene.Render();
        }

        public void ReceiveEvent(PEEvent event_)
        {
            if (currentScene != null)
                currentScene.ReceiveEvent(event_);
        }

        public void Dispose()
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