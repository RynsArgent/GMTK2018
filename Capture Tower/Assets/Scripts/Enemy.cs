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

	// Use this for initialization
	void Start () {
		path = GameObject.Find("Path").GetComponent<Path>();
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

            if (Input.GetMouseButtonDown(0) && GameController.skill == 1)
            {
                Damage(1);
            }
		}
	}

	public override void OnTriggerAggroRange(GameObject other) {
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

        if (GameController.Mana > 0)
        {
            if (GameController.skill == 2)
            { 
                // Instantiate Ally object and destroy this (cool conversion gfx?)
                Instantiate(allyVersion, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                Destroy(gameObject);
                GameController.Mana -= 10;
            }
        }
        
        
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
