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
		if (collision.tag == this.tag || collision.gameObject.layer == 10) return;  // Don't collide with units on the same team or AggroBounds
		Debug.Log(this.transform.parent.name + ".UnitBounds: \"" +
			collision.transform.parent.name + "." + collision.name + " triggered unit bounds.\"");
		unit.OnTriggerUnitBounds(collision.transform.parent.gameObject);
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == this.tag || collision.gameObject.layer == 10) return;  // Don't collide with units on the same team or AggroBounds
		unit.OnTriggerExitUnitBounds(collision.transform.parent.gameObject);
	}
}
