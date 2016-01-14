using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotSpawn : MonoBehaviour {

	public GameObject robotPrefab;
	public GameObject particlePrefab;
	public int x;
	public int y;
	public int id;

	public float xtarget, ytarget;

	public TerrainGenerator tg;

	void Start () {
	}

	void Update () {
	}

	public void spawnRobot() {
		GameObject robot = (GameObject)Instantiate(	robotPrefab,
								new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
								Quaternion.identity
							);
		Robot r=robot.GetComponent<Robot>();

		r.setDestination(xtarget,ytarget);
		/*r.x=x;
		r.y=y;
		r.id=id;
		r.floors=tg.getFloors();
*/
		//Destroy(transform.gameObject);
	}

	private void emitParticles() {
		Instantiate(	particlePrefab,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			);
	}
}
