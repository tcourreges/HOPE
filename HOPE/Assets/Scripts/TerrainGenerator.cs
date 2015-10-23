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

		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll (ray);

		for (int i = 0; i < hits.Length; i++) {

			RaycastHit hit = hits[i];

			if (Input.GetMouseButton (0)) {
				if (hit.transform.gameObject.tag == "Floor") {
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(hit.transform.position.x, 0, hit.transform.position.z);
					cube.transform.localScale = new Vector3(1,3,1);
				}
			}

			if (Input.GetMouseButton (1)) {
				if (hit.transform.gameObject.tag != "Floor") {
					Destroy (hit.transform.gameObject);
				}
			}
		}
	}
}
