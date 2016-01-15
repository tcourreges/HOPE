using UnityEngine;
using System.Collections;

/*
	script to link a particle system to a parent and kill both together
*/
public class PoweredParticle : MonoBehaviour {
	private GameObject parent;

	void Update () {
		if (parent == null)
			Destroy (gameObject);
	}

	public void setParent(GameObject p) {
		parent = p;
	}
}
