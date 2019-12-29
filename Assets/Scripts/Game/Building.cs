using UnityEngine;
using System.Collections;

public class Building : MonoBehaviour {
	
	public GameObject[] objects;
	public bool isBuild;
	public Material matBuild;
	public Texture mainTextureGO;
	public LayerMask lm;
	
	private GameObject Fantom, selectObject;
	private Material matSelectObject;
	private GameObject[] foundations;
	private GameObject gameAllPlayer;
	
	void Update () {
		foundations = GameObject.FindGameObjectsWithTag("Ground");
		for (byte n = 0; n < objects.Length; n++) {
			if (Input.GetKeyDown("" + n)) {
				if(objects[n] != null) {
					isBuild = !isBuild;
					selectObject = objects[n];
				}
			}
		}
		FantomControl();
		CreateObject();
		UpdateBuild();
	}
	
	void UpdateBuild() {
			RaycastHit hit = new RaycastHit ();
			Ray ray = Camera.main.ScreenPointToRay (new Vector3 (Screen.width / 2, Screen.height / 2, 0));
			
		if(Physics.Raycast(ray, out hit, 8f, lm)) {
			if (isBuild == true && Fantom != null) {
				float height = hit.point.y+(Fantom.transform.localScale.y/2);
				if(Fantom.tag == "Ground") {
					FoundationControl("Ground");
					if(hit.collider.tag == "Ground") {
						Fantom.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.red.r,Color.red.g,Color.red.b, Color.red.a/2f);
					}
					if(hit.collider.name == "GroundFantom") {	
						Fantom.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z);
					} else {
						Fantom.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
					}
					Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.green.r,Color.green.g,Color.green.b, Color.green.a/2f);
				} else {
					Fantom.transform.position = new Vector3(hit.point.x, height, hit.point.z);
				}
				
