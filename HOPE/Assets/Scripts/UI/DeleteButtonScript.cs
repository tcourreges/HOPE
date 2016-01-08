using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeleteButtonScript : MonoBehaviour {
	
	public ControlStateMachine controlStateMachine;
	public Button deleteButton;
	public Sprite normalButton;
	public Sprite pressedButton;
	
	void Update () {
		if (controlStateMachine.getState () == controlState.deleteWall) {
			deleteButton.image.sprite = pressedButton;
		} else {
			deleteButton.image.sprite = normalButton;
		}
	}
}
