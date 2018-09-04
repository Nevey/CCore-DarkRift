using System;
using DarkRift;
using DarkRift.Server;
using DarkRift.Server.Plugins.LogWriters;
using Senary.Logging;
using Senary.Messaging;
using Senary.Players;
using Senary.Rooms;

namespace Senary
{
    // TODO: Separate room logic into a different plugin
    public class CCorePlugin : Plugin
    {
        private readonly MessageController messageController;
        
        private readonly PlayerController playerController = new PlayerController();

        private readonly RoomController roomController = new RoomController();

        public override Version Version => new Version();
        
        public override bool ThreadSafe => false;

        public CCorePlugin(PluginLoadData pluginLoadData) : base(pluginLoadData)
        {
            Log.SetPluginLoadData(pluginLoadData);
            
            messageController = new MessageController(ClientManager);
            
            ClientManager.ClientConnected += OnClientConnected;
            ClientManager.ClientDisconnected += OnClientConnected;
        }

        private void OnClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Player player = playerController.OnPlayerConnected(e.Client);

            if (player == null)
            {
                return;
            }
            
            roomController.AddPlayerToRandomRoom(player);
        }

        private void OnClientConnected(object sender, ClientDisconnectedEventArgs e)
        {
            playerController.OnPlayerDisconnected(e.Client);
        }
    }
}