using System;
using PushEngine.Events;

namespace PushEngine.Containers
{
	public interface IContainer
	{
		string Name { get; set; }

		void StartContainer ();
		void FinishContainer ();
		void Update ();
		void Render();
		void OnStart();
		void OnKey (KeyEventData kev_);
	}
}

