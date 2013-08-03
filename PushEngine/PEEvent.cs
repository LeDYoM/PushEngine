using System;
using PushEngine.Input;

namespace PushEngine
{
    public class PEEvent : ObjectWithContext
    {
        public enum EventType
        {
            Key = 0,
            ObjectProperties = 1,
            ObjectState = 2
        }

        public enum EventScope
        {
            SystemOnly = 0,
            All = 1,
            ProcessesOnly = 2,
            ProcessInternal = 3
        }

        public EventType eType { get; internal set; }
        public EventScope eScope { get; internal set; }

        public PEEvent(EventType type):base("PEEvent"+type.ToString())
        {
            eType = type;
            eScope = EventScope.ProcessInternal;
        }

        internal PEEvent(EventType type, EventScope eScope_):this(type)
        {
            eScope = eScope_;
        }

        // Helpers for Key events.
        public Key EventKey { get { return (Key)getContextProperty("Key"); } }

        public bool IsKeyPressed  { get { return (getContextProperty("Action").Equals("KeyPressed")); } }
        public bool IsKeyPressing { get { return (getContextProperty("Action").Equals("KeyPressing")); } }
        public bool IsKeyReleased { get { return (getContextProperty("Action").Equals("KeyReleased")); } }

        internal static PEEvent KeyPressedEvent(Key k)
        {
            PEEvent temp = new PEEvent(EventType.Key, EventScope.All);
            temp.setContextProperty("Key", k);
            temp.setContextProperty("Action", "KeyPressed");
            return temp;
        }

        internal static PEEvent KeyPressingEvent(Key k)
        {
            PEEvent temp = new PEEvent(EventType.Key, EventScope.All);
            temp.setContextProperty("Key", k);
            temp.setContextProperty("Action", "KeyPressing");
            return temp;
        }

        internal static PEEvent KeyReleasedEvent(Key k)
        {
            PEEvent temp = new PEEvent(EventType.Key, EventScope.All);
            temp.setContextProperty("Key", k);
            temp.setContextProperty("Action", "KeyReleased");
            return temp;
        }

    }
}
