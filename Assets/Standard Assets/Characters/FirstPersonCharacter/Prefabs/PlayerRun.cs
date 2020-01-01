using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour {

	public GameObject model;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Walk");
		}

		if (Input.GetKeyUp (KeyCode.W)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.S)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Back");
		}

		if (Input.GetKeyUp (KeyCode.S)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.A)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Left");
		}

		if (Input.GetKeyUp (KeyCode.A)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.D)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Right");
		}

		if (Input.GetKeyUp (KeyCode.D)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Idle");
		}

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			model.GetComponent<Animator>().SetTrigger ("Jump");
		}
	}
}
