using UnityEngine;
using System.Collections;

/*
	script to instantiate a ring of the right size (range of the tower) and link it to the tower
*/

public class TowerRange : MonoBehaviour {
	
	private GameObject tower;

	void Start () {
	
	}

	void Update() {
		if (tower == null) {
			Destroy(transform.gameObject);
			return;
		}
	}

	public void initialize (float range, GameObject _tower) {
		tower = _tower;
		transform.localScale = new Vector3(2 * range + 1, 0, 2 * range + 1);
	}
}
