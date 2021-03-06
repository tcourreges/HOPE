﻿using UnityEngine;
using System.Collections;

/*
TerrainGenerator class : instantiates all the Floor, and allows floor selection
*/

public class TerrainGenerator : MonoBehaviour {

	public int sizeOfMap=30;
	public GameObject floorPrefab;
	public GameObject wallPrefab;
	public Vector3 coreLocation;

	public Floor[,] floors;

	// Instantiate all the Floor on a sizeOfMap*sizeOfMap grid
	void Start () {
		floors=new Floor[100,100];
		for (int i = 0 ; i < sizeOfMap ; i++) {
			for (int j = 0 ; j < sizeOfMap ; j++) {
				GameObject cube = (GameObject)Instantiate(floorPrefab, new Vector3(-sizeOfMap/2 + i + 0.5f, 0, -sizeOfMap/2 + j + 0.5f), Quaternion.identity);
				cube.GetComponent<MeshRenderer>().enabled = false;
				floors[i,j] =cube.GetComponent<Floor>();
			}
		}
	}

	public Floor[,] getFloors() { 
	print("requesting floors");
	return floors;}

	void Update() {
	}

	//returns clicked Floor (iterates through raycast and returns first Floor element found)
	public Floor getFloor() {
		RaycastHit[] hits;
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		hits = Physics.RaycastAll (ray);

		for (int i = 0; i < hits.Length; i++) {
			RaycastHit hit = hits[i];

			if (hit.transform.gameObject.tag == "Floor") {
				Floor f = hit.transform.GetComponent<Floor>();
				return f;
			}
		}
		return null;
	}
}
