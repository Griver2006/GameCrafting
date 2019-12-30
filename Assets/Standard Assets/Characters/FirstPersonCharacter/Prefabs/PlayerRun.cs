using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Walk");
		}

		if (Input.GetKeyUp (KeyCode.W)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.S)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Back");
		}

		if (Input.GetKeyUp (KeyCode.S)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Left");
		}

		if (Input.GetKeyUp (KeyCode.A)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.D)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Right");
		}

		if (Input.GetKeyUp (KeyCode.D)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			gameObject.GetComponent<Animator>().SetTrigger ("Jump");
		}
	}
}
