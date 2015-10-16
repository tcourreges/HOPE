using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public GameObject towerPrefab;

	private GameObject currentTower;

	void OnMouseUpAsButton() {
		if(currentTower == null) {
			print("creating tower");
			currentTower= (GameObject)Instantiate(towerPrefab);
			currentTower.transform.position = transform.position + Vector3.up;
		}
		else {
			print("already a tower");
		}
	}
}
