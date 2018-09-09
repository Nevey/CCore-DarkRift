using System;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

namespace CCore.Scripts.Messaging
{
	public class MessageListener : MonoBehaviour
	{
		[SerializeField] private UnityClient client;

		public event Action TestEvent;
		public event Action<int> PlayerJoinedRoomEvent;
		public event Action<int> PlayerLeftRoomEvent;

		public static MessageListener Instance;

		private void Awake()
		{
			client.MessageReceived += ClientOnMessageReceived;

			Instance = this;
		}

		private void OnDestroy()
		{
			client.MessageReceived -= ClientOnMessageReceived;
		}

		private void ClientOnMessageReceived(object sender, MessageReceivedEventArgs e)
		{
			using (Message message = e.GetMessage())
			{
				using (DarkRiftReader reader = message.GetReader())
				{
					switch (e.Tag)
					{
						case MessageTags.TEST:
							
							if (TestEvent != null)
							{
								TestEvent();
							}
							
							break;

						case MessageTags.PLAYERJOINEDROOM:
							
							if (PlayerJoinedRoomEvent != null)
							{
								PlayerJoinedRoomEvent(reader.ReadByte());
							}

							break;
						
						case MessageTags.PLAYERLEFTROOM:

							if (PlayerLeftRoomEvent != null)
							{
								PlayerLeftRoomEvent(reader.ReadByte());
							}
							
							break;
					}
				}
			}
		}
	}
}