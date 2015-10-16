using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float ScrollSpeed;
	private float ScrollEdge = 0.01f;

	private float Zoom = 0f;
	public float ZoomSpeed;
	private float StartTime;
	private float journeyLength = 1.0f;

	private Vector3 CurCamPos;
	private Vector3 TarCamPos;

	private Vector3 velocity=Vector3.zero;
	
	void Start() {
		TarCamPos = transform.position;
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
		float x = (transform.position.x - TarCamPos.x);
		float y = (transform.position.y - TarCamPos.y);
		float z = (transform.position.z - TarCamPos.z);
		print (x + " " + y + " " + z);
		TarCamPos = transform.position;
		Zoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomSpeed;
		Vector3 ZoomVector = new Vector3(0.0f, -Mathf.Cos(transform.rotation.x)*Zoom, Mathf.Sin(transform.rotation.x)*Zoom);
		TarCamPos += ZoomVector;
		transform.position = Vector3.SmoothDamp (transform.position, TarCamPos, ref velocity, 0.2f);
	}
}