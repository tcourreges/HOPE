using UnityEngine;
using System.Collections;

/* All the interaction modes
*/
public enum controlState{
	idle, //default
	tower1, //tower selected
	wall, deleteWall,
	generator1, generator2, generator3,
	simulation1, simulation2, simulation3
};

/*
StateMachine that keeps track of the current interaction mode (whether the user wants to create a wall or a tower, etc)
*/

public class ControlStateMachine : MonoBehaviour {

	private controlState currentState;
	private towerType currentType;

	// Use this for initialization
	void Start () {
		currentState = controlState.idle;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentState!=controlState.simulation2 && Input.GetKeyDown("s")) {
			setState(controlState.simulation1);
		}
	}

	public void setState(controlState s) {
		currentState = s;
		print("changing state: "+currentState);
	}

	public void setTowerType(towerType t) {
		currentType = t;
	}

	public controlState getState() {
		return currentState;
	}

	public towerType getTowerType() {
		return currentType;
	}

	public void stateWall() {
		if (currentState == controlState.wall) {
			setState (controlState.idle);
		} else {
			setState (controlState.wall);
		}
	}

	public void stateTower1() {
		if (currentState == controlState.tower1) {
			setState (controlState.idle);
		} else {
			setState (controlState.tower1);
			setTowerType(towerType.tower1);
		}
	}

	public void stateTower2() {
		if (currentState == controlState.tower1) {
			setState (controlState.idle);
		} else {
			setState (controlState.tower1);
			setTowerType(towerType.tower2);
		}
	}

	public void stateTower3() {
		if (currentState == controlState.tower1) {
			setState (controlState.idle);
		} else {
			setState (controlState.tower1);
			setTowerType(towerType.tower3);
		}
	}

	public void stateDelete() {
		if (currentState == controlState.deleteWall) {
			setState (controlState.idle);
		} else {
			setState (controlState.deleteWall);
		}
	}

	public void stateGenerator() {
		if (currentState == controlState.generator1) {
			setState (controlState.idle);
		} else {
			setState (controlState.generator1);
		}
	}

	public void stateSimulation1() {
		setState (controlState.simulation1);
	}

	public void stateSimulation3() {
		if (currentState == controlState.simulation2) {
			setState (controlState.idle);
		}
	}
}
