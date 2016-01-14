using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* Types of towers
*/
public enum towerType{
	core,
	generator,
	tower1,
	tower2,
	tower3
};


/*
Tower class : can be powered, attacks aliens
*/
public class Tower : MonoBehaviour {

	public bool transparent;
	public bool powered;
	private int updated;
	public GameObject towerRangePrefab;
	private GameObject towerRange;
	public GameObject towerWallPrefab;
	private GameObject towerWall;	
	public GameObject poweredParticlePrefab;
	private GameObject poweredParticle;

	public Material towerRangeMaterial;

	private float range=4;
	private int damage=25;
	private int reload=60;
	private int strategy=1;

	private int currentReload;

	public GameObject projectilePrefab;

	public towerType type;

	// Use this for initialization
	void Start () {
		transparent=true;
		powered=false;
		currentReload = reload;

		setTowerType(type);
	}

	void setTowerType(towerType t) {
		if(t==towerType.tower1) {
			range=4;
			damage=25;
			reload=60;
			strategy=1;
			showRange();
		}
		else if(t==towerType.tower2) {
			range=2;
			damage=50;
			reload=100;
			strategy=2;
			showRange();
		}
		else {
			range=6;
			damage=0;
			reload=40;
			strategy=1;
			showRange();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(updated>10) {
			powered=false;
			Destroy (poweredParticle);
			lightOn(0f);
			gameObject.GetComponent<Renderer>().material.color = Color.white;
		}
		updated++;

		currentReload++;

		if(powered && currentReload > reload) {
			if(strategy==1) {
				GameObject alien = findClosestEnemy();
				if(alien != null)
					attack(alien);
			}
			else if(strategy==2){
				List<GameObject> aliens=findEnemiesInRange();
				foreach(GameObject a in aliens) {
					if(a != null)
						attack(a);
				}
			}
			else {
				GameObject alien = findClosestEnemy();
				if(alien != null)
					slow(alien);
			}
			
		}

		//if(powered && towerWall==null) wallRange();
		//if(!powered && towerWall!=null) Destroy (towerWall); 
	}

	//Powers the tower during the next 10 frames
	public void power() {
		powered=true;
		lightOn(2.8f);
		gameObject.GetComponent<Renderer>().material.color = Color.red;
		emitParticles();
		updated=0;
	}

	public void emitParticles() {
		if (poweredParticle == null) {
			if (type == towerType.tower1) {
				poweredParticle = (GameObject)Instantiate (poweredParticlePrefab,
				                                           new Vector3 (transform.position.x, transform.position.y + 1f, transform.position.z),
				                                           Quaternion.identity
				                                           );
				poweredParticle.GetComponent<PoweredParticle>().setParent (gameObject);
			} else if (type == towerType.tower2) {

			} else if (type == towerType.tower3) {

			}
		}
	}

	public void attack(GameObject a) {
		a.GetComponent<Alien>().healthDown(damage);
		currentReload = 0;

		GameObject projectile = (GameObject) Instantiate(	projectilePrefab,
		            						new Vector3(a.transform.position.x, a.transform.position.y, a.transform.position.z),
		            						Quaternion.identity
		            				);
		projectile.GetComponent<LaserProjectile> ().setOriginEnd (transform.position, a.transform.position);		
	}

	public void slow(GameObject a) {
		a.GetComponent<Alien>().slowDown();
		currentReload = 0;

		GameObject projectile = (GameObject) Instantiate(	projectilePrefab,
		            						new Vector3(a.transform.position.x, a.transform.position.y, a.transform.position.z),
		            						Quaternion.identity
		            				);
		projectile.GetComponent<LaserProjectile> ().setOriginEnd (transform.position, a.transform.position);		
	}

    	private GameObject findClosestEnemy() {
		GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
		GameObject closest = null;

		float minDist = Mathf.Infinity;
		Vector3 position = transform.position;
		foreach (GameObject alien in aliens) { 
		    Vector3 diff = alien.transform.position - position;
			float dist = diff.x * diff.x + diff.z * diff.z;
		    if (dist < minDist && dist < range * range) {
		        closest = alien;
		        minDist = dist;
		    }
		}
		return closest;
	}

	private List<GameObject> findEnemiesInRange() {
		GameObject[] aliens = GameObject.FindGameObjectsWithTag("Alien");
		List<GameObject> res=new List<GameObject>();

		Vector3 position = transform.position;
		foreach (GameObject alien in aliens) { 
		    Vector3 diff = alien.transform.position - position;
			float dist = diff.x * diff.x + diff.z * diff.z;
		    if (dist < range * range) {
		        res.Add(alien);
		    }
		}
		return res;
	}

	public float getRange() {return range;}
	public float getDamage() {return damage;}

	private void showRange() {
		towerRange = (GameObject)Instantiate(	towerRangePrefab,
		            				new Vector3(transform.position.x, transform.position.y+0.01f, transform.position.z),
		            				Quaternion.identity
		            				);
		towerRange.GetComponent<TowerRange> ().initialize (range, gameObject);
		towerRange.GetComponent<MeshRenderer> ().material = towerRangeMaterial;
	}

	private void wallRange() {
		towerWall = (GameObject)Instantiate(	towerWallPrefab,
		            				new Vector3(transform.position.x, transform.position.y+0.01f, transform.position.z),
		            				Quaternion.identity
		            				);
		//towerWall.GetComponent<TowerRange> ().initialize (range, gameObject);
		towerWall.transform.localScale = new Vector3(2 * range + 1, 1, 2 * range + 1);
	}

	public void deleteRange() {
		Destroy (towerRange);
	}

	public void lightOn(float i) {
		Component[] lights = GetComponentsInChildren<Light>();
		foreach (Light l in lights) {
			l.intensity = i;
	        }
	}
}
