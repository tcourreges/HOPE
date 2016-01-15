using UnityEngine;
using System.Collections;

/*
	Rotates the camera in the menu
*/
public class MenuCamera : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
		transform.Rotate(Vector3.up, 0.05f);
		transform.Rotate(Vector3.left, 0.02f);
	}
}
