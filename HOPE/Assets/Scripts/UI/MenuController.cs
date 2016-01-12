using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject confirmationMessageBoxPrefab;

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
}
