using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TowerButtonScript : MonoBehaviour {
	
	public ControlStateMachine controlStateMachine;
	public Button towerButton;
	public Sprite normalButton;
	public Sprite pressedButton;
	
	void Update () {
		if (controlStateMachine.getState () == controlState.tower1) {
			towerButton.image.sprite = pressedButton;
		} else {
			towerButton.image.sprite = normalButton;
		}
	}
}
