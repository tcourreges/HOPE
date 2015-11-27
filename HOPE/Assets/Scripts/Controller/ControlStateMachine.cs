using UnityEngine;
using System.Collections;

/* All the interaction modes
*/
public enum controlState{
	idle, //default
	tower1, //tower selected
	wall, deleteWall

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
	}

	public void setState(controlState s) {
		currentState = s;
		print("changing state: "+currentState);
	}
	public controlState getState() {
		return currentState;
	}
}
