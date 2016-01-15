using UnityEngine;
using System.Collections;

class Core : MonoBehaviour {

	public int nbAliens0=0, nbRobots0=0;
	private int nbAliens, nbRobots;

	public ControlStateMachine cSM;

	public int countdown0=0;
	public int countdownR0=0;
	private int countdown;
	private int countdownR;
	private bool over;
	private bool started;

	public UIController UI;

	// Use this for initialization
	void Start () {
		reset();
	
	}
	
	public void reset() {
		nbRobots=nbRobots0;
		nbAliens=nbAliens0;
		countdown=countdown0;
		countdownR=countdownR0;
		over=false;
		started=false;
	}

	private void win() {
		UI.win ();
		over=true;
		started=false;
	}

	private void lose() {
		UI.lose ();
		over=true;
		started=false;
	}
	
	// Update is called once per frame
	void Update () {
		if(over)
			return;

		Collider[] hitColliders = Physics.OverlapSphere(transform.position, 4.0f);
			if(started)
				countdownR--;
			if(nbRobots>0 && countdownR<0)
				lose();

			nbAliens = 0;
			GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
			foreach (GameObject a in aliens)
				nbAliens++;

			if(nbAliens>0)
				started=true;

			if(nbRobots == 0 && nbAliens == 0 && cSM.getState () != controlState.idle)
				win();

			if(nbRobots == 0)
				countdown--;

			if(countdown<0)
				win();

			int i = 0;
			while (i < hitColliders.Length) {
				if(hitColliders[i].tag == "Robot") {
					Destroy(hitColliders[i].gameObject);
					nbRobots--;
				}
			
				if(hitColliders[i].tag == "Alien") {
					Destroy(hitColliders[i].gameObject);
					lose();
				}
				i++;
			}
	}

}
