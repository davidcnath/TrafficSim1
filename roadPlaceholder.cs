using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadPlaceholder : MonoBehaviour {
	
	private RoadTiles roadManager;

	public int xCoord;
	public int yCoord;
	public string xyCoords; //unique position

	private string up;
	private string down;
	private string left;
	private string right;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<Renderer> ().material.color = Color.grey;
		roadManager = transform.parent.GetComponent<RoadTiles> ();
		findNeighbourCoords ();
	}

	void findNeighbourCoords (){
		up = xCoord + " " + (yCoord + 3);
		down = xCoord + " " + (yCoord - 3);
		left = (xCoord - 3) + " " + yCoord;
		right = (xCoord + 3) + " " + yCoord;	
	}

	public void updateCoords(){
		roadManager.getNeighbourCoords (up, down, left, right);
	}


	public void getCoords(int x, int y){
		xCoord = x;
		yCoord = y;
		xyCoords = x + " " + y;
		findNeighbourCoords ();
	}

}
