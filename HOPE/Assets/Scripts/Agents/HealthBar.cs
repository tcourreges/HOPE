using UnityEngine;
using System.Collections;

/*
	displays the healthBar of an alien
*/
public class HealthBar : MonoBehaviour {

	private Alien alien;

	public void setAlien(Alien a) {
		alien=a;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (alien==null) {
			Destroy(transform.gameObject);
			return;
		}

		float percentage = (float)alien.GetComponent<Alien> ().getHealth () / alien.GetComponent<Alien> ().getHealthMax ();

		transform.position = alien.transform.position;
		transform.Translate(new Vector3(0,1,-1.5f));
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
		transform.GetChild (0).localScale = new Vector3(percentage, 1, 1);

		transform.GetChild (0).GetChild (0).GetComponent<SpriteRenderer> ().color = Color.Lerp (Color.red, Color.green, percentage);
	}
}
