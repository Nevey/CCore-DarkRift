using UnityEngine;
using UnityEngine.UI;

namespace CCore.Scripts.Players
{
    public class PlayerRoomView : MonoBehaviour
    {
        [SerializeField] private Text text;

        public void SetPlayerID(int id)
        {
            text.text = id.ToString();
        }
    }
}