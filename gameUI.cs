using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameUI : MonoBehaviour {

	public Button addCar;
	public GameObject orangeCar;
	public GameObject allCars;

	private GameObject[] waypoints;

	// Use this for initialization
	void Start () {
		transform.gameObject.SetActive (false);

		Button confirmBtn = addCar.GetComponent<Button> ();
		confirmBtn.onClick.AddListener (addCarToMap);
	}
	
	void addCarToMap(){
		waypoints = GameObject.FindGameObjectsWithTag ("waypoint");
		List<GameObject> firstWayPoints = new List<GameObject> ();
		foreach (GameObject wp in waypoints) {
			if (wp.GetComponent<roadWaypoint> ().waypointNumber == 1) {
				firstWayPoints.Add (wp);
			}
		}
		int random = Random.Range (0, firstWayPoints.Count);
		Vector3 newLoc = firstWayPoints [random].transform.position;

		Vector3 newPos = new Vector3 (newLoc.x, newLoc.y, newLoc.z); 
		GameObject newCar = Instantiate (orangeCar, newLoc, Quaternion.Euler (0f, 0f, 0f)) as GameObject;
		newCar.transform.parent = allCars.gameObject.transform;
	}




}
