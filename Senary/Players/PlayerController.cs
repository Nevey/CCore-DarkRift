using System.Collections.Generic;
using System.Linq;
using DarkRift.Server;
using Senary.Logging;

namespace Senary.Players
{
    public class PlayerController
    {
        private readonly List<Player> players = new List<Player>();

        private Player CreateNewPlayer(IClient client)
        {
            Player player = new Player(client);

            if (HasPlayerWithID(player.Client.ID))
            {
                Log.WriteLog("Player with ID {0} was already found, not creating new player...", player.Client.ID);
                
                return null;
            }
            
            Log.WriteLog("Created new player with ID {0}", player.Client.ID);
            
            players.Add(player);

            return player;
        }

        private void RemovePlayer(IClient client)
        {
            Player player = GetPlayerWithID(client.ID);

            if (player == null)
            {
                Log.WriteLog("Trying to disconnect player with ID {0}, but this player could not be found!",
                    client.ID);
                
                return;
            }
            
            Log.WriteLog("Disconnecting player with ID {0}", player.Client.ID);
            
            player.OnDisconnected();

            players.Remove(player);
        }

        private bool HasPlayerWithID(ushort id)
        {
            return players.Any(t => t.Client.ID == id);
        }

        private Player GetPlayerWithID(ushort id)
        {
            return players.FirstOrDefault(t => t.Client.ID == id);
        }

        public Player OnPlayerConnected(IClient client)
        {
            return CreateNewPlayer(client);
        }

        public void OnPlayerDisconnected(IClient client)
        {
            RemovePlayer(client);
        }
    }
}