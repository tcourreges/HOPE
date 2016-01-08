using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AlienSpawn : MonoBehaviour {

	public GameObject alienPrefab;
	public GameObject particlePrefab;
	public int x;
	public int y;
	public int id;

	private int timer=500;

	public TerrainGenerator tg;

	void Start () {
	}

	void Update () {
		if(timer%10==0)
			emitParticles();
		timer--;
		if(timer<0) {
			spawnAlien();
			Destroy(transform.gameObject);
		}
	}

	//Instantiate a Wall above the Floor
	public void spawnAlien() {
		GameObject alien = (GameObject)Instantiate(	alienPrefab,
									new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
									Quaternion.identity
								);
		Alien a=alien.GetComponent<Alien>();
		a.x=x;
		a.y=y;
		a.id=id;
		a.floors=tg.getFloors();
	}

	private void emitParticles() {
		Instantiate(	particlePrefab,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			);
	}
}
