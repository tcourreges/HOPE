﻿using UnityEngine;
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
	public GameObject towerWallPrefab;
	private GameObject towerWall;
	public GameObject poweredParticlePrefab;
	private GameObject poweredParticle;

	private float range=4;
	private int damage=2;
	private int reload=60;

	private int currentReload;

	public GameObject projectilePrefab;

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
			Destroy (poweredParticle);
			light(0f);
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		updated++;

		currentReload++;

		if(powered && currentReload > reload) {
			GameObject alien = findClosestEnemy();
			if(alien != null)
				attack(alien);
		}

		//if(powered && towerWall==null) wallRange();
		//if(!powered && towerWall!=null) Destroy (towerWall); 
	}

	//Powers the tower during the next 10 frames
	public void power() {
		powered=true;
		light(2.8f);
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		if (poweredParticle == null) {
			poweredParticle = (GameObject)Instantiate (poweredParticlePrefab,
		                                          new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z),
		                                          Quaternion.identity
			);
		}
		updated=0;
	}

	public void attack(GameObject a) {
		a.GetComponent<Alien>().healthDown(damage);
		currentReload = 0;

		GameObject projectile = (GameObject) Instantiate(	projectilePrefab,
		            						new Vector3(a.transform.position.x, a.transform.position.y, a.transform.position.z),
		            						Quaternion.identity
		            				);
		projectile.GetComponent<LaserProjectile> ().setOriginEnd (transform.position, a.transform.position);		
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
		towerRange = (GameObject)Instantiate(	towerRangePrefab,
		            				new Vector3(transform.position.x, transform.position.y+0.01f, transform.position.z),
		            				Quaternion.identity
		            				);
		towerRange.GetComponent<TowerRange> ().initialize (range, gameObject);
	}

	private void wallRange() {
		towerWall = (GameObject)Instantiate(	towerWallPrefab,
		            				new Vector3(transform.position.x, transform.position.y+0.01f, transform.position.z),
		            				Quaternion.identity
		            				);
		//towerWall.GetComponent<TowerRange> ().initialize (range, gameObject);
		towerWall.transform.localScale = new Vector3(2 * range + 1, 1, 2 * range + 1);
	}

	public void deleteRange() {
		Destroy (towerRange);
	}

	public void light(float i) {
		Component[] lights = GetComponentsInChildren<Light>();
		foreach (Light l in lights) {
			l.intensity = i;
	        }
	}
}
