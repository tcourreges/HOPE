﻿using UnityEngine;
using System.Collections;

/*
	Robot agent
*/
public class Robot : MonoBehaviour {

	private NavMeshAgent agent;

	// Use this for initialization
	void Start () {
	
	}

	public void setDestination(float x, float y) {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = new Vector3 (x, 1.5f, y);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
