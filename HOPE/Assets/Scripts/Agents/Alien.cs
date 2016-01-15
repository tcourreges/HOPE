using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/*
	Alien agent
*/

public class Alien : MonoBehaviour {

	private int healthMax = 100;
	private int health;
	private int cdSpeed=0;

	private int sight=8;

	private HealthBar healthBar;
	public HealthBar healthBarPrefab;

	public int id;

	public GameObject explosionParticles;

	private terrain[,] map;
	public Floor[,] floors;
	public int x,y;
	private int xtarget, ytarget;
	private bool[,] marked;

	public bool transparent=false;

	public TerrainGenerator tg;

	private bool foundCore=false;

	public int alienType=0;

	private Vector3[,] from;

	public bool stuck=false;
	public int hitByLaser=0;

	private float laserx=-1, lasery=-1;
	private bool backToLaser=false;

	NavMeshAgent agent;// = GetComponent<NavMeshAgent>();

	// Use this for initialization
	void Start () {
		print(id+" creating alien");

		health=healthMax;

		healthBar = (HealthBar)Instantiate(	healthBarPrefab,
							new Vector3(transform.position.x, transform.position.y+1.5f, transform.position.z),
							Quaternion.identity
			  		);
		healthBar.setAlien(this);

		xtarget=x; ytarget=y;
		map = new terrain[100,100];
		from = new Vector3[100,100];
		for(int i=0; i<100; i++)
			for(int j=0; j<100; j++)
				if(i<tg.sizeOfMap && j<tg.sizeOfMap)
					map[i,j]=terrain.unknown;
				else
					map[i,j]=terrain.outofmap;

		agent = GetComponent<NavMeshAgent>();
	}
	
	/* ---------------------------------- */

	// Update is called once per frame
	void Update () {
		cdSpeed--;
		if(cdSpeed<0)
			agent.speed=2;

		if(stuck)
			return;

		//print(hitByLaser);

		if(alienType==0) {
			if(atDestination()) {
				if(x==xtarget && y==ytarget) {
					mapPosition();

					if(foundCore) {
						//print(id+" found core");

						moveTo((int)tg.coreLocation.x, (int)tg.coreLocation.y);
					}
					else {
						Vector3 toexplore = findClosestUnknown(true);
						if(toexplore.z==-1){
							//print(id+" through towers");
							toexplore = findClosestUnknown(false);
						}
						if(toexplore.z!=-1) {
							//print(id+" next location : "+toexplore.x+" "+toexplore.y);
							xtarget = (int)toexplore.x;
							ytarget = (int)toexplore.y;
							moveTo((int) toexplore.x, (int) toexplore.y);
						}
						else {
							stuck=true;
						}
					}
				}
				else {
					stepTowards(xtarget,ytarget);
				}
			}
			else {
				moveTo(x,y);
				//floors[xtarget,ytarget].emitParticles(2);	
			}
		}
		else {
			if(backToLaser)
				return;
			if(hitByLaser>0 && laserx==-1) {
				print("foundlaser");
				laserx=agent.destination.x;
				lasery=agent.destination.y;
			}
			if(atDestination()) {
				if(x==xtarget && y==ytarget) {
					mapPosition();



					if(foundCore) {
						//print(id+" found core");

						moveTo((int)tg.coreLocation.x, (int)tg.coreLocation.y);
					}
					else {
						Vector3 toexplore = findClosestUnknown(true);
						if(toexplore.z!=-1) {
							//print(id+" next location : "+toexplore.x+" "+toexplore.y);
							xtarget = (int)toexplore.x;
							ytarget = (int)toexplore.y;
							moveTo((int) toexplore.x, (int) toexplore.y);
						}
						else {
							if(laserx!=-1) {
								print("stuck so i go back to laser");
								agent.destination = new Vector3(laserx, 0, lasery);
								backToLaser=true;
							}
							else {
								stuck=true;
							}
						}
					}
				}
				else {
					stepTowards(xtarget,ytarget);
				}
			}
			else {
				moveTo(x,y);
				//floors[xtarget,ytarget].emitParticles(2);	
			}
		}
		
		if(hitByLaser>0)
			hitByLaser--;
	}

