using System;
using PushEngine.Input;

namespace PushEngine
{
    public class PEEvent : ObjectWithContext
    {
        internal Client senderClient = null;
        internal Client receiverClient = null;
        internal Object senderObject = null;
        internal Object receiverObject = null;
        internal string Action = "";
        internal Key key;

        public const string ActionStartProcess = "StartProcess";

        internal PEEvent()
            : base("PEEvent")
        {
        }

        // Helpers for Key events.
        public Key EventKey { get { return key; } }

        public bool IsKeyPressed { get { return (Action.Equals("KeyPressed")); } }
        public bool IsKeyPressing { get { return (Action.Equals("KeyPressing")); } }
        public bool IsKeyReleased { get { return (Action.Equals("KeyReleased")); } }

        public bool IsStartForProcess
        {
            get
            {
                if (eScope == PEEvent.EventScope.Process)
                {
                    if (Action.Equals(PEEvent.ActionStartProcess))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool isKeyEvent { get { return Action.Equals("KeyPressed") || Action.Equals("KeyPressing") || Action.Equals("KeyReleased"); } }

        internal static PEEvent KeyPressedEvent(Key k)
        {
            PEEvent temp = new PEEvent(EventScope.All);
            temp.key = k;
            temp.Action = "KeyPressed";
            return temp;
        }
    }
}
