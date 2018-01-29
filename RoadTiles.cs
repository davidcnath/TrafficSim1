using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTiles : MonoBehaviour {

	private GameObject roadTiles;
	public GameObject roadNetwork;

	public GameObject[] roadTilePrefabs;
	private GameObject selectedRoadTile;

	private bool upRoad;
	private bool downRoad;
	private bool leftRoad;
	private bool rightRoad;

	private string up;
	private string down;
	private string left;
	private string right;

	void Start(){
		roadTiles = transform.gameObject;
		//roadNetwork = transform.parent.parent.Find("roadNetwork").gameObject;
	}

	public void getNeighbourCoords (string tileUp, string tileDown, string tileLeft, string tileRight){
		up = tileUp;
		down = tileDown;
		left = tileLeft;
		right = tileRight;
	}

	void selectRoadTile(){
		if (!upRoad && !downRoad && !leftRoad && !rightRoad) 	{	selectedRoadTile = null; Debug.Log ("Error");	}
		if (!upRoad && downRoad && !leftRoad && !rightRoad) 	{	selectedRoadTile = roadTilePrefabs[0];	}
		if (!upRoad && !downRoad && !leftRoad && rightRoad) 	{	selectedRoadTile = roadTilePrefabs[1];	}
		if (upRoad && !downRoad && !leftRoad && !rightRoad) 	{	selectedRoadTile = roadTilePrefabs[2];	}
		if (!upRoad && !downRoad && leftRoad && !rightRoad) 	{	selectedRoadTile = roadTilePrefabs[3];	}
		if (upRoad && downRoad && !leftRoad && !rightRoad) 		{	selectedRoadTile = roadTilePrefabs[4];	}
		if (!upRoad && !downRoad && leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[5];	}
		if (upRoad && !downRoad && leftRoad && !rightRoad) 		{	selectedRoadTile = roadTilePrefabs[6];	}
		if (!upRoad && downRoad && leftRoad && !rightRoad) 		{	selectedRoadTile = roadTilePrefabs[7];	}
		if (upRoad && !downRoad && !leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[8];	}
		if (!upRoad && downRoad && !leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[9];	}
		if (upRoad && downRoad && leftRoad && !rightRoad) 		{	selectedRoadTile = roadTilePrefabs[10];	}
		if (upRoad && !downRoad && leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[11];	}
		if (upRoad && downRoad && !leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[12];	}
		if (!upRoad && downRoad && leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[13];	}
		if (upRoad && downRoad && leftRoad && rightRoad) 		{	selectedRoadTile = roadTilePrefabs[14];	}
	}


	public void createRoads(){
		// foreach road tile, roadtilescript, getroadtilecoords, roadtile.checkupdownleftright(), roadtile.selectcorrecttile(), replacewithtile(x, y);
		foreach(Transform child in transform){
			upRoad = false; downRoad = false; leftRoad = false; rightRoad = false;
			roadPlaceholder tile = child.GetComponent<roadPlaceholder> ();
			tile.updateCoords ();
			foreach(Transform otherChild in roadTiles.transform){
				roadPlaceholder otherTile = otherChild.GetComponent<roadPlaceholder> ();
				if(otherTile.xyCoords == up){upRoad = true; }  
				if(otherTile.xyCoords == down){downRoad = true; } 
				if(otherTile.xyCoords == left){leftRoad = true; } 
				if(otherTile.xyCoords == right){rightRoad = true; }
			}
			selectRoadTile ();
			if (selectedRoadTile != null) { 
				Vector3 newTilePos = new Vector3 (tile.xCoord, 0, tile.yCoord);
				GameObject newTile = Instantiate (selectedRoadTile, newTilePos, Quaternion.Euler (0f, 0f, 0f)) as GameObject;
				newTile.transform.parent = roadNetwork.transform;
			}
		}
		Destroy (gameObject);
	}


}
