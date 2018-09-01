using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

	public Transform[] wayPointsList;
	public int currentWayPoint = 0;

	private Transform targetWaypoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWayPoint < wayPointsList.Length) {
			if (targetWaypoint == null)
				targetWaypoint = wayPointsList[currentWayPoint];
			move();
		}
		
	}

	private void move() {

	}
}
