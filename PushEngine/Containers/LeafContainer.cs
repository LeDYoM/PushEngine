using System;
using OpenTK;
using PushEngine.Events;

namespace PushEngine.Containers
{
	public class LeafContainer : IContainer
	{
		protected Matrix4d matrix = Matrix4d.Identity;
		protected Renderer renderer { get { return PushEngineCore.Instance.renderer; } }
		public string Name { get; set; }

		public virtual void StartContainer()
		{
			renderer.MultAndPushModelView(ref matrix);
		}

		public virtual void FinishContainer()
		{
			renderer.PopModelView();
		}

		public virtual void Update()
		{
		}

		public virtual void Render()
		{
			StartContainer();
			FinishContainer();
		}

		public virtual void OnStart()
		{
		}

		public virtual void OnKey(KeyEventData kev_)
		{
		}
	}
}

