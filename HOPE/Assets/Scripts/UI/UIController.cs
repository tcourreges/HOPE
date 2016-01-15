using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
	handles the ui buttons and how they interact with the state machine
*/
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
	public Text tower1Price;
	public Text tower2Price;
	public Text tower3Price;
	public Text generatorPrice;
	public GameObject confirmationMessageBoxPrefab;
	public Text endText;

	private float minerals;
	private float mineralsTarget;
	private float velocity = 0;
	private GameObject[] buttons;
	private GameObject[] toggles;

	void Start () {
		buttons = GameObject.FindGameObjectsWithTag ("Button");
		toggles = GameObject.FindGameObjectsWithTag ("Toggle");
		wallPrice.text = GameController.wallCost.ToString();
		tower1Price.text = GameController.tower1Cost.ToString();
		tower2Price.text = GameController.tower2Cost.ToString();
		tower3Price.text = GameController.tower3Cost.ToString();
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
				startStopSimulation ();
			else if (Input.GetKey(KeyCode.Escape))
				menuConfirmation();
		}

		if (controlStateMachine.getState () == controlState.generator2) {
			generatorToggle.isOn = false;
		}
		if (controlStateMachine.getState () == controlState.simulation1 || controlStateMachine.getState () == controlState.simulation2) {
			foreach (GameObject b in buttons) {
				b.GetComponent<Button> ().interactable = false;
			}
			foreach (GameObject t in toggles) {
				t.GetComponent<Toggle> ().interactable = false;
			}
		} else {
			foreach (GameObject b in buttons) {
				b.GetComponent<Button> ().interactable = true;
			}
			foreach (GameObject t in toggles) {
				t.GetComponent<Toggle> ().interactable = true;
			}
		}
		mineralsTarget = gameController.minerals;
		minerals = Mathf.SmoothDamp ((float)minerals, (float)mineralsTarget, ref velocity, 0.07f);
		mineralCount.text = ((int)Mathf.Round(minerals)).ToString();
	}

	public void menuConfirmation() {
		Time.timeScale = 0;
		GameObject confirmationMessageBox = (GameObject)Instantiate (confirmationMessageBoxPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		confirmationMessageBox.transform.SetParent(gameObject.transform, false);
	}

	public void menu() {
		Time.timeScale = 1;
		Application.LoadLevel (0);
	}

	public void destroyConfirmationMessageBoxes() {
		Time.timeScale = 1;
		GameObject[] confirmationMessageBoxes = GameObject.FindGameObjectsWithTag ("ConfirmationMessageBox");
		foreach (GameObject cMB in confirmationMessageBoxes) {
			Destroy(cMB);
		}
	}
	
	public void toggleOff() {
		foreach (GameObject t in toggles) {
			t.GetComponent<Toggle> ().isOn = false;
		}
	}

	public void startStopSimulation() {
		if (controlStateMachine.getState () != controlState.simulation1 && controlStateMachine.getState () != controlState.simulation2) {
			toggleOff ();
			controlStateMachine.stateSimulation1 ();
			startButton.transform.GetChild(0).GetComponent<Text>().text = "Stop";
		} else {
			controlStateMachine.stateSimulation3 ();
			startButton.transform.GetChild(0).GetComponent<Text>().text = "Start";
			endText.text = "";
		}
	}

	public void changeValue(Toggle toggle) {
		if (toggle.isOn == true)
			toggle.isOn = false;
		else
			toggle.isOn = true;
	}

	public void win() {
		endText.text = "GG!";
	}

	public void lose() {
		endText.text = "Try again!";
	}
}
