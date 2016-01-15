using UnityEngine;
using System.Collections;

/*
	particle displayed when building/selling defense (price of the action)
*/

public class MineralSprite : MonoBehaviour {

	public TextMesh text;
	private float time;

	void Start () {
		time = 1.0f;
	}

	void Update() {
		transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
		transform.Translate(new Vector3(0, 0.05f, 0));
		time -= Time.deltaTime;
		if (time < 0) {
			Destroy (gameObject);
		}
	}

	public void setText(string sign, string price) {
		if (sign == "+") {
			text.color = Color.green;
		} else if (sign == "-") {
			text.color = Color.red;
		}
		text.text = sign + price;
	}
}
