using System;
using System.Collections.Generic;
using System.Linq;
using Senary.Logging;
using Senary.Messaging;
using Senary.Players;

namespace Senary.Rooms
{
    public class RoomController
    {
        private readonly List<Room> rooms = new List<Room>();

        private Room CreateRoom()
        {
            // Room id is rooms count
            Room room = new Room(rooms.Count);
            
            Log.WriteLog("New room created...");
            
            rooms.Add(room);

            return room;
        }

        private void RemoveRoom(Room room)
        {
            if (!rooms.Contains(room))
            {
                Log.WriteLog("Failed to remove room from rooms list!");
                
                return;
            }

            rooms.Remove(room);
            
            Log.WriteLog("Room successfully removed from rooms list");

            room = null;
        }

        private void CheckForEmptyRooms()
        {
            Log.WriteLog("Checking for empty rooms to clean up...");
            
            for (int i = 0; i < rooms.Count; i++)
            {
                if (!rooms[i].IsEmpty)
                {
                    continue;
                }
                
                Log.WriteLog("Empty room found, removing...");
                    
                RemoveRoom(rooms[i]);
            }
        }

        private Room GetRandomRoom()
        {
            List<Room> eligibleRooms = rooms.Where(t => !t.IsFull).ToList();

            if (eligibleRooms.Count <= 0)
            {
                Log.WriteLog("No eligible rooms are available, creating new room...");
                
                return CreateRoom();
            }
            
            Random r = new Random();
            int randomIndex = r.Next(eligibleRooms.Count);
            
            Log.WriteLog("Pre-existing room found...");

            return eligibleRooms[randomIndex];
        }

        public void AddPlayerToRandomRoom(Player player)
        {
            Log.WriteLog("Trying to add player to random room...");
            
            Room room = GetRandomRoom();

            if (!room.AddPlayer(player))
            {
                Log.WriteLog("Failed to add player to random room!");
                
                return;
            }
            
            player.OnJoinedRoom(room);
                
            player.DisconnectedEvent += RemovePlayerFromRoom;
            
            CCorePlugin.Instance.MessageController.SendMessage(
                MessageTags.PLAYERJOINEDROOM,
                (byte)room.ID);
        }

        public void RemovePlayerFromRoom(Player player)
        {
            Log.WriteLog("Trying to remove player from it's room...");
            
            Room room = player.MyRoom;

            if (!rooms.Contains(room))
            {
                Log.WriteLog("Room could not be found in rooms list!");
                
                return;
            }

            if (!room.RemovePlayer(player))
            {
                Log.WriteLog("Failed to remove player from room!");
                
                return;
            }
            
            player.OnLeftRoom(room);

            player.DisconnectedEvent -= RemovePlayerFromRoom;
            
            CheckForEmptyRooms();

            // TODO: Send message to players
        }
    }
}