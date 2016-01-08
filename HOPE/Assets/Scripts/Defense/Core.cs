using UnityEngine;
using System.Collections;

class Core : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4.0f);
			int i = 0;
			while (i < hitColliders.Length) {			
				if(hitColliders[i].tag == "Alien") {
					Destroy(hitColliders[i].gameObject);
					print("alien near the core");
				}
				i++;
			}

	}

}
