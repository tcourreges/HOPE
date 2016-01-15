using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
	functions called by the menu buttons
*/

public class MenuController : MonoBehaviour {

	public GameObject confirmationMessageBoxPrefab;
	public GameObject levelSelectionPrefab;

	void Start () {

	}

	void Update () {
	
	}

	public void quitConfirmation() {
		GameObject confirmationMessageBox = (GameObject)Instantiate (confirmationMessageBoxPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
		confirmationMessageBox.transform.SetParent(gameObject.transform, false);
	}

	public void quit() {
		Application.Quit ();
	}

	public void newGame() {
		Application.LoadLevel (1);
	}

	public void loadGame() {
			GameObject levelSelectionBox = (GameObject)Instantiate (levelSelectionPrefab, new Vector3 (0, 0, 0), Quaternion.identity);
			levelSelectionBox.transform.SetParent(gameObject.transform, false);
			Button button = levelSelectionBox.transform.GetChild(4).GetComponentInChildren<Button> ();
			button.interactable = false;
	}

	public void closeLevelSelection() {
		GameObject[] levelSelections = GameObject.FindGameObjectsWithTag ("LevelSelection");
		foreach (GameObject lS in levelSelections) {
			Destroy(lS);
		}
	}

	public void loadLevel(int i) {
		Application.LoadLevel (i);
	}
}
