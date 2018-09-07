using System.Data;
using DarkRift;
using DarkRift.Server;
using Senary.Logging;

namespace Senary.Messaging
{
    public class MessageController
    {
        private readonly IClientManager clientManager;
        
        public MessageController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }
        
        public void SendMessage(ushort messageTag, params byte[] bytes)
        {
            using (DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create())
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    darkRiftWriter.Write(bytes[i]);
                }

                using (Message newPlayerMessage = Message.Create(messageTag, darkRiftWriter))
                {
                    foreach (IClient client in clientManager.GetAllClients())
                    {
                        client.SendMessage(newPlayerMessage, SendMode.Reliable);
                    }
                }
            }
        }
    }
}