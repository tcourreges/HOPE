using UnityEngine;
using System.Collections;

/* All the interaction modes
*/
public enum controlState{
	idle, //default
	tower1, //tower selected
	wall, deleteWall,
	generator1, generator2, generator3
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
		if (Input.GetKeyDown("t"))
			setState(controlState.tower1);
		else if (Input.GetKeyDown("w"))
			setState(controlState.wall);
		else if (Input.GetKeyDown("x"))
			setState(controlState.deleteWall);
		else if (Input.GetKeyDown("g"))
			setState(controlState.generator1);
		print (currentState);
	}

	public void setState(controlState s) {
		currentState = s;
		print("changing state: "+currentState);
	}

	public controlState getState() {
		return currentState;
	}

	public void stateWall() { setState (controlState.wall); }
	public void stateTower1() { setState (controlState.tower1); }
	public void stateDelete() { setState (controlState.deleteWall); }
	public void stateGenerator() { setState (controlState.generator1); }
}
