using UnityEngine;
using System.Collections;

public class GUIManager : MonoBehaviour {

	public GameObject panelTextures, panelPause;
	public bool isPanelTex, isPause;

	private MouseLook playerMouse, cameraMouse;

	void Update () {
		if(GameObject.Find("Player") != null && playerMouse == null) {
			playerMouse = GameObject.Find("Player").GetComponent<MouseLook>();
			cameraMouse = GameObject.Find("Player").transform.Find("Main Camera").GetComponent<MouseLook>();
		}

		UpdatePanels();
		Mouse();
	}

	void UpdatePanels() {
		if(Input.GetKeyDown(KeyCode.Tab) && panelTextures != null) {
			GameObject.Find("World").GetComponent<Building>().isBuild = false;
			isPause = false;
			isPanelTex = !isPanelTex;
		}
		if(Input.GetKeyDown(KeyCode.Escape) && panelPause != null) {
			GameObject.Find("World").GetComponent<Building>().isBuild = false;
			isPanelTex = false;
			isPause = !isPause;
		}
		panelPause.SetActive(isPause);
		panelTextures.SetActive(isPanelTex);
	}

	void Mouse() {
		//Input.GetAxis("Mouse X")
		if (isPanelTex == false || isPause == false) {
			Cursor.visible = false;
			
			if (playerMouse != null)
				playerMouse.enabled = true;
			if (cameraMouse != null)
				cameraMouse.enabled = true;
		}
		if (isPanelTex == true || isPause == true) {
			Cursor.visible = true;
			
			if(playerMouse != null)
				playerMouse.enabled = false;
			if(cameraMouse != null)
				cameraMouse.enabled = false;
		}
	}

	public void Button(string name) {
		if(name == "Resume") {
			isPause = !isPause;
		}
		if(name == "Exit") {
			Network.Destroy(GameObject.Find("Player"));
			Network.Disconnect();
			Application.LoadLevel("Menu");
		}
	}
}
