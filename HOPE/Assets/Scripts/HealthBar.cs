using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	TextMesh tm;

	private Alien alien;

	public void setAlien(Alien a) {alien=a;}

	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = alien.transform.position;
		transform.Translate(new Vector3(0,1,0));

		transform.forward = Camera.main.transform.forward;


		tm.text = "";
		for(int i =0; i<alien.getHealth(); i++)
			tm.text +="x";
		for(int i=alien.getHealth(); i<alien.getHealthMax(); i++)
			tm.text +="-";
	}
}
