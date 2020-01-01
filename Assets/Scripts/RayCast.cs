using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour {

	public float distance = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.E)) 
		{
			Ray ray = Camera.main.ScreenPointToRay (new Vector2 (Screen.width / 2, Screen.height / 2));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, distance)) 
			{
				if (hit.collider.tag == "Door") 
				{
					hit.collider.GetComponent<Animator> ().SetTrigger ("Open");
				} 
				else 
				{
					hit.collider.GetComponent<Animator> ().SetTrigger ("Close");
				}
			}
		}
	}
}
