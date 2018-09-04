using DarkRift;
using DarkRift.Server;

namespace Senary.Messaging
{
    public class MessageController
    {
        private readonly IClientManager clientManager;
        
        public MessageController(IClientManager clientManager)
        {
            this.clientManager = clientManager;
        }
        
        public void SendMessage(params byte[] bytes)
        {
            const ushort messageTag = 0;

            using (DarkRiftWriter darkRiftWriter = DarkRiftWriter.Create())
            {
                darkRiftWriter.Write(bytes);

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