using UnityEngine;
using System.Collections;

/*
Selection
*/
public class Selection : MonoBehaviour {

	private int countdown;
// Use this for initialization
	void Start () {
		countdown = 5;
	}
	
	// Update is called once per frame
	void Update () {
		countdown--;
		if(countdown<0) {
			print("salut gars");
			Destroy(transform.gameObject);
		}
	}
}
