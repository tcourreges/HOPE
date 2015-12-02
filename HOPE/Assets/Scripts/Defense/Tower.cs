using UnityEngine;
using System.Collections;

/* Types of towers
*/
public enum towerType{
	core,
	generator,
	tower1
};


/*
Tower class : can be powered, attacks aliens
*/
public class Tower : MonoBehaviour {

	public bool transparent;
	public bool powered;
	private int updated;
	public GameObject towerRangePrefab;
	private GameObject towerRange;

	private float range=4;
	private int damage=2;
	private int reload=60;

	private int currentReload;

	//public GameObject laser;

	// Use this for initialization
	void Start () {
		transparent=true;
		powered=false;
		currentReload = reload;
		showRange ();
	}
	
	// Update is called once per frame
	void Update () {
		if(updated>10) {
			powered=false;
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		updated++;

		currentReload++;

		if(powered && currentReload > reload) {
			GameObject alien = findClosestEnemy();
			if(alien != null)
				attack(alien.GetComponent<Alien>());
		}
	}

	//Powers the tower during the next 10 frames
	public void power() {
		powered=true;
		gameObject.GetComponent<Renderer>().material.color = Color.red;

		updated=0;
	}

	public void attack(Alien a) {
		print("attack");
		a.healthDown(damage);
		currentReload = 0;
	}


    private GameObject findClosestEnemy() {
		GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
		GameObject closest = null;

		float minDist = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject alien in aliens) { 
		    Vector3 diff = alien.transform.position - position;
			float dist = diff.x * diff.x + diff.z * diff.z;
		    if (dist < minDist && dist < range * range) {
		        closest = alien;
		        minDist = dist;
		    }
		}
		return closest;
	}

	private void showRange() {
		towerRange = (GameObject)Instantiate(		towerRangePrefab,
		            								new Vector3(transform.position.x, transform.position.y-1.49f, transform.position.z),
		            								Quaternion.identity
		            						);
		towerRange.GetComponent<TowerRange> ().initialize (range, gameObject);
	}

	public void deleteRange() {
		Destroy (towerRange);
	}
}
