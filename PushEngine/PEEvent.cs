using System;
using PushEngine.Input;
using PushEngine.Draw;

namespace PushEngine
{
    public class PEEvent : ObjectWithContext
    {
        public enum ClientBroadcasting
        {
            None = 0,
            ToAllClients = 1,
            ToActiveClients = 2
        }

        public enum ObjectBroadcasting
        {
            None = 0,
            ToActiveObjects = 1
        }

        internal Client senderClient = null;
        internal Client receiverClient = null;
        internal Object senderObject = null;
        internal Object receiverObject = null;
        internal string action = "";
        internal ClientBroadcasting cBroadCast = ClientBroadcasting.None;
        internal ObjectBroadcasting oBroadCast = ObjectBroadcasting.None;

        internal PEEvent()
            : base("PEEvent")
        {
        }

        public string Action { get { return action; } }

        public bool isAction(string str)
        {
            return action.Equals(str);
        }


        internal static PEEvent NewEvent(Client receiverClient, Object receiverObject, Client senderClient, Object senderObject)
        {
            PEEvent temp = new PEEvent();
            temp.receiverClient = receiverClient;
            temp.receiverObject = receiverObject;
            temp.senderClient = senderClient;
            temp.senderObject = senderObject;
            return temp;
        }

        // Event creators.
        internal static PEEvent EventForObject(Client receiverClient, Object receiverObject, Client senderClient, Object senderObject)
        {
            PEEvent temp = NewEvent(receiverClient, receiverObject, senderClient, senderObject);
            return temp;
        }

        internal static PEEvent EventForClient(Client receiverClient, Client senderClient, Object senderObject)
        {
            PEEvent temp = NewEvent(receiverClient, null, senderClient, senderObject);
            return temp;
        }

        internal static PEEvent EventForClients(Client senderClient, Object senderObject)
        {
            return NewEvent(null, null, senderClient, senderObject);
        }

        // Helpers for Key events.
        public const string Key = "eventKey";
        public const string KeyPressedAction = "KeyPressed";
        public const string KeyPressingAction = "KeyPressing";
        public const string KeyReleasedAction = "KeyReleased";

        public bool IsKeyPressed { get { return (isAction(KeyPressedAction)); } }
        public bool IsKeyPressing { get { return (isAction(KeyPressingAction)); } }
        public bool IsKeyReleased { get { return (isAction(KeyReleasedAction)); } }

        public bool isKeyEvent { get { return IsKeyPressed || IsKeyPressing || IsKeyReleased; } }
        public Key EventKey { get { return (Key)getContextProperty(Key); } }

        internal static PEEvent EventForKey(Key k)
        {
            PEEvent temp = EventForClients(null, null);
            temp.cBroadCast = ClientBroadcasting.ToActiveClients;
            temp.oBroadCast = ObjectBroadcasting.ToActiveObjects;
            temp.setContextProperty(Key, k);
            return temp;
        }

        internal static PEEvent KeyPressedEvent(Key k)
        {
            PEEvent temp = EventForKey(k);
            temp.action = KeyPressedAction;
            return temp;
        }

        internal static PEEvent KeyPressingEvent(Key k)
        {
            PEEvent temp = EventForKey(k);
            temp.action = KeyPressingAction;
            return temp;
        }

        internal static PEEvent KeyReleasedEvent(Key k)
        {
            PEEvent temp = EventForKey(k);
            temp.action = KeyReleasedAction;
            return temp;
        }
    }
}
