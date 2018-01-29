using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class orangeCar : MonoBehaviour {

	public float driveSpeed = 5;

	private GameObject[] waypoints;

	private bool lookForStart = true; 

	private float nearestStart;
	private Vector3 nextPosition;

	private GameObject nextWayPoint;
	private int wayPointNumber;

	void Start(){
		waypoints = GameObject.FindGameObjectsWithTag ("waypoint");
		findNearestStart ();
	}


	void Update () {
		moveTo (nextPosition);
		if (!lookForStart) {
			if (Vector3.Distance (transform.position, nextPosition) < 0.1f) {
				findNextPosition ();
			}
		} else if (lookForStart) {
			if (Vector3.Distance (transform.position, nextPosition) < 0.1f) {
				findNearestStart ();
			}
		}		
	}

	// --------------------- Code for contact with other cars ----------------------------- // 




	// -------------- ----- Code for finding where to go and moving there -------------------- //

	void findNextPosition(){
		GameObject waypointManager = nextWayPoint.transform.parent.gameObject; 
		wayPointNumber++;
		waypointManager.GetComponent<waypoints> ().findNextWayPointByNumber (wayPointNumber);
		nextPosition = waypointManager.GetComponent<waypoints> ().nextPosition;
		nextWayPoint = waypointManager.GetComponent<waypoints> ().nextWayPoint;
		if (waypointManager.GetComponent<waypoints>().nextIsLast) {
			lookForStart = true;
		}
	}

	//need to get it to differentiate between starts at the same location. 

	void findNearestStart(){
		nearestStart = 10000000000;
		List<GameObject> firstWayPoints = new List<GameObject> ();
		Vector3 carPos = transform.position;
		//loops through waypoints, selecting the closest numbered 1
		foreach (GameObject wp in waypoints) {
			int wpNum = wp.GetComponent<roadWaypoint> ().waypointNumber;
			Vector3 wayPointPos = wp.transform.position;
			float distanceToWP = Vector3.Distance (carPos, wayPointPos);
			if (distanceToWP < nearestStart && wpNum == 1) {
				nextWayPoint = wp;
				nearestStart = Vector3.Distance (carPos, nextWayPoint.transform.position);
				nextPosition = nextWayPoint.transform.position;
				wayPointNumber = 1;
			}
		}
		// loops through waypoints, if there are multiple at nearest, then it adds them to list
		foreach (GameObject wp in waypoints) {
			int wpNum = wp.GetComponent<roadWaypoint> ().waypointNumber;
			Vector3 wayPointPos = wp.transform.position;
			float distanceToWP = Vector3.Distance (carPos, wayPointPos);
			if (distanceToWP == nearestStart && wpNum == 1) {
				firstWayPoints.Add (wp);
			}
		}
		// if list has more than one in, then it will randomly select a start.
		if (firstWayPoints.Count > 1) {
			int ranNum = Random.Range (0, firstWayPoints.Count);
			nextWayPoint = firstWayPoints [ranNum];
		}

		nextPosition = nextWayPoint.transform.position;
		wayPointNumber = 1;
		lookForStart = false;
	}

	void moveTo(Vector3 nextPos){ 
		float speed = Time.deltaTime * driveSpeed;
		transform.position = Vector3.MoveTowards (transform.position, nextPos, speed);

		Vector3 targetDirection = nextPos - transform.position;
		Vector3 newPos = Vector3.RotateTowards (transform.forward, targetDirection, speed, 0.0f);
		transform.rotation = Quaternion.LookRotation (newPos);
	}



}
