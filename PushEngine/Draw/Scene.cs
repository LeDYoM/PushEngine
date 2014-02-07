﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using PushEngine.Containers;
using OpenTK;
using System.Drawing;

namespace PushEngine.Draw
{
	public class Scene : TContainer<IContainer>, IDisposable
    {
        protected View perspectiveView = new View();
        public View SceneView { get { return perspectiveView; } }
		protected List<Timer> timers = new List<Timer>();

        public Scene() { }

		public Timer newTimer()
		{
			timers.Add (new Timer ());
			return timers[timers.Count - 1];
		}

		internal T GetNewDrawElement<T>(string name_) where T : IContainer, new()
        {
            T obj = new T();
            obj.Name = name_;
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

		public override void Update ()
		{
			base.Update ();
			foreach (Timer t in timers)
			{
				t.Update ();
			}
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
