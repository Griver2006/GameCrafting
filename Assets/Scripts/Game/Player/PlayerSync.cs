using UnityEngine;
using System.Collections;

public class PlayerSync : MonoBehaviour {

	private Vector3 lastPos;
	private Quaternion lasrRot;
	private Transform myTransform;

	void Start() {
		if (GetComponent<NetworkView>().isMine) {
			myTransform = transform;
		} else {
			enabled = false;
		}
	}

	void Update() {
		if (Vector3.Distance (myTransform.position, lastPos) >= 0.5f) {
			lastPos = myTransform.position;
			GetComponent<NetworkView>().RPC("UpdateMovement", RPCMode.OthersBuffered, myTransform.position, myTransform.rotation);
		}
		if(Quaternion.Angle(myTransform.rotation, lasrRot) >= 1) {
			lasrRot = myTransform.rotation;
			GetComponent<NetworkView>().RPC("UpdateMovement", RPCMode.OthersBuffered, myTransform.position, myTransform.rotation);
		}
	}

	[RPC]
	void UpdateMovement(Vector3 newPosition, Quaternion newRotation) {
		transform.position = newPosition;
		transform.rotation = newRotation;
	}
}
