using System;
using System.Collections.Generic;
using Senary.Logging;
using Senary.Players;

namespace Senary.Rooms
{
    public class Room
    {
        private readonly int id;
        
        private ushort minPlayerCount = 1;

        private ushort maxPlayerCount = 2;

        private readonly List<Player> players = new List<Player>();

        public int ID => id;

        public bool IsEmpty => players.Count == 0;

        public bool HasEnoughPlayers => players.Count >= minPlayerCount;
        
        public bool IsFull => players.Count == maxPlayerCount;

        public Room(int id)
        {
            this.id = id;
        }

        public Room(ushort minPlayerCount, ushort maxPlayerCount)
        {
            this.minPlayerCount = minPlayerCount;
            this.maxPlayerCount = maxPlayerCount;
        }

        public void SetMinPlayerCount(ushort minPlayerCount)
        {
            this.minPlayerCount = minPlayerCount;
        }

        public void SetMaxPlayerCount(ushort maxPlayerCount)
        {
            this.maxPlayerCount = maxPlayerCount;
        }

        public bool AddPlayer(Player player)
        {
            Log.WriteLog("Trying to add player to room...");
            
            if (players.Contains(player))
            {
                Log.WriteLog("Player is already in this room! Stopping...");
                
                return false;
            }
            
            players.Add(player);
            
            Log.WriteLog("Player added to room! - Room player count {0}", players.Count);

            return true;
        }

        public bool RemovePlayer(Player player)
        {
            Log.WriteLog("Trying to remove player from room...");
            
            if (!players.Contains(player))
            {
                Log.WriteLog("Player seems to not be in this room! Stopping...");
                
                return false;
            }
            
            players.Remove(player);
            
            Log.WriteLog("Player removed from room! - Room player count {0}", players.Count);

            return true;
        }
    }
}