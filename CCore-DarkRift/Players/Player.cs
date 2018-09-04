using System;
using DarkRift.Server;
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
        }

        public void OnLeftRoom(Room room)
        {
            MyRoom = null;
        }

        public void OnDisconnected()
        {
            DisconnectedEvent?.Invoke(this);
        }
    }
}