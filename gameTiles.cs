using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameTiles : MonoBehaviour {

	// map creation variables
	public int gameSizeX = 24;
	public int gameSizeY = 24;
	public GameObject defaultTile;
	private defaultTile newDefaultTile;
	private int tileSize = 3;

	public GameObject otherTiles;
	public GameObject grassTile;

	// map builder variables
	private bool roadBuilderOn;
	public bool firstTileSelected;
	private bool mapTemplateDrawn;

	// road builder coords
	private int firstPosX;
	private int firstPosY;
	private int endPosX;
	private int endPosY;


	// Use this for initialization
	void Start () {
		makeGrid ();
	}

	// ------------------ bool toggles ------------------ //

	public void toggleRoadBuilder(){
		if (!roadBuilderOn) {
			roadBuilderOn = true;
			foreach (Transform child in transform) {
				child.GetComponent<defaultTile> ().roadBuilderToggle();
			}
		} else {
			roadBuilderOn = false;
			foreach (Transform child in transform) {
				child.GetComponent<defaultTile> ().roadBuilderToggle();
			}
		}
	}

	public void selectedTileToggle(){
		if (!firstTileSelected) {
			firstTileSelected = true;
		} else {
			firstTileSelected = false;
		}
	}

	public void templateDrawnToggle(){
		if (!mapTemplateDrawn) {
			mapTemplateDrawn = true;
		} else {
			mapTemplateDrawn = false;
		}
	}

	// ----------------- Public Methods ----------------- // 

	public void getFirstPos(int x, int y){
		firstPosX = x;
		firstPosY = y;
	}

	public void getSecondPos(int x, int y){
		endPosX = x;
		endPosY = y;
	}

	public void resetAllColors(){
		foreach (Transform child in transform) {
			child.GetComponent<defaultTile> ().resetColor ();
		}
	}

	// ----------------- Grid - Map Maker ----------------- // 

	public void makeGrid(){
		for (int i = 0; i < gameSizeX; i++) {
			for (int j = 0; j < gameSizeY; j++) 
				addTile (i * tileSize, j * tileSize);
		}
	}

	void addTile(int x, int y){
		Vector3 newTilePos = new Vector3 (x, 0f,y);
		GameObject newTile = Instantiate (defaultTile, newTilePos, Quaternion.Euler(90f,0f,0f)) as GameObject;
		newTile.transform.parent = gameObject.transform;
		// each tile recieves it's coords and named appropriately
		newTile.GetComponent<defaultTile> ().updateCoords (x, y);
		newTile.name = "Tile_" + x + "_" + y;
	}

	// -------------------- Road Template Builder -------------------- //

	public void roadBuildMode(){
			if (firstPosX == endPosX) { 
				if (endPosY > firstPosY) {
					foreach (Transform child in transform) {
						defaultTile tile = child.GetComponent<defaultTile> ();
						if (tile.xCoord == firstPosX && tile.yCoord > firstPosY && tile.yCoord < endPosY) {
							child.GetComponent<Renderer> ().material.color = Color.yellow;
						}
					}
				}
				if (endPosY < firstPosY) {
					foreach (Transform child in transform) {
						defaultTile tile = child.GetComponent<defaultTile> ();
						if (tile.xCoord == firstPosX && tile.yCoord < firstPosY && tile.yCoord > endPosY) {
							child.GetComponent<Renderer> ().material.color = Color.yellow;
						}
					}
				}
			}
			if (firstPosY == endPosY) { 
				if (endPosX > firstPosX) {
					foreach (Transform child in transform) {
						defaultTile tile = child.GetComponent<defaultTile> ();
						if (tile.yCoord == firstPosY && tile.xCoord > firstPosX && tile.xCoord < endPosX) {
							child.GetComponent<Renderer> ().material.color = Color.yellow;
						}
					}
				}
				if (endPosX < firstPosX) {
					foreach (Transform child in transform) {
						defaultTile tile = child.GetComponent<defaultTile> ();
						if (tile.yCoord == firstPosY && tile.xCoord < firstPosX && tile.xCoord > endPosX) {
							child.GetComponent<Renderer> ().material.color = Color.yellow;
						}
					}
				}
			}
	}

	// -------------------- Road Builder -------------------- //

	public void buildRoads(){
		if (firstPosX == endPosX) { 
			if (endPosY > firstPosY) {
				foreach (Transform child in transform) {
					defaultTile tile = child.GetComponent<defaultTile> ();
					if (tile.xCoord == firstPosX && tile.yCoord >= firstPosY && tile.yCoord <= endPosY) {
						//TODO reset build roads, as it still draws coords from first tile??!?
						tile.createRoad ();
						Destroy (child.gameObject);
					}
				}
			}
			if (endPosY < firstPosY) {
				foreach (Transform child in transform) {
					defaultTile tile = child.GetComponent<defaultTile> ();
					if (tile.xCoord == firstPosX && tile.yCoord <= firstPosY && tile.yCoord >= endPosY) {
						tile.createRoad ();
						Destroy (child.gameObject);
					}
				}
			}
		}
		if (firstPosY == endPosY) { 
			if (endPosX > firstPosX) {
				foreach (Transform child in transform) {
					defaultTile tile = child.GetComponent<defaultTile> ();
					if (tile.yCoord == firstPosY && tile.xCoord >= firstPosX && tile.xCoord <= endPosX) {
						tile.createRoad ();
						Destroy (child.gameObject);
					}
				}
			}
			if (endPosX < firstPosX) {
				foreach (Transform child in transform) {
					defaultTile tile = child.GetComponent<defaultTile> ();
					if (tile.yCoord == firstPosY && tile.xCoord <= firstPosX && tile.xCoord >= endPosX) {
						tile.createRoad ();
						Destroy (child.gameObject);
					}
				}
			}
		}
		if (firstPosX != endPosX && firstPosY != endPosY) {
			selectedTileToggle ();
		}
	}
	// ----------------- Create Map ----------------- //

	public void createMap(){ 
		foreach (Transform child in transform) {
			Vector3 newPos = new Vector3 (child.transform.position.x, child.transform.position.y, child.transform.position.z); 
			GameObject newTile = Instantiate (grassTile, newPos, Quaternion.Euler (0f, 90f, 0f)) as GameObject;
			newTile.transform.parent = otherTiles.gameObject.transform;
			Destroy (child.gameObject);
		}
		Destroy (gameObject);
	}


	// ----------------- end ----------------- //

}
