using UnityEngine;
using System.Collections;

/*
	Obsolete : not used in the project anymore
*/
public class Path : MonoBehaviour {

	public int x0, y0, xf, yf;

	public int sizeOfMap=30;

	public TerrainGenerator tg;

	public Floor[,] floors;
	public bool[,] marked;

	bool done=false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(done) {
			marked=new bool[100,100];
			//print(dfs(x0,y0)); return;
		}
		done=true;

		floors=tg.getFloors();
		//marked=new bool[100,100];
		//print(dfs(x0,y0));
	}

	public bool dfs(int x, int y) {
		if(x==xf && y==yf)
			return true;

		//print(x+" "+y+" "+floors[x,y]);

		marked[x,y]=true;

		bool res=false;

		if(x>0 && !marked[x-1,y] && floors[x-1,y].walkable(false))		res = res || dfs(x-1,y);
		if(x<sizeOfMap-1 && !marked[x+1,y] && floors[x+1,y].walkable(false))	res = res || dfs(x+1,y);
		if(y>0 && !marked[x,y-1] && floors[x,y-1].walkable(false)) 		res = res || dfs(x,y-1);
		if(y<sizeOfMap-1 && !marked[x,y+1]&& floors[x,y+1].walkable(false)) 	res = res || dfs(x,y+1);

		return res;

	}
}
