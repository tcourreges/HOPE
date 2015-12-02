using UnityEngine;
using System.Collections;

/*
Floor class: basic element of the terrain grid
Walls and Towers can be built on each Floor
*/

public class Floor : MonoBehaviour {

	private GameObject currentObject;

	public GameObject wallPrefab;
	public GameObject towerPrefab;
	public GameObject highlightPrefab;
	public GameObject generatorPrefab;
	public GameObject laserPrefab;

	public GameObject particlePrefab;

	public bool isEmpty() {return currentObject == null;}

	//Instantiate a Wall above the Floor
	public void createWall() {
		if(!isEmpty())
			return;

		GameObject wallObject = (GameObject)Instantiate(	wallPrefab,
									new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
									Quaternion.identity
								);
		currentObject = wallObject;
		wallObject.tag = "Wall";

		emitParticles();
	}
	
	//Instantiate a Tower above the Floor
	public void createTower(towerType t) {
		if(!isEmpty())
			return;

		GameObject towerObject = (GameObject)Instantiate(	towerPrefab,
									new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
									Quaternion.identity
								);
		currentObject = towerObject;
		towerObject.tag = "Tower";

		emitParticles();
	}

	//Destroy the currentObject
	public void deleteObject() {
		Destroy(currentObject);
	}

	public void highlight() {
		/*GameObject hl = (GameObject)*/Instantiate(	highlightPrefab,
								new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
								Quaternion.identity
							);
	}

	public void createGenerator(Floor end) {
		GameObject generatorObject = (GameObject)Instantiate(	generatorPrefab,
									new Vector3(transform.position.x, transform.position.y+1, transform.position.z),
									Quaternion.identity
								);
		
		GameObject laserObject = (GameObject)Instantiate(	laserPrefab,
									new Vector3(transform.position.x, transform.position.y+1, transform.position.z),
									Quaternion.identity
								);
		
		laserObject.GetComponent<Laser>().setOriginEnd(generatorObject, end.gameObject);

		emitParticles();
	}

	private void emitParticles() {
		Instantiate(	particlePrefab,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			);
	}

}
