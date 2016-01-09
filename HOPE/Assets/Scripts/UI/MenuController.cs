using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	void Start () {
	
	}

	void Update () {
	
	}

	public void quit() {
		Application.Quit ();
	}

	public void newGame() {
		Application.LoadLevel (1);
	}
}
