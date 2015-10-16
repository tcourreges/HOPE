using UnityEngine;
using System.Collections;

public class HealthBar : MonoBehaviour {

	TextMesh tm;

	public int currentHealth() {
		return tm.text.Length;
	}

	// Use this for initialization
	void Start () {
		tm = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		transform.forward = Camera.main.transform.forward;
	}

	void inc() {
		if(currentHealth() > 1)
			tm.text = tm.text.Remove(tm.text.Length -1);
		else
			Destroy(transform.parent.gameObject);
	}
}
