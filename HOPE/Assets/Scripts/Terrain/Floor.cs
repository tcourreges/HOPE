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
	public GameObject tower1Prefab;
	public GameObject tower2Prefab;
	public GameObject tower3Prefab;
	public GameObject highlightPrefab;
	public GameObject generatorPrefab;
	public GameObject laserPrefab;

	public GameObject particlePrefab;
	public GameObject particlePrefab2;
	public GameObject mineralSpritePrefab;

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
		
		emitMineralSprite ("-", GameController.wallCost.ToString());
		emitParticles(1);

		return true;
	}
	
	//Instantiate a Tower above the Floor
	public bool createTower(towerType t) {
		if(!isEmpty())
			return false;

		has=terrain.tower;

		if (t == towerType.tower1) {
			GameObject towerObject = (GameObject)Instantiate(	tower1Prefab,
			                                                 new Vector3(transform.position.x, transform.position.y, transform.position.z),
			                                                 Quaternion.identity
			                                                 );
			currentObject = towerObject;
			towerObject.tag = "Tower";
			emitMineralSprite ("-", GameController.tower1Cost.ToString());
		} else if (t == towerType.tower2) {
			GameObject towerObject = (GameObject)Instantiate(	tower2Prefab,
			                                                 new Vector3(transform.position.x, transform.position.y, transform.position.z),
			                                                 Quaternion.identity
			                                                 );
			currentObject = towerObject;
			towerObject.tag = "Tower";
			emitMineralSprite ("-", GameController.tower2Cost.ToString());
		} else if (t == towerType.tower3) {
			GameObject towerObject = (GameObject)Instantiate(	tower3Prefab,
			                                                 new Vector3(transform.position.x, transform.position.y, transform.position.z),
			                                                 Quaternion.identity
			                                                 );
			currentObject = towerObject;
			towerObject.tag = "Tower";
			emitMineralSprite ("-", GameController.tower3Cost.ToString());
		}
		emitParticles(1);

		return true;
	}

	//Destroy the currentObject
	public string deleteObject() {
		if (isEmpty ())
			return "";
		
		string tag = currentObject.tag;

		if (tag == "Wall") {
			emitMineralSprite("+", GameController.wallCost.ToString());
		} else if (tag == "Tower") {
			if (currentObject.GetComponent<Tower>().type == towerType.tower1) {
				emitMineralSprite("+", GameController.tower1Cost.ToString());
				tag = "Tower1";
			} else if (currentObject.GetComponent<Tower>().type == towerType.tower2) {
				emitMineralSprite("+", GameController.tower2Cost.ToString());
				tag = "Tower2";
			} else if (currentObject.GetComponent<Tower>().type == towerType.tower3) {
				emitMineralSprite("+", GameController.tower3Cost.ToString());
				tag = "Tower3";
			}
		} else if (tag == "Generator") {
			emitMineralSprite("+", GameController.generatorCost.ToString());
		}

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

		emitMineralSprite ("-", GameController.generatorCost.ToString());
		emitParticles(1);

		return true;
	}

	public void emitMineralSprite(string sign, string price) {
		GameObject mineralSpriteObject = (GameObject)Instantiate (mineralSpritePrefab,
		                                                          new Vector3 (transform.position.x, transform.position.y + 3f, transform.position.z),
		                                                          Quaternion.identity
		                                                          );
		mineralSpriteObject.GetComponent<MineralSprite> ().setText (sign, price);
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
