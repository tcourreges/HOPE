using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float ScrollSpeed;
	private float ScrollEdge = 0.01f;

	private float Zoom = 0f;
	public float ZoomSpeed;
	private float StartTime;

	private Vector3 initPos;
	private Quaternion initRot;


	private Vector3 velocity = Vector3.zero;
	
	void Start() {
		//initPos=transform.position;
		//initRot=transform.rotation;
	}
	
	void Update () {
		// SCROLL
		if (Input.mousePosition.x >= Screen.width * (1 - ScrollEdge) || Input.GetKey(KeyCode.RightArrow)) {
			transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
		}
		else if (Input.mousePosition.x <= Screen.width * ScrollEdge || Input.GetKey(KeyCode.LeftArrow)) {
			transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);
		}
		
		if (Input.mousePosition.y >= Screen.height * (1 - ScrollEdge) || Input.GetKey(KeyCode.UpArrow)) {
			transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
		}
		else if (Input.mousePosition.y <= Screen.height * ScrollEdge || Input.GetKey(KeyCode.DownArrow)) {
			transform.Translate(Vector3.forward * Time.deltaTime * -ScrollSpeed, Space.World);
		}

		// ZOOM
		Vector3 TarCamPos = transform.position;
		Zoom = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;

		if(Input.GetKey(KeyCode.PageUp))
			Zoom = -0.05f * ZoomSpeed;
		else if(Input.GetKey(KeyCode.PageDown))
			Zoom = 0.1f * ZoomSpeed;

		TarCamPos = transform.position + transform.forward * Time.deltaTime * Zoom * 1000;
		float yMax = Mathf.Max (TarCamPos.y, 0.5f);
		float zMax = (yMax / TarCamPos.y) * TarCamPos.z;
		TarCamPos.y = yMax;
		TarCamPos.z = zMax;
		transform.position = Vector3.SmoothDamp (transform.position, TarCamPos, ref velocity, 0.2f);

		//rotations
		if(Input.GetKey("p")) {
			transform.Rotate(new Vector3(0,1,0), Space.World);
		}
		if(Input.GetKey("m")) {
			transform.Rotate(new Vector3(0,-1,0), Space.World);
		}
		
	}
}
