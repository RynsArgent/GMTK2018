using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    public Ally allyVersion;

    private GameObject convert;
	private Path path;
	private Transform[] waypoints;
	private int currentWp = 0;
	private Transform targetWp;

    public void SetPath(Path path)
    {
        this.path = path;
        waypoints = path.waypointList;
    }

    // Update is called once per frame
    protected override void Update () {
		base.Update();
		if (state == State.Dying) {
			// Dying, do nothing else
			return;
		}
		if (aggroQueue.Count == 0 && currentWp < waypoints.Length && state != State.Attacking) {
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

	public override void OnTriggerAggroRange(GameObject other)
    {
        // If the game object is cleaned up, it will have no unit component.
        Unit otherUnitComponent = other.GetComponent<Unit>();
        if (otherUnitComponent == null)
        {
            return;
        }

        if (other.name != "Tower")
        {
            // Quick way to make the enemies aggro the tower only and not all attack the allied slime.
            return;
        }
		if (aggroQueue.Count < aggroLimit && !aggroQueue.Contains(other)) {
			aggroQueue.Add(other);
			other.GetComponent<Unit>().Engage(gameObject);
		}
	}

    public override void OnClickUnitBounds() {
        
        // Instantiate Ally object and destroy this (cool conversion gfx?)
        Instantiate(allyVersion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(gameObject);
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

	
}
