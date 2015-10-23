using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	private LineRenderer line;
	public GameObject origin, end;

	private Vector3 x0, xi, xf;
	RaycastHit hit;

	void setOrigin(GameObject o) {origin = o;}
	void setEnd(GameObject e) {end = e;}

	// Use this for initialization
	void Start () {
		line=GetComponent<LineRenderer>();
		x0=origin.transform.position;
		xf=end.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		xi=xf;

		if(Physics.Raycast(x0, xf-x0, out hit)) {
			GameObject col=hit.collider.gameObject;
			xi=col.transform.position;
			if(col.tag == "Tower" ) {
				//col = tour touchée par le laser

				
			}
		}

		line.SetPosition(0, x0);	
		line.SetPosition(1, xi);
	}
}
