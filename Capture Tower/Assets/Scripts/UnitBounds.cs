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
                                                                                    //Debug.Log(this.transform.parent.name + ".UnitBounds: \"" +
                                                                                    //collision.transform.parent.name + "." + collision.name + " triggered unit bounds.\"");
        if (collision.tag == "Enemy" || collision.tag == "Ally")
        {
            unit.OnTriggerUnitBounds(collision.transform.parent.gameObject);
        }
        else
        {
            unit.OnTriggerUnitBounds(collision.gameObject);
        }
	}

	private void OnTriggerExit2D(Collider2D collision) {
		if (collision.tag == this.tag || collision.gameObject.layer == 10) return;  // Don't collide with units on the same team or AggroBounds
        if (collision.tag == "Enemy" || collision.tag == "Ally")
        {
            unit.OnTriggerExitUnitBounds(collision.transform.parent.gameObject);
        }
        else
        {
            unit.OnTriggerExitUnitBounds(collision.gameObject);
        }
	}
}
