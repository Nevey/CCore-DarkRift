using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
	[SerializeField] private UnityClient client;
	
	void Awake() 
	{
		client.MessageReceived += ClientOnMessageReceived;
	}

	private void ClientOnMessageReceived(object sender, MessageReceivedEventArgs e)
	{
		Debug.Log(e.Tag);
	}
}
