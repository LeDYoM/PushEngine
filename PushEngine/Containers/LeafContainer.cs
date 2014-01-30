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
		protected bool started = false;

		public double PositionX
		{
			get { return matrix.M41; }
			set
			{
				matrix.M41 = value;
			}
		}

		public double PositionY
		{
			get { return matrix.M42; }
			set
			{
				matrix.M42 = value;
			}
		}

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
			started = true;
		}

		public virtual void OnKey(KeyEventData kev_)
		{
		}
	}
}

