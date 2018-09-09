using System;
using DarkRift.Server;
using Senary.Messaging;
using Senary.Rooms;

namespace Senary.Players
{
    public class Player
    {
        private readonly IClient client;

        public IClient Client => client;

        public Room MyRoom { get; private set; }

        public event Action<Player> DisconnectedEvent;

        public Player(IClient client)
        {
            this.client = client;
        }

        public void OnJoinedRoom(Room room)
        {
            MyRoom = room;

            CCorePlugin.Instance.MessageController.SendMessage(MyRoom.ID, MessageTags.PLAYERJOINEDROOM, (byte)Client.ID);
        }

        public void OnLeftRoom(Room room)
        {
            if (MyRoom != room)
            {
                throw new Exception("Player left a room, but wasn't in that room in the first place!!!");
            }
            
            CCorePlugin.Instance.MessageController.SendMessage(MyRoom.ID, MessageTags.PLAYERLEFTROOM, (byte)Client.ID);
            
            MyRoom = null;
        }

        public void OnDisconnected()
        {
            DisconnectedEvent?.Invoke(this);
        }
    }
}