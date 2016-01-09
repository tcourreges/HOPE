using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public ControlStateMachine controlStateMachine;
	public GameController gameController;
	public Toggle generatorToggle;
	public Text mineralCount;
	public Text wallPrice;
	public Text towerPrice;
	public Text generatorPrice;

	private float minerals;
	private float mineralsTarget;
	private float velocity = 0;
	private GameObject[] buttons;
	private GameObject[] toggles;

	void Start () {
		buttons = GameObject.FindGameObjectsWithTag ("Button");
		toggles = GameObject.FindGameObjectsWithTag ("Toggle");
		wallPrice.text = gameController.wallCost.ToString();
		towerPrice.text = gameController.towerCost.ToString();
		generatorPrice.text = gameController.generatorCost.ToString();
		minerals = gameController.minerals;
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
		mineralsTarget = gameController.minerals;
		minerals = Mathf.SmoothDamp ((float)minerals, (float)mineralsTarget, ref velocity, 0.2f);
		mineralCount.text = ((int)minerals).ToString();
	}

	public void menu() {
		Application.LoadLevel (0);
	}
}
