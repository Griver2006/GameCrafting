using UnityEngine;
using UnityEngine.UI;

public class GUIMenu : MonoBehaviour {

	public InputField ip, port;
	public GameObject panelMenu, panelMyltiplayer, panelServer, panelClient;

	void Update() {
		if(panelServer.activeInHierarchy == true) {
			Text connections = panelServer.transform.Find("Connections").GetComponent<Text>();
			connections.text = "Подключилось: " + (Network.connections.Length+1);
		}
		if(panelClient.activeInHierarchy == true) {
			Text connections = panelClient.transform.Find("Connections").GetComponent<Text>();
			connections.text = "Подключилось: " + (Network.connections.Length+1);
		}
	}

	public void CreateServer() {
		Network.InitializeServer(5, int.Parse(port.text), true);
	}

	public void Connect() {
		if(ip.text != "" && port.text != "") {
			Network.Connect(ip.text, int.Parse(port.text));
		}
	}	

	void OnServerInitialized() {
		panelMenu.SetActive(false);
		panelMyltiplayer.SetActive(false);
		panelServer.SetActive(true);
		panelClient.SetActive(false);
	}

	void OnFailedConnect(NetworkConnectionError error) {
		Debug.Log("Could not connect to server: " + error);
	}

	void OnConnectedToServer() {
		panelMenu.SetActive(false);
		panelMyltiplayer.SetActive(false);
		panelServer.SetActive(false);
		panelClient.SetActive(true);
	}

	public void Disconnect() {
		Network.Disconnect();
	}
	void OnDisconnectedFromServer() {
		panelMenu.SetActive(true);
		panelMyltiplayer.SetActive(false);
		panelServer.SetActive(false);
		panelClient.SetActive(false);
	}

	public void StartRoom() {
		GetComponent<NetworkView>().RPC("LoadLevel", RPCMode.All);
	}

	public void Exit() {
		Application.Quit();
	}
	
	[RPC]
	void LoadLevel() {
		Application.LoadLevel("Game");
	}
}
