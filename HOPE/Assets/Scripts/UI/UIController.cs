using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public ControlStateMachine controlStateMachine;
	public Toggle generatorToggle;

	private GameObject[] buttons;
	private GameObject[] toggles;

	void Start () {
		buttons = GameObject.FindGameObjectsWithTag ("Button");
		toggles = GameObject.FindGameObjectsWithTag ("Toggle");
	}

	void Update () {
		if (controlStateMachine.getState () == controlState.generator2) {
			generatorToggle.isOn = false;
		}
		if (controlStateMachine.getState () == controlState.simulation1 || controlStateMachine.getState () == controlState.simulation2) {
			foreach (GameObject b in buttons) {
				b.GetComponent<Button>().interactable = false;
			}
			foreach (GameObject t in toggles) {
				t.GetComponent<Toggle>().interactable = false;
			}
		}
	}
}
