using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {
	private Path path;
	private Transform[] waypoints;
	private int currentWp = 0;
	private Transform targetWp;

	// Use this for initialization
	void Start () {
		path = GameObject.Find("Path").GetComponent<Path>();
		waypoints = path.waypointList;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update();

		if (aggroQueue.Count == 0 && currentWp < waypoints.Length) {
			if (targetWp == null)
				targetWp = waypoints[currentWp];

			animator.SetBool("Move", true);
			if (transform.position.x < targetWp.position.x) {
				spriteRenderer.flipX = true;
			} else {
				spriteRenderer.flipX = false;
			}

			MoveAlongPath();
		}
	}

	public override void OnTriggerAggroRange(GameObject other) {
		// Override Unit.OnTriggerAggroRange to do nothing
	}

	private void MoveAlongPath() {
		transform.root.position = Vector3.MoveTowards(transform.root.position, targetWp.position, moveSpeed * Time.deltaTime);

		if (transform.root.position == targetWp.position) {
			currentWp++;
			if (currentWp == waypoints.Length) {
				animator.SetBool("Move", false);
				return;
			}
			targetWp = waypoints[currentWp];
		}
	}

	public void Convert() {
		// Death/conversion animation stuff

		// Instantiate Ally object and destroy this
	}
}
