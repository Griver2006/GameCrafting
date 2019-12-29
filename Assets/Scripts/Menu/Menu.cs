using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public string ip, port;

	private byte windiws = 0;
	
	void OnServerInitialized() {
		windiws = 1;
	}
	void OnFailedConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}
	void OnConnectedToServer() {
		windiws = 2;
	}
	void OnDisconnectedFromServer() {
		windiws = 0;
	}

	void OnGUI() {
		if(windiws == 0) {
			GUI.Label(new Rect(Screen.width/2 - 100, 155, 110-40, 25), "IP:");
			GUI.Label(new Rect(Screen.width/2 + 25, 155, 110-40, 25), "Port:");
			ip = GUI.TextField(new Rect(Screen.width/2 - 80, 155, 110 - 10, 25), ip);
			port = GUI.TextField(new Rect(Screen.width/2 + 55, 155, 85-40, 25), port);

			if(GUI.Button(new Rect(Screen.width/2 - 100, 185, 200, 25), "Создать Сервер")) {
				Network.InitializeServer(5, int.Parse(port), true);
			}
			
			if(GUI.Button(new Rect(Screen.width/2 - 100, 215, 200, 25), "Подключиться")) {
				Network.Connect(ip, int.Parse(port));
			}			
			if(GUI.Button(new Rect(Screen.width/2 - 100, 245, 200, 25), "Выход")) {
				Application.Quit();
			}
		}

		if(windiws == 1) {
			GUI.Button (new Rect (Screen.width / 2 - 100, 155, 200, 25), "Подключено: " + (Network.connections.Length+1));
			if(GUI.Button(new Rect(Screen.width/2 - 100, 185, 200, 25), "Старт игры!")) {
				GetComponent<NetworkView>().RPC("LoadLevel", RPCMode.All);
			}

			if(GUI.Button(new Rect(Screen.width/2 - 100, 215, 200, 25), "Отключиться")) {
				Network.Disconnect();
				windiws = 0;
			}
		}
		
		if(windiws == 2) {
			GUI.Button (new Rect (Screen.width / 2 - 100, 155, 200, 25), "Всего подключились " + Network.connections.Length);
			//if(GUI.Button(new Rect(Screen.width/2 - 100, 185, 200, 25), "Старт")) {
			//	GetComponent<NetworkView>().RPC("LoadLevel", RPCMode.Others);
			//}
			if(GUI.Button(new Rect(Screen.width/2 - 100, 215, 200, 25), "Отключиться")) {
				Network.Disconnect();
				windiws = 0;
			}
		}
	}

	[RPC]
	void LoadLevel() {
		Application.LoadLevel("Game");
	}
}
