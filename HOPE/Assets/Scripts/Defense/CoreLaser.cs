﻿using UnityEngine;
using System.Collections;

/*
	Specific case of laser: does not need a generator to be powered
*/
public class CoreLaser : MonoBehaviour {

	private LineRenderer line;
	public GameObject core;
	public GameObject end;

	private Vector3 x0, xf;

	private float height=1;

	// Use this for initialization
	void Start () {
		line=GetComponent<LineRenderer>();
		x0=core.transform.position;
		xf=end.transform.position;

		x0.y=height;
		xf.y=height;

		Vector3 direction=xf-x0;
		direction.Normalize();
		xf=xf + direction*10000;
	}
	
	// Update is called once per frame
	void Update () {
		if(line==null)
			return;

		line.SetPosition(0, x0);
		line.SetPosition(1, x0);


		line.SetPosition(0, x0);
		line.SetPosition(1, xf);
	
		RaycastHit[] hits = Physics.RaycastAll(x0, xf-x0);
		//powers the towers/generators along the way, stops when a collider is not transparent
		for(int i=0; i<hits.Length; i++) {
			RaycastHit hit=hits[i];
			GameObject col=hit.collider.gameObject;
				if(col.tag == "Tower") {
					col.GetComponent<Tower>().power();
					if(col.GetComponent<Tower>().transparent==false) {
					line.SetPosition(1, hit.point);
					//line.SetPosition(1, x0 + Vector3.Project( col.transform.position - x0, xf - x0));
					break;
					}
			}
			if(col.tag == "Alien") {
				if(col.GetComponent<Alien>().transparent==false) {
					line.SetPosition(1, hit.point);
					col.GetComponent<Alien>().hitByLaser=10;
					break;
				}
			}
			if(col.tag == "Wall" || col.tag == "Border") {
				line.SetPosition(1, hit.point);
				break;
			}
			if(col.tag == "Generator") {
				col.GetComponent<Generator>().power();
			}
		}

	}
}
