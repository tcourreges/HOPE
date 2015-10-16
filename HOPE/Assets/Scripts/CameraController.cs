using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float ScrollSpeed;
	private float ScrollEdge = 0.01f;
	
	private int HorizontalScroll = 1;
	private int VerticalScroll = 1;
	private int DiagonalScroll = 1;

	public Vector2 ZoomRange;
	private float CurrentZoom = 0f;
	public float ZoomSpeed;
	private float ZoomRotation = 1f;

	private Vector3 InitPos;
	private Vector3 InitRotation;
	
	void Start() {
		InitPos = transform.position;
		InitRotation = transform.eulerAngles;
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
		CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomSpeed;
		CurrentZoom = Mathf.Clamp(CurrentZoom,ZoomRange.x,ZoomRange.y);
		float ZoomZ = (float)(-(transform.position.y - (InitPos.y + CurrentZoom)) * 0.1);
		transform.Translate (0f, 0f, ZoomZ, Space.Self);
	}
}