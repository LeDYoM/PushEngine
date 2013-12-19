using System;

namespace PushEngine.Containers
{
    interface IContained
    {
        IContainer ParentContainer
        {
            get;
            set;
        }
    }
}
