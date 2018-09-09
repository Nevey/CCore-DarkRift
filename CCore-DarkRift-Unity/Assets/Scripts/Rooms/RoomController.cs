using System.Collections.Generic;
using CCore.Scripts.Messaging;
using CCore.Scripts.Players;
using UnityEngine;

namespace CCore.Scripts.Rooms
{
    public class RoomController : MonoBehaviour
    {
        // TODO: Shove this in a player controller class...
        private readonly List<Player> players = new List<Player>();
            
        private void Start()
        {
            MessageListener.Instance.PlayerJoinedRoomEvent += OnPlayerJoinedRoom;
            MessageListener.Instance.PlayerLeftRoomEvent += OnPlayerLeftRoom;
        }

        private void OnPlayerJoinedRoom(int id)
        {
            Debug.LogFormat("Player with ID {0} joined the room", id);
            
            players.Add(new Player(id));
        }

        private void OnPlayerLeftRoom(int id)
        {
            Debug.LogFormat("Player with ID {0} left the room", id);

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].ID != id)
                {
                    continue;
                }
                
                players.Remove(players[i]);
                
                break;
            }
        }
    }
}