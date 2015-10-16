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
		if (Input.GetAxis("Mouse ScrollWheel")!=0) {
			StartTime = Time.time;
			Zoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomSpeed;
			Vector3 ZoomVector = new Vector3(0.0f, -Mathf.Cos(transform.rotation.x)*Zoom, Mathf.Sin(transform.rotation.x)*Zoom);
			TarCamPos += ZoomVector;
			journeyLength = Vector3.Distance (transform.position, TarCamPos);
		}
		float distCovered = (Time.time - StartTime) * 500;
		float fracJourney = distCovered / journeyLength;
		//print (transform.position);
		//print (TarCamPos);
		//print (fracJourney);
		transform.position = Vector3.Lerp (transform.position, TarCamPos, fracJourney);
	}
}