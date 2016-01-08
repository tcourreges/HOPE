using UnityEngine;
using System.Collections;

/*
GameController : manages mouse clicks depending on the current state (to instantiate Wall, Tower, etc).
*/

public class GameController : MonoBehaviour {

	public ControlStateMachine sm;
	public TerrainGenerator tg;

	private Floor lastFloor;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(lastFloor!=null)
			lastFloor.highlight();
		
		/*
		foreach(GameObject toto in GameObject.FindGameObjectsWithTag("Laser")) {
			toto.GetComponent<Laser>().updateLaser();
		}*/
		if(sm.getState() == controlState.simulation1) {
			spawnAgents();
			sm.setState(controlState.simulation2);
		}
		if(sm.getState() == controlState.simulation2)
			return;

		Floor f=tg.getFloor();
		if(f!=null) {

			if(sm.getState() != controlState.idle)
				f.highlight();

			if(Input.GetMouseButton (0)) {
				if(sm.getState() == controlState.wall) {
					f.createWall();
				}

				else if(sm.getState() == controlState.tower1) {
					f.createTower(towerType.tower1);
				}

				else if(sm.getState() == controlState.deleteWall) {
					f.deleteObject();
				}

				else if(sm.getState() == controlState.generator1) {
					lastFloor=f;
					sm.setState(controlState.generator2);
				}
				else if(sm.getState() == controlState.generator3) {
					lastFloor.createGenerator(f);					
					
					lastFloor = null;
					sm.setState(controlState.idle);
				}
			}	

			if(!Input.GetMouseButton(0)) {
				if(sm.getState() == controlState.generator2) {
					sm.setState(controlState.generator3);
				}
			}
		}

	
	}

	private void spawnAgents() {
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("AlienSpawn");
        
		foreach (GameObject a in spawns) {
			a.GetComponent<AlienSpawn>().spawnAlien();
			Destroy(a);
		}

		GameObject[] spawns2 = GameObject.FindGameObjectsWithTag("RobotSpawn");
        
		foreach (GameObject r in spawns2) {
			r.GetComponent<RobotSpawn>().spawnRobot();
			Destroy(r);
		}

	}
}
