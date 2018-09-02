using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Unit {
	
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	protected override void Update() {
		if (state == State.Dying) {
			// Dying, do nothing else
			return;
		}
		if (aggroQueue.Count < aggroLimit && interestedEnemies.Count > 0) {
			GameObject nextEnemy = interestedEnemies[0];
			aggroQueue.Add(nextEnemy);
			interestedEnemies.RemoveAt(0);
		}
		if (target != null) {
			if (state != State.Attacking) {
				Debug.Log(name + " ATTACKING " + target.name + "!");
				StartCoroutine("Attack", target);
			}
		} else if (aggroQueue.Count > 0) {
			target = aggroQueue[0];
			aggroQueue.RemoveAt(0);
		}
	}

	public override void OnTriggerAggroRange(GameObject other) {
        // If the game object is cleaned up, it will have no unit component.
        Unit otherUnitComponent = other.GetComponent<Unit>();
        if (otherUnitComponent == null) {
            return;
        }

		if (aggroQueue.Count < aggroLimit && !aggroQueue.Contains(other)) {
			if (target == null) {
				target = other;
			}
			//aggroQueue.Add(other);
		} else {
			interestedEnemies.Add(other);
		}
	}
	
	// Check if this unit has aggro capacity left to aggro another enemy
	// Returns true if other can be added
	public override bool Engage(GameObject other) {
		if (aggroQueue.Contains(other)) {
			return true;
		} else if (aggroQueue.Count < aggroLimit) {
			aggroQueue.Add(other);
			return true;
		} else {
			// If this unit already has max aggro, move or put this enemy at the front of the interest list
			interestedEnemies.Remove(other);
			interestedEnemies.Insert(0, other);
			return false;
		}
	}


}
