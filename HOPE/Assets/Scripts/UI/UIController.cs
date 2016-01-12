using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

	public ControlStateMachine controlStateMachine;
	public GameController gameController;
	public Toggle wallToggle;
	public Toggle towerToggle;
	public Toggle generatorToggle;
	public Toggle deleteToggle;
	public Button startButton;
	public Text mineralCount;
	public Text wallPrice;
	public Text towerPrice;
	public Text generatorPrice;
	public GameObject confirmationMessageBoxPrefab;

	private float minerals;
	private float mineralsTarget;
	private float velocity = 0;
	private GameObject[] buttons;
	private GameObject[] toggles;

	void Start () {
		buttons = GameObject.FindGameObjectsWithTag ("Button");
		toggles = GameObject.FindGameObjectsWithTag ("Toggle");
		wallPrice.text = GameController.wallCost.ToString();
		towerPrice.text = GameController.towerCost.ToString();
		generatorPrice.text = GameController.generatorCost.ToString();
		minerals = gameController.minerals;
	}

	void Update () {
		if(controlStateMachine.getState()!=controlState.simulation2) {
			if (Input.GetKeyDown("t"))
				changeValue (towerToggle);
			else if (Input.GetKeyDown("w"))
				changeValue (wallToggle);
			else if (Input.GetKeyDown("x"))
				changeValue(deleteToggle);
			else if (Input.GetKeyDown("g"))
				changeValue (generatorToggle);
			else if (Input.GetKeyDown("s"))
				startSimulation ();
		}

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
		minerals = Mathf.SmoothDamp ((float)minerals, (float)mineralsTarget, ref velocity, 0.07f);
		mineralCount.text = ((int)Mathf.Round(minerals)).ToString();
	}

	public void menuConfirmation() {
		GameObject confirmationMessageBox = (GameObject)Instantiate (confirmationMessageBoxPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		confirmationMessageBox.transform.SetParent(gameObject.transform, false);
	}

	public void menu() {
		Application.LoadLevel (0);
	}

	public void destroyConfirmationMessageBoxes() {
		GameObject[] confirmationMessageBoxes = GameObject.FindGameObjectsWithTag ("ConfirmationMessageBox");
		foreach (GameObject cMB in confirmationMessageBoxes) {
			Destroy(cMB);
		}
	}
	
	public void toggleOff() {
		wallToggle.isOn = false;
		towerToggle.isOn = false;
		deleteToggle.isOn = false;
		generatorToggle.isOn = false;
	}

	public void startSimulation() {
		toggleOff ();
		controlStateMachine.stateSimulation1();
	}

	public void changeValue(Toggle toggle) {
		if (toggle.isOn == true)
			toggle.isOn = false;
		else
			toggle.isOn = true;
	}
}
