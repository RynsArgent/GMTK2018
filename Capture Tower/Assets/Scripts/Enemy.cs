using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit {

    public Ally allyVersion;
    public int ConversionCost = 10;

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
		} else if (target != null) {
			if (state != State.Attacking) {
				Debug.Log(name + " ATTACKING " + target.name + "!");
				StartCoroutine("Attack", target);
			}
		} else if (currentWp < waypoints.Length && state != State.Attacking) {
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
        if (otherUnitComponent == null) {
            return;
        }

		if (target == null) {
			bool otherHasAggroCapacity = otherUnitComponent.Engage(gameObject);
			if (otherHasAggroCapacity) {
				target = other;
			}
		}
	}

    public override void OnClickUnitBounds()
    {
        if (GameController.Mana >= ConversionCost + 0)
        {
            // Instantiate Ally object and destroy this (cool conversion gfx?)
            Instantiate(allyVersion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(gameObject);
            GameController.Mana -= ConversionCost;
        }
    }

	private void MoveAlongPath() {
		Vector3 oldPosition = transform.root.position;
		transform.root.position = Vector3.MoveTowards(transform.root.position, targetWp.position, moveSpeed * Time.deltaTime);

		if (transform.root.position.x < oldPosition.x) {
			spriteRenderer.flipX = true;
		} else {
			spriteRenderer.flipX = false;
		}

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
