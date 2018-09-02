using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

	public Ally allyVersion;

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

	// Detects when its being lazered
	public override void OnTriggerUnitBounds(GameObject other) {
		if (other.name == "NecroHit") {
			hp -= 10;
		}
	}

	public override void OnClickUnitBounds() {
		Convert();
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
		// Instantiate Ally object and destroy this (cool conversion gfx?)
		Instantiate(allyVersion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
		Destroy(gameObject);
	}
}
