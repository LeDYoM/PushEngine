using System;
using System.Collections.Generic;

namespace PushEngine
{
    public struct PEClientData
    {
        public string name;
        public string version;
    }

    public class PEClient
    {
        private PEClientData ClientData;
        private static List<PEClient> clients = new List<PEClient>();

        private PEClient(PEClientData clData)
        {
            ClientData = clData;
        }

        public static PEClient newPLEClient(PEClientData clData)
        {
            PEClient newClient = new PEClient(clData);
            clients.Add(newClient);
            return newClient;
        }
    }
}
