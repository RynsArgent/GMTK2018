using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

	public Transform[] wayPointsList;
	public int currentWayPoint = 0;
	public float speed = 3f;

	private Transform targetWaypoint;
	private Transform unitObjTransform;

	// Use this for initialization
	void Awake () {
		unitObjTransform = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentWayPoint < wayPointsList.Length) {
			if (targetWaypoint == null)
				targetWaypoint = wayPointsList[currentWayPoint];
			Move();
		}
		
	}

	private void Move() {
		unitObjTransform.position = Vector3.MoveTowards(unitObjTransform.position, targetWaypoint.position, speed * Time.deltaTime);

		if (unitObjTransform.position == targetWaypoint.position) {
			currentWayPoint++;
			targetWaypoint = wayPointsList[currentWayPoint];
		}
	}
}
