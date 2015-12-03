using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

	private NavMeshAgent agent;
	private int cpt = 0;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		agent.destination = new Vector3 (3, 1.5f, 5);
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 diff = transform.position - agent.destination;
		float dist = diff.x * diff.x + diff.z * diff.z;

		if (dist < 0.1 && cpt == 0) {
			agent.destination = new Vector3 (1, 1.5f, -1);
			cpt++;
		} else if (dist < 0.1 && cpt == 1) {
			agent.destination = new Vector3 (-10.5f, 1.5f, -10.5f);
			cpt++;
		}
	}
}
