using UnityEngine;
using System.Collections;

public class TerrainGenerator : MonoBehaviour {

	public int sizeOfMap;

	// Use this for initialization
	void Start () {
		for (int i = 0 ; i < sizeOfMap ; i++) {
			for (int j = 0 ; j < sizeOfMap ; j++) {
				for (int k = 0 ; k < 3 ; k++) {
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.position = new Vector3(-sizeOfMap/2 + i, -1 + k, -sizeOfMap/2 + j);
					cube.transform.localScale = new Vector3(1,1,1);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