	//fills map on the current position
	private void mapPosition() {
		for(int i=x-sight; i<x+sight; i++)
			for(int j=y-sight; j<y+sight; j++)
				if(fitsMap(i,j)) {
					if(i == (int) tg.coreLocation.x && j == (int) tg.coreLocation.y)
						foundCore=true;
					if(map[i,j]==terrain.unknown) {
						map[i,j] = floors[i,j].has;
					}
				}
	}

	//checks if i,j is a valid index
	private bool fitsMap(int i, int j) {
		return (i>=0 && i<tg.sizeOfMap && j>=0 && j<tg.sizeOfMap);
	}

	//finds the next node on the way to _x,_y, and calls moveTo
	private void stepTowards(int _x, int _y) {
		int x1=_x; int y1=_y;
		int x2=_x; int y2=_y;

		while(!(x==x1 && y==y1)) {
			x2=x1; y2=y1;
			Vector3 v=from[x2, y2];
			x1=(int)v.x;
			y1=(int)v.y;
		}
		moveTo(x2, y2);
	}

	//sets the navmesh agent destination to the coordinates of floor[_x,_y]
	private void moveTo(int _x, int _y) {
		//print(id+"  moveto "+x+" "+y);
		Vector3 dest = floors[_x,_y].gameObject.transform.position;
		dest.y = 1.5f;
		agent.destination = dest;
		x=_x; y=_y;
	}

	//checks if the agent is close enough to floor[x,y]
	private bool atDestination() {
		Vector3 diff = transform.position - agent.destination;
		float dist = diff.x * diff.x + diff.z * diff.z;
		return (dist < 0.1);
	}

	//bfs to find the closest unknown in the map (also fills the table from to rebuild the path
	private Vector3 findClosestUnknown(bool avoidTower) {
            var queue = new Queue<Vector3>();
            queue.Enqueue(new Vector3(x,y,0));

            marked=new bool[100,100];

	    int it=0;

            while (queue.Count != 0)
            {
		it++;
		//print(it);
	        if(it>tg.sizeOfMap*tg.sizeOfMap) {
			print("error "+it);
			return new Vector3(0,0,-1);
		}
                Vector3 v = queue.Dequeue();
		int i=(int)v.x;
		int j=(int)v.y;

		//floors[i,j].emitParticles(1);
		//print(i+" "+j);

		if(map[i,j]==terrain.unknown && floors[i,j].walkable(avoidTower)) {
			//floors[i,j].emitParticles(2);
			return v;
		}

		if(floors[i,j].walkable(avoidTower)) {
		//if(map[i,j]==terrain.empty) {
			List<Vector3> neighbours = new List<Vector3>();
			neighbours.Add(new Vector3(	i-1,	j,0));
			neighbours.Add(new Vector3(	i+1,	j,0));
			neighbours.Add(new Vector3(	i,	j-1,0));
			neighbours.Add(new Vector3(	i,	j+1,0));

		        //foreach (Vector3 w in neighbours.orderRandomly())
			int[] indexes = new int[] {0,1,2,3};
			shuffle(indexes);
			foreach(int n in indexes)
		        {
			    Vector3 w = neighbours[n];
		            if (fitsMap((int)w.x, (int)w.y) && !marked[(int)w.x, (int)w.y])
		            {
				marked[(int)w.x, (int)w.y]=true;
				from[(int)w.x,(int)w.y]=v;
		                queue.Enqueue(w);
		            }
		        }
		}
            }
	    return new Vector3(0,0,-1);
	}

	//shuffle a vector so there is no bias in the direction choice
	void shuffle(int[] v) {
		for (int t = 0; t < v.Length; t++ )
		{
		    int tmp = v[t];
		    int r = UnityEngine.Random.Range(t, v.Length);
		    v[t] = v[r];
		    v[r] = tmp;
		}
	}

	/* ---------------------------------- */
	//decreases hp by i
	public void healthDown(int i) {
		health -= i;
		if(health < 1)
			die();
	}
	//the agents is slowed
	public void slowDown() {
		agent.speed=0.5f;
		cdSpeed=150;
	}
	//kills the agent and the healthBar, creates an explosion
	private void die() {
		Destroy(transform.gameObject);
		if(healthBar!=null)
			Destroy(healthBar.transform.gameObject);

		Instantiate(	explosionParticles,
				new Vector3(transform.position.x, transform.position.y, transform.position.z),
				Quaternion.identity
			  );
	}

	public int getHealth() {return health;}
	public int getHealthMax() {return healthMax;}
}
