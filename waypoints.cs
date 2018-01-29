using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waypoints : MonoBehaviour {


	private int wpnumber;

	public Vector3 nextPosition;
	public GameObject nextWayPoint;

	public bool nextIsLast;

	void Awake(){
		wpnumber = 1;
		foreach (Transform child in transform) {
			child.GetComponent<roadWaypoint> ().getWayPointNumber (wpnumber);
			wpnumber++;
		}
		foreach (Transform child in transform) {
			child.GetComponent<roadWaypoint> ().getLastWayPoint (wpnumber);
		}
	}

	public void findNextWayPointByNumber(int x){
		foreach (Transform child in transform) {
			if (child.GetComponent<roadWaypoint> ().waypointNumber == x) {
				nextPosition = child.transform.position;
				nextWayPoint = child.transform.gameObject;
				if (x == wpnumber - 1) {
					nextIsLast = true;
				} else {
					nextIsLast = false;
				}
			} 
		}
	}


	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Transform firstWaypoint = null;
		foreach (Transform child in transform) {
			Transform secondWaypoint = child;
			if (firstWaypoint != null) {
				Gizmos.DrawLine (firstWaypoint.position, secondWaypoint.position);
			}
			firstWaypoint = child;
		}
	}






}
