using UnityEngine;
using System.Collections;


public class Obstacle : MonoBehaviour {

	bool done=false;
	// Instantiate all the Floor on a sizeOfMap*sizeOfMap grid
	void Start () {
		
	}

	void Update() {
		if(done)
			return;
		GameObject f=findClosestFloor();
		if(f!=null) {
			done=true;
			f.GetComponent<Floor>().canEdit=false;
			f.GetComponent<Floor>().has=terrain.obstacle;
			gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}

	private GameObject findClosestFloor() {
		GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
		GameObject closest = null;

		float minDist = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject f in floors) { 
		    Vector3 diff = f.transform.position - position;
			float dist = diff.x * diff.x + diff.z * diff.z;
		    if (dist < minDist) {
		        closest = f;
		        minDist = dist;
		    }
		}
		return closest;
	}
}
