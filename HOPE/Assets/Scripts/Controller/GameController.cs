using UnityEngine;
using System.Collections;

/*
GameController : manages mouse clicks depending on the current state (to instantiate Wall, Tower, etc).
*/

public class GameController : MonoBehaviour {

	public ControlStateMachine sm;
	public TerrainGenerator tg;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {	
		Floor f=tg.getFloor();
		if(f!=null) {

			if(sm.getState() != controlState.idle)
				f.highlight();

			if(Input.GetMouseButton (0)) {
				if(sm.getState() == controlState.wall) {
					f.createWall();
				}
				else if(sm.getState() == controlState.tower1) {
					f.createTower(towerType.tower1);
				}
				if(sm.getState() == controlState.deleteWall) {
					f.deleteObject();
				}
			}	
		}

	
	}
}
