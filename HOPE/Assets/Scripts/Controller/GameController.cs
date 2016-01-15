using UnityEngine;
using System.Collections;

/*
GameController : manages mouse clicks depending on the current state (to instantiate Wall, Tower, etc).
*/

public class GameController : MonoBehaviour {

	public ControlStateMachine sm;
	public TerrainGenerator tg;

	public int minerals;
	public static int wallCost = 50;
	public static int tower1Cost = 200;
	public static int tower2Cost = 210;
	public static int tower3Cost = 220;
	public static int generatorCost = 250;

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
		if(sm.getState() == controlState.simulation3) {
			killAgents();
			sm.setState(controlState.idle);
		}
		if(sm.getState() == controlState.simulation2)
			return;

		Floor f=tg.getFloor();
		if(f!=null && f.canEdit) {

			if(sm.getState() != controlState.idle)
				f.highlight();

			if(Input.GetMouseButton (0)) {
				if(sm.getState() == controlState.wall && minerals >= wallCost) {
					if (f.createWall())
						minerals -= wallCost;
				}

				else if(sm.getState() == controlState.tower1 && sm.getTowerType() == towerType.tower1 && minerals >= tower1Cost) {
					if(f.createTower(towerType.tower1))
						minerals -= tower1Cost;
				}

				else if(sm.getState() == controlState.tower1 && sm.getTowerType() == towerType.tower2 && minerals >= tower2Cost) {
					if(f.createTower(towerType.tower2))
						minerals -= tower2Cost;
				}

				else if(sm.getState() == controlState.tower1 && sm.getTowerType() == towerType.tower3 && minerals >= tower3Cost) {
					if(f.createTower(towerType.tower3))
						minerals -= tower3Cost;
				}

				else if(sm.getState() == controlState.deleteWall) {
					string tag = f.deleteObject();
					if (tag == "Wall") {
						minerals += wallCost;
					} else if (tag == "Tower1") {
						minerals += tower1Cost;
					} else if (tag == "Tower2") {
						minerals += tower2Cost;
					} else if (tag == "Tower3") {
						minerals += tower3Cost;
					} else if (tag == "Generator") {
						minerals += generatorCost;
					}
				}

				else if(sm.getState() == controlState.generator1 && minerals >= generatorCost) {
					lastFloor=f;
					sm.setState(controlState.generator2);
				}
				else if(sm.getState() == controlState.generator3 && minerals >= generatorCost) {
					if(lastFloor.createGenerator(f))
						minerals -= generatorCost;
					
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
			Component[] particleSystems = a.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem ps in particleSystems) {
				ps.enableEmission = false;
			}
		}

		GameObject[] spawns2 = GameObject.FindGameObjectsWithTag("RobotSpawn");
        
		foreach (GameObject r in spawns2) {
			r.GetComponent<RobotSpawn>().spawnRobot();
			Component[] particleSystems = r.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem ps in particleSystems) {
				ps.enableEmission = false;
			}
		}
	}

	private void killAgents() {
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("AlienSpawn");
		foreach (GameObject a in spawns) {
			Component[] particleSystems = a.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem ps in particleSystems) {
				ps.enableEmission = true;
			}
		}

		GameObject[] spawns2 = GameObject.FindGameObjectsWithTag("RobotSpawn");
		foreach (GameObject r in spawns2) {
			Component[] particleSystems = r.GetComponentsInChildren<ParticleSystem>();
			foreach (ParticleSystem ps in particleSystems) {
				ps.enableEmission = true;
			}
		}

		GameObject[] cores = GameObject.FindGameObjectsWithTag("Core");
		foreach (GameObject c in cores) {
			c.GetComponent<Core>().reset();
		}

		GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
		foreach (GameObject a in aliens) {
			Destroy(a);
		}

		GameObject[] robots = GameObject.FindGameObjectsWithTag("Robot");
		foreach (GameObject a in robots) {
			Destroy(a);
		}
	}

}
