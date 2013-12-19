using System;
using OpenTK;

namespace PushEngine.Containers
{
    public interface IContainer
    {
        Vector2d TopLeft { get; }
        Vector2d DownRight { get; }

        void apply();
    }
}
