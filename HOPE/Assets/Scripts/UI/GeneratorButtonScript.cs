using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GeneratorButtonScript : MonoBehaviour {
	
	public ControlStateMachine controlStateMachine;
	public Button generatorButton;
	public Sprite normalButton;
	public Sprite pressedButton;
	
	void Update () {
		if (controlStateMachine.getState () == controlState.generator1 || controlStateMachine.getState () == controlState.generator2 || controlStateMachine.getState () == controlState.generator3) {
			generatorButton.image.sprite = pressedButton;
		} else {
			generatorButton.image.sprite = normalButton;
		}
	}
}