				if(Fantom.tag == "Bulk") {
					FoundationControl("Bulk");
					if(hit.collider.name == "BulkFantom") {	
						Fantom.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y+Fantom.transform.localScale.y/2f, hit.collider.transform.position.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.green.r,Color.green.g,Color.green.b, Color.green.a/2f);
					} else {
						Fantom.transform.position = new Vector3(hit.point.x, height, hit.point.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.red.r,Color.red.g,Color.red.b, Color.red.a/2f);
					}
				}
				
				
				if(Fantom.tag == "Wall") {
					FoundationControl("Wall");
					if(hit.collider.name == "WallFantom") {	
						Fantom.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y+Fantom.transform.localScale.y/2f, hit.collider.transform.position.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.green.r,Color.green.g,Color.green.b, Color.green.a/2f);
					} else {
						Fantom.transform.position = new Vector3(hit.point.x, height, hit.point.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.red.r,Color.red.g,Color.red.b, Color.red.a/2f);
					}
				}
				if(Fantom.tag == "Doorway") {
					FoundationControl("Doorway");
					if(hit.collider.name == "WallFantom") {	
						Fantom.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y+Fantom.transform.localScale.y*2, hit.collider.transform.position.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.green.r,Color.green.g,Color.green.b, Color.green.a/2f);
					} else {
						Fantom.transform.position = new Vector3(hit.point.x, hit.point.y+Fantom.transform.localScale.y*2, hit.point.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.red.r,Color.red.g,Color.red.b, Color.red.a/2f);
					}
				}
				
				if(Fantom.tag == "Ladder") {
					FoundationControl("Ladder");
					if(hit.collider.name == "LadderFantom") {	
						Fantom.transform.position = new Vector3(hit.collider.transform.position.x-Fantom.transform.localScale.x/1.5f, hit.collider.transform.position.y-Fantom.transform.localScale.y*1.5f, hit.collider.transform.position.z+Fantom.transform.localScale.z*1.6f);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.green.r,Color.green.g,Color.green.b, Color.green.a/2f);
					} else {
						Fantom.transform.position = new Vector3(hit.point.x, hit.point.y-Fantom.transform.localScale.y*2, hit.point.z);
						Fantom.GetComponent<MeshRenderer>().material.color = new Color(Color.red.r,Color.red.g,Color.red.b, Color.red.a/2f);
					}
				}
			}
			if(hit.collider.gameObject.tag == "Ground" || hit.collider.gameObject.tag == "Bulk" || hit.collider.gameObject.tag == "Wall" || hit.collider.gameObject.tag == "Doorway" || hit.collider.gameObject.tag == "Ladder") {
				if(Input.GetKeyDown(KeyCode.F1)) {
					Network.Destroy(hit.collider.gameObject.GetComponent<NetworkView>().viewID);
				}
			}
		}
	}
	
	void CreateObject() {
		if(selectObject != null && isBuild == true && Fantom.GetComponent<MeshRenderer>().material.color == new Color(Color.green.r,Color.green.g,Color.green.b, Color.green.a/2f)) {
			for(byte i = 0; i < objects.Length; i++) {
				if(objects[i].tag == selectObject.tag) {
					if(Input.GetMouseButtonDown(0)) { 
						objects[i].GetComponent<Collider>().enabled = true;
						objects[i].transform.position = Fantom.transform.position;
						objects[i].transform.rotation = Fantom.transform.rotation;
						print("+");
						Network.Instantiate(objects[i], objects[i].transform.position, objects[i].transform.rotation, 0);
						//isBuild = false;
					}
				}
			}
		}
	}
	
	void FantomControl() {
		if(Fantom == null && selectObject != null) {
			Fantom = Instantiate(selectObject, Vector3.zero, Quaternion.identity) as GameObject;
			Fantom.name = selectObject.name;
			Fantom.GetComponent<Collider>().enabled = false;
			matSelectObject = Fantom.GetComponent<MeshRenderer>().material;
			Fantom.GetComponent<MeshRenderer>().material = matBuild;
		}
		
		if(Input.GetKeyDown(KeyCode.R) && Fantom.tag != "Ladder") {
			if(Fantom.transform.rotation == Quaternion.identity) {
				Fantom.transform.rotation = Quaternion.Euler(0,90,0);
			} else {
				Fantom.transform.rotation = Quaternion.Euler(0,0,0);
			}
		}
		
		if(isBuild == false && Fantom != null) {
			Destroy(Fantom);
			FoundationControl("None");
		}
	}
	
	void FoundationControl(string buildName) {
		if(buildName == "Ground") {
			for(byte i = 0; i < foundations.Length; i++) {
				foundations[i].transform.GetChild(0).gameObject.SetActive(false);
				foundations[i].transform.GetChild(1).gameObject.SetActive(false);
				foundations[i].transform.GetChild(2).gameObject.SetActive(false);
				foundations[i].transform.GetChild(3).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(4).gameObject.SetActive(false);
				foundations[i].transform.GetChild(5).gameObject.SetActive(false);
				foundations[i].transform.GetChild(6).gameObject.SetActive(false);
				foundations[i].transform.GetChild(7).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(8).gameObject.SetActive(true);
				foundations[i].transform.GetChild(9).gameObject.SetActive(true);
				foundations[i].transform.GetChild(10).gameObject.SetActive(true);
				foundations[i].transform.GetChild(11).gameObject.SetActive(true);
				foundations[i].transform.GetChild(12).gameObject.SetActive(true);
				foundations[i].transform.GetChild(13).gameObject.SetActive(true);
				foundations[i].transform.GetChild(14).gameObject.SetActive(true);
				foundations[i].transform.GetChild(15).gameObject.SetActive(true);
				foundations[i].transform.GetChild(16).gameObject.SetActive(true);
				
				foundations[i].transform.GetChild(17).gameObject.SetActive(false);
			}
		}
		
		if(buildName == "Bulk") {
			for(byte i = 0; i < foundations.Length; i++) {
				foundations[i].transform.GetChild(0).gameObject.SetActive(true);
				foundations[i].transform.GetChild(1).gameObject.SetActive(true);
				foundations[i].transform.GetChild(2).gameObject.SetActive(true);
				foundations[i].transform.GetChild(3).gameObject.SetActive(true);
				
				foundations[i].transform.GetChild(4).gameObject.SetActive(false);
				foundations[i].transform.GetChild(5).gameObject.SetActive(false);
				foundations[i].transform.GetChild(6).gameObject.SetActive(false);
				foundations[i].transform.GetChild(7).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(8).gameObject.SetActive(false);
				foundations[i].transform.GetChild(9).gameObject.SetActive(false);
				foundations[i].transform.GetChild(10).gameObject.SetActive(false);
				foundations[i].transform.GetChild(11).gameObject.SetActive(false);
				foundations[i].transform.GetChild(12).gameObject.SetActive(false);
				foundations[i].transform.GetChild(13).gameObject.SetActive(false);
				foundations[i].transform.GetChild(14).gameObject.SetActive(false);
				foundations[i].transform.GetChild(15).gameObject.SetActive(false);
				foundations[i].transform.GetChild(16).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(17).gameObject.SetActive(false);
			}
		}
		
		if(buildName == "Wall") {
			for(byte i = 0; i < foundations.Length; i++) {
				foundations[i].transform.GetChild(0).gameObject.SetActive(false);
				foundations[i].transform.GetChild(1).gameObject.SetActive(false);
				foundations[i].transform.GetChild(2).gameObject.SetActive(false);
				foundations[i].transform.GetChild(3).gameObject.SetActive(false);
				
				if(Fantom.transform.rotation == Quaternion.Euler(0,0,0)) {
					foundations[i].transform.GetChild(4).gameObject.SetActive(true);
					foundations[i].transform.GetChild(5).gameObject.SetActive(true);
					foundations[i].transform.GetChild(6).gameObject.SetActive(false);
					foundations[i].transform.GetChild(7).gameObject.SetActive(false);
				}
				if(Fantom.transform.rotation == Quaternion.Euler(0,90,0)) {
					foundations[i].transform.GetChild(4).gameObject.SetActive(false);
					foundations[i].transform.GetChild(5).gameObject.SetActive(false);
					foundations[i].transform.GetChild(6).gameObject.SetActive(true);
					foundations[i].transform.GetChild(7).gameObject.SetActive(true);
				}
				
				foundations[i].transform.GetChild(8).gameObject.SetActive(false);
				foundations[i].transform.GetChild(9).gameObject.SetActive(false);
				foundations[i].transform.GetChild(10).gameObject.SetActive(false);
				foundations[i].transform.GetChild(11).gameObject.SetActive(false);
				foundations[i].transform.GetChild(12).gameObject.SetActive(false);
				foundations[i].transform.GetChild(13).gameObject.SetActive(false);
				foundations[i].transform.GetChild(14).gameObject.SetActive(false);
				foundations[i].transform.GetChild(15).gameObject.SetActive(false);
				foundations[i].transform.GetChild(16).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(17).gameObject.SetActive(false);
			}
		}
		
		if(buildName == "Doorway") {
			for(byte i = 0; i < foundations.Length; i++) {
				foundations[i].transform.GetChild(0).gameObject.SetActive(false);
				foundations[i].transform.GetChild(1).gameObject.SetActive(false);
				foundations[i].transform.GetChild(2).gameObject.SetActive(false);
				foundations[i].transform.GetChild(3).gameObject.SetActive(false);
				
				if(Fantom.transform.rotation == Quaternion.Euler(0,90,0)) {
					foundations[i].transform.GetChild(4).gameObject.SetActive(true);
					foundations[i].transform.GetChild(5).gameObject.SetActive(true);
					foundations[i].transform.GetChild(6).gameObject.SetActive(false);
					foundations[i].transform.GetChild(7).gameObject.SetActive(false);
				}
				if(Fantom.transform.rotation == Quaternion.Euler(0,0,0)) {
					foundations[i].transform.GetChild(4).gameObject.SetActive(false);
					foundations[i].transform.GetChild(5).gameObject.SetActive(false);
					foundations[i].transform.GetChild(6).gameObject.SetActive(true);
					foundations[i].transform.GetChild(7).gameObject.SetActive(true);
				}
				
				foundations[i].transform.GetChild(8).gameObject.SetActive(false);
				foundations[i].transform.GetChild(9).gameObject.SetActive(false);
				foundations[i].transform.GetChild(10).gameObject.SetActive(false);
				foundations[i].transform.GetChild(11).gameObject.SetActive(false);
				foundations[i].transform.GetChild(12).gameObject.SetActive(false);
				foundations[i].transform.GetChild(13).gameObject.SetActive(false);
				foundations[i].transform.GetChild(14).gameObject.SetActive(false);
				foundations[i].transform.GetChild(15).gameObject.SetActive(false);
				foundations[i].transform.GetChild(16).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(17).gameObject.SetActive(false);
			}
		}
		
		if(buildName == "Ladder") {
			for(byte i = 0; i < foundations.Length; i++) {
				foundations[i].transform.GetChild(0).gameObject.SetActive(false);
				foundations[i].transform.GetChild(1).gameObject.SetActive(false);
				foundations[i].transform.GetChild(2).gameObject.SetActive(false);
				foundations[i].transform.GetChild(3).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(4).gameObject.SetActive(false);
				foundations[i].transform.GetChild(5).gameObject.SetActive(false);
				foundations[i].transform.GetChild(6).gameObject.SetActive(false);
				foundations[i].transform.GetChild(7).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(8).gameObject.SetActive(false);
				foundations[i].transform.GetChild(9).gameObject.SetActive(false);
				foundations[i].transform.GetChild(10).gameObject.SetActive(false);
				foundations[i].transform.GetChild(11).gameObject.SetActive(false);
				foundations[i].transform.GetChild(12).gameObject.SetActive(false);
				foundations[i].transform.GetChild(13).gameObject.SetActive(false);
				foundations[i].transform.GetChild(14).gameObject.SetActive(false);
				foundations[i].transform.GetChild(15).gameObject.SetActive(false);
				foundations[i].transform.GetChild(16).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(17).gameObject.SetActive(true);
			}
		}
		
		if (buildName == "None") {
			for(byte i = 0; i < foundations.Length; i++) {
				foundations[i].transform.GetChild(0).gameObject.SetActive(false);
				foundations[i].transform.GetChild(1).gameObject.SetActive(false);
				foundations[i].transform.GetChild(2).gameObject.SetActive(false);
				foundations[i].transform.GetChild(3).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(4).gameObject.SetActive(false);
				foundations[i].transform.GetChild(5).gameObject.SetActive(false);
				foundations[i].transform.GetChild(6).gameObject.SetActive(false);
				foundations[i].transform.GetChild(7).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(8).gameObject.SetActive(false);
				foundations[i].transform.GetChild(9).gameObject.SetActive(false);
				foundations[i].transform.GetChild(10).gameObject.SetActive(false);
				foundations[i].transform.GetChild(11).gameObject.SetActive(false);
				foundations[i].transform.GetChild(12).gameObject.SetActive(false);
				foundations[i].transform.GetChild(13).gameObject.SetActive(false);
				foundations[i].transform.GetChild(14).gameObject.SetActive(false);
				foundations[i].transform.GetChild(15).gameObject.SetActive(false);
				foundations[i].transform.GetChild(16).gameObject.SetActive(false);
				
				foundations[i].transform.GetChild(17).gameObject.SetActive(false);
			}
		}
	}

	public void BuildingTexture(Texture tex) {
		mainTextureGO = tex;
	}
}
