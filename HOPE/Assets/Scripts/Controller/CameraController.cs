using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float ScrollSpeed;
	private float ScrollEdge = 0.01f;

	private float Zoom = 0f;
	public float ZoomSpeed;
	private float StartTime;

	private Vector3 CurCamPos;
	private Vector3 TarCamPos;
	private float distance;

	private Vector3 velocity = Vector3.zero;
	
	void Start() {
		TarCamPos = transform.position;
		distance = Mathf.Abs (transform.position.y);
	}
	
	void Update () {
		// SCROLL
		if (Input.mousePosition.x >= Screen.width * (1 - ScrollEdge)) {
			transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
		}
		else if (Input.mousePosition.x <= Screen.width * ScrollEdge) {
			transform.Translate(Vector3.right * Time.deltaTime * -ScrollSpeed, Space.World);
		}
		
		if (Input.mousePosition.y >= Screen.height * (1 - ScrollEdge)) {
			transform.Translate(Vector3.forward * Time.deltaTime * ScrollSpeed, Space.World);
		}
		else if (Input.mousePosition.y <= Screen.height * ScrollEdge) {
			transform.Translate(Vector3.forward * Time.deltaTime * -ScrollSpeed, Space.World);
		}

		// ZOOM
		TarCamPos = transform.position;
		Zoom = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
		TarCamPos = transform.position + transform.forward * Time.deltaTime * Zoom * 1000;
		float yMax = Mathf.Max (TarCamPos.y, 0.5f);
		float zMax = (yMax / TarCamPos.y) * TarCamPos.z;
		TarCamPos.y = yMax;
		TarCamPos.z = zMax;
		transform.position = Vector3.SmoothDamp (transform.position, TarCamPos, ref velocity, 0.2f);
		/*
		Vector3 ZoomVector = new Vector3(0.0f, -Mathf.Cos(transform.rotation.x)*Zoom, Mathf.Sin(transform.rotation.x)*Zoom);
		TarCamPos += ZoomVector;
		//TarCamPos.y = Mathf.Clamp (TarCamPos.y, 0.4f, Mathf.Infinity);
		transform.position = Vector3.SmoothDamp (transform.position, TarCamPos, ref velocity, 0.2f);
		distance = Mathf.Abs (TarCamPos.y);
		*/
	}
}