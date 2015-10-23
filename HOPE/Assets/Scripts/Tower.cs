using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public bool emitsLaser = true;
	public bool powered = false;

	public GameObject laser;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(powered==true) {
			gameObject.GetComponent<Renderer>().material.color = Color.red;
		}	
	}
}
