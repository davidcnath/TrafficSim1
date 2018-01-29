using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class defaultTile : MonoBehaviour {

	//Tile size is 3 x 3 units. 

	private gameTiles tileMaster;

	public bool destroyOnLoad;
	public int xCoord;
	public int yCoord;

	// road builder bools
	private bool roadBuilderActive;
	private bool colorYellow;
	private bool colorRed;
	private bool tileSelected;

	// Placeholder tiles
	private GameObject roadTiles;
	public GameObject newRoadPlaceholder;

	void Start () {
		if (destroyOnLoad) {Destroy (gameObject);	}
		tileMaster = transform.parent.GetComponent<gameTiles> ();
		roadTiles = transform.parent.parent.Find("RoadTiles").gameObject;
	}

	// --------------------- Mouse Controls --------------------- //
	void OnMouseDown(){
		if (roadBuilderActive) {
			if (!tileMaster.firstTileSelected) {
				tileMaster.selectedTileToggle();
				tileMaster.getFirstPos (xCoord, yCoord);
				toggleRed();
			} else {
				if (colorRed) {
					createRoad ();
					Destroy (gameObject);
				}
				tileMaster.getSecondPos (xCoord, yCoord);
				tileMaster.selectedTileToggle();
				tileMaster.buildRoads ();
			}
		}
	}

	void OnMouseOver(){
		if (roadBuilderActive && !tileMaster.firstTileSelected) {
			gameObject.GetComponent<Renderer> ().material.color = Color.green;
		} else if (roadBuilderActive && tileMaster.firstTileSelected){
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
			tileMaster.getSecondPos (xCoord, yCoord);
			tileMaster.roadBuildMode ();
		}
	}

	void OnMouseExit(){
		if (roadBuilderActive && !tileMaster.firstTileSelected) {
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
			tileMaster.resetAllColors ();
		} else if (roadBuilderActive && tileMaster.firstTileSelected && !colorRed) {
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
			tileMaster.resetAllColors ();
		}

	}

	// --------------- Public methods to alter bools and/or colors --------------- // 

	public void resetColor(){
		if (!colorRed) {
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
	}

	public void roadBuilderToggle(){
		if (!roadBuilderActive) {
			roadBuilderActive = true;
		} else {
			roadBuilderActive = false;
		}
	}


	public void toggleRed(){
		if (!colorRed) {
			colorRed = true;
			gameObject.GetComponent<Renderer> ().material.color = Color.red;
		} else {
			colorRed = false;
			gameObject.GetComponent<Renderer> ().material.color = Color.white;
		}
	}
	// ----------------- Destroy and make road ------------- //

	public void createRoad(){
		Vector3 tilePos = transform.position;
		GameObject newTile = Instantiate (newRoadPlaceholder, tilePos, Quaternion.Euler(90f,0f,0f)) as GameObject;
		//newTile.transform.parent = gameObject.transform.parent.transform;
		newTile.transform.parent = roadTiles.transform;
		newTile.GetComponent<roadPlaceholder> ().getCoords (xCoord, yCoord);
		newTile.name = "RoadPlaceHolder_" + xCoord + "_" + yCoord;
	}

	// --------------- Positioning / Misc --------------- // 

	public void updateCoords(int x, int y){
		xCoord = x;
		yCoord = y;
	}

}
