﻿using UnityEngine;
using System.Collections;

public class Alien : MonoBehaviour {


	private int healthMax=3;
	private int health;

	private HealthBar healthBar;
	public HealthBar healthBarPrefab;

	// Use this for initialization
	void Start () {
		health=healthMax;

		healthBar = (HealthBar)Instantiate(	healthBarPrefab,
						new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
						Quaternion.identity
			  		);
		healthBar.setAlien(this);
	}
	
	// Update is called once per frame
	void Update () {
		move();
		//print(health);

	}

	private void move() {
		transform.Translate(Vector3.left * 0.01f);
		//healthBar.transform.Translate(Vector3.left * 0.01f);
	}

	public void healthDown(int i) {
		health -= i;
		if(health < 1)
			die();
	}

	private void die() {
		Destroy(transform.gameObject);
		Destroy(healthBar.transform.gameObject);
	}

	public int getHealth() {return health;}
	public int getHealthMax() {return healthMax;}
}
