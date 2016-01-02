using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Alien : MonoBehaviour {

	private int healthMax=4;
	private int health;

	private int sight=4;

	private HealthBar healthBar;
	public HealthBar healthBarPrefab;

	public GameObject explosionParticles;

	private terrain[,] map;
	public Floor[,] floors;
	public int x,y;
	public bool[,] marked;

	private bool actionDone;
	public TerrainGenerator tg;

	NavMeshAgent agent;// = GetComponent<NavMeshAgent>();

	// Use this for initialization
	void Start () {
		health=healthMax;

		healthBar = (HealthBar)Instantiate(	healthBarPrefab,
							new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
							Quaternion.identity
			  		);
		healthBar.setAlien(this);
		floors=tg.getFloors();
		map = new terrain[100,100];
		for(int i=0; i<100; i++)
			for(int j=0; j<100; j++)
				if(i<tg.sizeOfMap && j<tg.sizeOfMap)
					map[i,j]=terrain.unknown;
				else
					map[i,j]=terrain.outofmap;
		
		actionDone = false;

		agent = GetComponent<NavMeshAgent>();
	}
	
	/* ---------------------------------- */

	// Update is called once per frame
	void Update () {
		floors=tg.getFloors();

		if(atDestination()) {
			mapPosition();
			Vector3 toexplore = findClosestUnknown();
			if(toexplore.z==-1){
				//print("donexploring");
			} else {
				//print(toexplore.x +" "+ toexplore.y);
				moveTo((int) toexplore.x, (int) toexplore.y);
			}
		}
		else {
			moveTo(x,y);	
		}/**/
	}

	private void mapPosition() {
		for(int i=x-sight; i<x+sight; i++)
			for(int j=y-sight; j<y+sight; j++)
				if(fitsMap(i,j))
					if(map[i,j]==terrain.unknown) {
						map[i,j] = floors[i,j].has;
					}
		//print(map);
	}

	private bool fitsMap(int i, int j) {
		return (i>=0 && i<tg.sizeOfMap && j>=0 && j<tg.sizeOfMap);
	}

	private void moveTo(int _x, int _y) {
		Vector3 dest = floors[_x,_y].gameObject.transform.position;
		dest.y = 1.5f;
		agent.destination = dest;
		x=_x; y=_y;
	}

	private bool atDestination() {
		Vector3 diff = transform.position - agent.destination;
		float dist = diff.x * diff.x + diff.z * diff.z;
		return (dist < 0.1);
	}

	private Vector3 findClosestUnknown() {
            var queue = new Queue<Vector3>();
            queue.Enqueue(new Vector3(x,y,0));

            marked=new bool[100,100];

	    int it=0;

            while (queue.Count != 0)
            {
		it++;
		//print(it);
	        if(it>tg.sizeOfMap*tg.sizeOfMap) {
			//print(queue.Count);
			return new Vector3(0,0,-1);
		}
                Vector3 v = queue.Dequeue();
		int i=(int)v.x;
		int j=(int)v.y;

		marked[i,j] = true;
		//print(i+" "+j);

		if(map[i,j]==terrain.unknown && floors[i,j].walkable())
			return v;

		if(floors[i,j].walkable()) {
		//if(map[i,j]==terrain.empty) {
			List<Vector3> neighbours = new List<Vector3>();
			neighbours.Add(new Vector3(	i-1,	j,0));
			neighbours.Add(new Vector3(	i+1,	j,0));
			neighbours.Add(new Vector3(	i,	j-1,0));
			neighbours.Add(new Vector3(	i,	j+1,0));
		        foreach (Vector3 w in neighbours)
		        {
		            if (fitsMap((int)w.x, (int)w.y) && !marked[(int)w.x, (int)w.y])
		            {
		                queue.Enqueue(w);
		            }
		        }
		}
            }
	    return new Vector3(0,0,-1);
	}

	/* ---------------------------------- */

	public void healthDown(int i) {
		health -= i;
		if(health < 1)
			die();
	}

	private void die() {
		Destroy(transform.gameObject);
		Destroy(healthBar.transform.gameObject);

		Instantiate(	explosionParticles,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			  );
	}

	public int getHealth() {return health;}
	public int getHealthMax() {return healthMax;}
}
