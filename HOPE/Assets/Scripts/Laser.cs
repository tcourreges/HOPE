using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	private LineRenderer line;
	public GameObject origin, end;

	private Vector3 x0, xf;
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

		line.SetPosition(0, x0);
		line.SetPosition(1, xf);
		
		RaycastHit[] hits = Physics.RaycastAll(x0, xf-x0);

		for(int i=0; i<hits.Length; i++) {
			RaycastHit hit=hits[i];
			GameObject col=hit.collider.gameObject;

			if(col.tag == "Tower") {
				col.GetComponent<Tower>().power();

				if(col.GetComponent<Tower>().transparent==false) {
					line.SetPosition(1, x0 + Vector3.Project( col.transform.position - x0, xf - x0));
					break;
				}
			}
		}



	}
}
