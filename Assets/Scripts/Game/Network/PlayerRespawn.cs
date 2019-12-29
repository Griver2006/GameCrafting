using UnityEngine;
using System.Collections;

public class PlayerRespawn : MonoBehaviour {

	public Transform player;

	void Start () {
		Network.Instantiate(player, transform.position+new Vector3(Random.Range(-7.5f, 7.5f), 0, Random.Range(-7.5f, 7.5f)), transform.rotation, 0);
	}
}
