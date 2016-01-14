using UnityEngine;
using System.Collections;

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
