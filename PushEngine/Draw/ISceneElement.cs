using System;

namespace PushEngine.Draw
{
    public interface ISceneElement : IUpdateAndRender
    {
        void StartContainer();
        void FinishContainer();
    }
}
