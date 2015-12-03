using UnityEngine;
using System.Collections;

public class LaserProjectile : MonoBehaviour {

	private Vector3 x0, xf;
	private int duration = 0;	

	// Use this for initialization
	void Start () {
	}

	public void setOriginEnd(Vector3 origin, Vector3 end) {
		x0 = origin; xf = end;

		LineRenderer line=GetComponent<LineRenderer>();
		line.SetPosition(0, x0);
		line.SetPosition(1, xf);
		duration=2;
	}
	
	// Update is called once per frame
	void Update() {
		duration--;
		if(duration<0)
			Destroy(transform.gameObject);
	}
}
