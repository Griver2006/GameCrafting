using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {

	public Transform client;
	public Transform me;

	// Use this for initialization
	void Start () 
	{
		if (isLocalPlayer) 
		{
			client.gameObject.SetActive (true);
			me.gameObject.SetActive (false);
		} 
		else 
		{
			client.gameObject.SetActive (false);
			me.gameObject.SetActive (true);
		}
	}
}
