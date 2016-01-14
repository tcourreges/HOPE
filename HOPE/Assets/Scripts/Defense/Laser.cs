using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	private LineRenderer line;
	public GameObject origin, end;

	private Vector3 x0, xf;

	private float height=1;

	// Use this for initialization
	void Start () {
	}

	public void setOriginEnd(GameObject o, GameObject e) {
		origin = o; end = e;

		line=GetComponent<LineRenderer>();
		x0=origin.transform.position;
		xf=end.transform.position;

		x0.y=height;
		xf.y=height;

		Vector3 direction=xf-x0;
		direction.Normalize();
		xf=xf + direction*10000;
	}
	
	// Update is called once per frame
	void Update() {
		if(line==null)
			return;

		//if emitter is removed, delete the laser
		if(origin==null) {
			Destroy(transform.gameObject);
			return;
		}

		line.SetPosition(0, x0);
		line.SetPosition(1, x0);

		if(origin.GetComponent<Generator>().isPowered()) {
			line.SetPosition(0, x0);
			line.SetPosition(1, xf);
		
			RaycastHit[] hits = Physics.RaycastAll(x0, xf-x0);
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
				if(col.tag == "Wall" || col.tag == "Border") {
					line.SetPosition(1, hit.point);
					break;
				}
				if(col.tag == "Alien") {
					if(col.GetComponent<Alien>().transparent==false) {
						line.SetPosition(1, hit.point);
						break;
					}
				}
				if(col.tag == "Generator") {
					col.GetComponent<Generator>().power();
				}
			}
		}
		else {
			line.SetPosition(0, x0);
			line.SetPosition(1, x0);
		}



	}
}
