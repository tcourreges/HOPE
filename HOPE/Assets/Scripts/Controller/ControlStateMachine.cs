using UnityEngine;
using System.Collections;

/* All the interaction modes
*/
public enum controlState{
	idle, //default
	tower1, //tower selected
	wall, deleteWall,
	generator1, generator2, generator3,
	simulation1, simulation2
};

/*
StateMachine that keeps track of the current interaction mode (whether the user wants to create a wall or a tower, etc)
*/

public class ControlStateMachine : MonoBehaviour {

	private controlState currentState;

	// Use this for initialization
	void Start () {
		currentState = controlState.idle;
	}
	
	// Update is called once per frame
	void Update () {
		if(currentState!=controlState.simulation2) {
			if (Input.GetKeyDown("t"))
				setState(controlState.tower1);
			else if (Input.GetKeyDown("w"))
				setState(controlState.wall);
			else if (Input.GetKeyDown("x"))
				setState(controlState.deleteWall);
			else if (Input.GetKeyDown("g"))
				setState(controlState.generator1);
			else if (Input.GetKeyDown("s"))
				setState(controlState.simulation1);
		}
	}

	public void setState(controlState s) {
		currentState = s;
		print("changing state: "+currentState);
	}

	public controlState getState() {
		return currentState;
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
}
