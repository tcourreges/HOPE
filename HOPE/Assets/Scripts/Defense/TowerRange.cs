using UnityEngine;
using System.Collections;

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
