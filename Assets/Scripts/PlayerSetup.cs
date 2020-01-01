using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] components;


	// Use this for initialization
	void Start () 
	{
		if (!isLocalPlayer)
			for (int i = 0; i < components.Length; i++)
				components [i].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
