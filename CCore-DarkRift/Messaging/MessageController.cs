using DarkRift;
using DarkRift.Server;
using Senary.Players;

namespace Senary.Messaging
{
    public class MessageController
    {
        private readonly IClientManager clientManager;
        
        public MessageController(IClientManager clientManager)
        {
            // TODO: Check if this can be removed
            this.clientManager = clientManager;
        }
        
        public void SendMessage(int roomID, ushort messageTag, params byte[] bytes)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    writer.Write(bytes[i]);
                }

                using (Message message = Message.Create(messageTag, writer))
                {
                    for (int i = 0; i < CCorePlugin.Instance.PlayerController.Players.Count; i++)
                    {
                        Player player = CCorePlugin.Instance.PlayerController.Players[i];

                        if (player.MyRoom.ID != roomID)
                        {
                            continue;
                        }

                        player.Client.SendMessage(message, SendMode.Reliable);
                    }
                }
            }
        }
    }
}