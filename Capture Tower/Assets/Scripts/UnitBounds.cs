using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBounds : MonoBehaviour {

	private Unit unit;

	// Use this for initialization
	void Awake() {
		unit = GetComponentInParent<Unit>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == this.tag) return;		// Don't collide with units on the same team
		unit.OnTriggerUnitBounds(collision.transform.parent.gameObject);
	}
}
