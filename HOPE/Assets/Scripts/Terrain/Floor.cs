using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
Floor class: basic element of the terrain grid
Walls and Towers can be built on each Floor
*/

public enum terrain{
	empty, tower, generator, wall, underTower, unknown, outofmap
};

public class Floor : MonoBehaviour {

	private GameObject currentObject;

	public GameObject wallPrefab;
	public GameObject towerPrefab;
	public GameObject highlightPrefab;
	public GameObject generatorPrefab;
	public GameObject laserPrefab;

	public GameObject particlePrefab;
	public GameObject particlePrefab2;

	public terrain has;

	public bool isEmpty() {return currentObject == null;}

	void Start () {
		has=terrain.empty;
	}

	void Update () {

	}

	//Instantiate a Wall above the Floor
	public bool createWall() {
		if(!isEmpty())
			return false;

		has=terrain.wall;
		GameObject wallObject = (GameObject)Instantiate(	wallPrefab,
									new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
									Quaternion.identity
								);
		currentObject = wallObject;
		wallObject.tag = "Wall";

		emitParticles(1);

		return true;
	}
	
	//Instantiate a Tower above the Floor
	public bool createTower(towerType t) {
		if(!isEmpty())
			return false;

		has=terrain.tower;
		GameObject towerObject = (GameObject)Instantiate(	towerPrefab,
									new Vector3(transform.position.x, transform.position.y, transform.position.z),
									Quaternion.identity
								);
		currentObject = towerObject;
		towerObject.tag = "Tower";

		emitParticles(1);

		return true;
	}

	//Destroy the currentObject
	public string deleteObject() {
		if (isEmpty ())
			return "";
		
		string tag = currentObject.tag;
		Destroy(currentObject);
		has=terrain.empty;
		return tag;
	}

	public void highlight() {
		/*GameObject hl = (GameObject)*/Instantiate(	highlightPrefab,
								new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
								Quaternion.identity
							);
	}

	public bool createGenerator(Floor end) {
		if(!isEmpty())
			return false;

		has=terrain.generator;
		GameObject generatorObject = (GameObject)Instantiate(	generatorPrefab,
									new Vector3(transform.position.x, transform.position.y, transform.position.z),
									Quaternion.identity
								);
		
		GameObject laserObject = (GameObject)Instantiate(	laserPrefab,
									new Vector3(transform.position.x, transform.position.y, transform.position.z),
									Quaternion.identity
								);		
		laserObject.GetComponent<Laser>().setOriginEnd(generatorObject, end.gameObject);

		Vector3 vect = end.gameObject.transform.position - generatorObject.transform.position;


		float ang=Vector3.Angle(new Vector3(0,0,1), vect);

		if(vect.x<0)
			ang=ang*(-1);

		generatorObject.transform.Rotate(new Vector3(0, ang, 0 ));

		currentObject = generatorObject;
		generatorObject.tag = "Generator";

		emitParticles(1);

		return true;
	}

	public void emitParticles(int i) {
		if(i==1)
		Instantiate(	particlePrefab,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			);
		else
		Instantiate(	particlePrefab2,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			);
	}

	public bool walkable(bool avoidTowers) {
		if(avoidTowers) {		
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, 5.0f);
			int i = 0;
			while (i < hitColliders.Length) {			
				if(hitColliders[i].tag == "Tower") {
					if(hitColliders[i].GetComponent<Tower>().powered)
							return false;
				}
				i++;
			}
		}
		return isEmpty();
	}
}
