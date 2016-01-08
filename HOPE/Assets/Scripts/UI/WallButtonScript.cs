using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WallButtonScript : MonoBehaviour {

	public ControlStateMachine controlStateMachine;
	public Button wallButton;
	public Sprite normalButton;
	public Sprite pressedButton;

	void Update () {
		if (controlStateMachine.getState () == controlState.wall) {
			wallButton.image.sprite = pressedButton;
		} else {
			wallButton.image.sprite = normalButton;
		}
	}
}
