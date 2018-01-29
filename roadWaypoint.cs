using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadWaypoint : MonoBehaviour {

	private float gizmoSize = 0.1f;

	public int waypointNumber;
	public int lastWayPoint;

	public bool iAmLast;

	void Start(){
		if (waypointNumber == lastWayPoint) {
			iAmLast = true;
		}
	}

	public void getWayPointNumber(int x){
		waypointNumber = x;
	}

	public void getLastWayPoint(int x){
		lastWayPoint = x;
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.white;
		Gizmos.DrawSphere (transform.position, gizmoSize);
	}


}
