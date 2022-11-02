using UnityEngine;

/*

public class NetworkConsole : MonoBehaviour {

	public void Send (string text){
		Debug.Log("NC sending message.");
		networkView.RPC("Receive", RPCMode.Others, text);
	}
	
	[RPC]
	public void Receive (string text){
		InputBuffer.Submit(text, true);	
	}
	
	public void Host (int n=8){
		Network.InitializeServer(n, 1234, false);
	}
	
	public void Join (string ip){
		Network.Connect(ip,1234);
	}
	
	public string Status(){
		if (Network.isClient) {return "Client";}
		else if (Network.isServer) {return "Server";}
		else {return "Not connected";}
	}
	
	
	
}
 
*/