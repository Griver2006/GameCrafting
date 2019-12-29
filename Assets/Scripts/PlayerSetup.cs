using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] components;

	public Camera sceneCamera;

	// Use this for initialization
	void Start () 
	{
		if (!isLocalPlayer)
			for (int i = 0; i < components.Length; i++)
				components [i].enabled = false;
		else 
		{
			
			if (sceneCamera != null)
				sceneCamera.gameObject.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnDisable ()
	{
		if (sceneCamera != null)
			sceneCamera.gameObject.SetActive (true);
	}
}
