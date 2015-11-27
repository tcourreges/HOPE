using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	public int sizeOfMap;
	public GameObject prefab;
	public GameObject wall;

	// Use this for initialization
	void Start () {
		for (int i = 0 ; i < sizeOfMap ; i++) {
			for (int j = 0 ; j < sizeOfMap ; j++) {
				GameObject cube = (GameObject)Instantiate(prefab, new Vector3(-sizeOfMap/2 + i + 0.5f, -1, -sizeOfMap/2 + j + 0.5f), Quaternion.identity);
				cube.GetComponent<MeshRenderer>().enabled = false;
			}
		}
		/*
		GameObject map = GameObject.CreatePrimitive (PrimitiveType.Cube);
		map.transform.position = new Vector3 (0, 0, 0);
		map.transform.localScale = new Vector3 (sizeOfMap, 1, sizeOfMap);
		*/
	}

	void Update() {

		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll (ray);

		for (int i = 0; i < hits.Length; i++) {

			RaycastHit hit = hits[i];

			if (Input.GetMouseButton (0)) {
				if (hit.transform.gameObject.tag == "Floor" && hit.transform.GetComponent<Floor>().isEmpty) {
					GameObject wallObject;
					wallObject = (GameObject)Instantiate(wall, new Vector3(hit.transform.position.x, 1, hit.transform.position.z), Quaternion.identity);
					wallObject.transform.GetComponent<Wall>().floor = hit.collider.gameObject;
					wallObject.tag = "Wall";
					hit.transform.GetComponent<Floor>().isEmpty = false;
				}
			}

			if (Input.GetMouseButton (1)) {
				if (hit.transform.gameObject.tag == "Wall") {
					hit.transform.GetComponent<Wall>().floor.transform.GetComponent<Floor>().isEmpty = true;
					Destroy (hit.transform.gameObject);
				}
			}
		}
	}
}
