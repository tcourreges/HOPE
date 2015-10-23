using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	public int sizeOfMap;
	public Transform prefab;

	// Use this for initialization
	void Start () {
		for (int i = 0 ; i < sizeOfMap ; i++) {
			for (int j = 0 ; j < sizeOfMap ; j++) {
				GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.position = new Vector3(-sizeOfMap/2 + i, -1, -sizeOfMap/2 + j);
				cube.transform.localScale = new Vector3(1,1,1);
				cube.tag = "Floor";
			}
		}
	}

	void Update() {
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Input.GetMouseButton (1)) {
			if (Physics.Raycast(ray, out hit)) {
				if (hit.transform.gameObject.tag == "Floor") {
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
					cube.transform.localScale = new Vector3(1,3,1);
				}
			}
		}
	}
}
