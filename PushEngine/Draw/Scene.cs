using System;
using System.Collections.Generic;

namespace PushEngine.Draw
{
    internal class Scene
    {
        private List<DrawElement> sceneElements = new List<DrawElement>();

        internal Scene()
        {
        }

        internal T GetNewDrawElement<T>() where T : DrawElement, new()
        {
            T obj = new T();
            sceneElements.Add(obj);
            return obj;
        }

    }
}
