using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mapBuilderUI : MonoBehaviour {

	public GameObject gameTileMaster;
	private gameTiles gameTilesGO;

	public GameObject roadTileMaster;
	private RoadTiles roadTilesGO;

	public Button placeRoad;
	private bool placeRoadPressed = false;

	public Button confirm;

	public GameObject gameUI;

	// Use this for initialization
	void Start () {
		Button placeRoadBtn = placeRoad.GetComponent<Button> ();
		placeRoadBtn.onClick.AddListener (placeRoadBool);

		Button confirmBtn = confirm.GetComponent<Button> ();
		confirmBtn.onClick.AddListener (buildMap);

		gameTilesGO = gameTileMaster.GetComponent<gameTiles> ();
		roadTilesGO = roadTileMaster.GetComponent<RoadTiles> ();
	}


	void placeRoadBool(){
		toggleButton (placeRoad);
		gameTilesGO.toggleRoadBuilder ();
	}


	void toggleButton(Button btn){
		if (placeRoadPressed) {
			placeRoadPressed = false;
			btn.GetComponent<Image> ().color = Color.white;
		} else {
			placeRoadPressed = true;
			btn.GetComponent<Image> ().color = Color.green;
		}
	}

	void buildMap(){
		placeRoadBool ();
		roadTilesGO.createRoads ();
		gameTilesGO.createMap ();
		Destroy (gameObject);
		gameUI.SetActive (true);
	}

}
