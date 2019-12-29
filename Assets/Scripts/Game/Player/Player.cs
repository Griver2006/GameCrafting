﻿using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	public float sync;
	public Slider mouseSync;
	public GameObject camera, graphics;

	void Start() {
	}

	void Update() {
		if(mouseSync == null) {
			mouseSync = GameObject.Find("Pause").transform.Find("Slider").GetComponent<Slider>();
			mouseSync.maxValue = 15;
			mouseSync.minValue = 0;
		} else {
			sync = mouseSync.value;
		}

		if (GetComponent<NetworkView>().isMine) {
			gameObject.name = "Player";
			Destroy(graphics);
			gameObject.GetComponent<MouseLook>().sensitivityX = sync;
			camera.GetComponent<MouseLook>().sensitivityY = sync;
		} else {
			gameObject.name = "Client";
			Destroy(camera);
			gameObject.GetComponent<MouseLook>().enabled = false;
			gameObject.GetComponent<PlayerMove>().enabled = false;
		}
	}
}
