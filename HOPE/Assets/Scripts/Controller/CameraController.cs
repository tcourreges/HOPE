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

	private Vector3 velocity = Vector3.zero;
	
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
		TarCamPos = transform.position;
		float distance = Mathf.Abs (TarCamPos.y);
		Zoom = Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed * distance;
		Vector3 ZoomVector = new Vector3(0.0f, -Mathf.Cos(transform.rotation.x)*Zoom, Mathf.Sin(transform.rotation.x)*Zoom);
		Vector3 TestZoomVector = TarCamPos + ZoomVector;
		//print (TestZoomVector);
		//print (transform.position);
		//if (TestZoomVector.y > 0.4) {
			TarCamPos += ZoomVector;
			transform.position = Vector3.SmoothDamp (transform.position, TarCamPos, ref velocity, 0.2f);
		//}
	}
}
