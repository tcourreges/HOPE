﻿using UnityEngine;
using System.Collections;

/*
	Generator class : instantiates a new Laser object
*/
public class Generator : MonoBehaviour {

	public bool transparent;
	public bool powered;
	private int updated;

	//public GameObject laser;

	// Use this for initialization
	void Start () {
		transparent=true;
		powered=false;
	}

	// Update is called once per frame
	void Update () {		
		//print(updated);

		if(updated>10) {
			powered=false;
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		updated++;
	}

	//Powers the tower during the next 10 frames
	public void power() {
		powered=true;
		gameObject.GetComponent<Renderer>().material.color = Color.red;

		updated=0;
	}

	public bool isPowered() {return powered;}
}
