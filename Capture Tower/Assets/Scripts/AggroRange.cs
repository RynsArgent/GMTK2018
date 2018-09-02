﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroRange : MonoBehaviour {

	private Unit unit;

	// Use this for initialization
	void Awake() {
		unit = GetComponentInParent<Unit>();
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == this.tag) return;      // Don't collide with units on the same team
		Debug.Log(this.transform.parent.name + ".AggroRange: \"" +
			collision.transform.parent.name + "." + collision.name + " triggered aggro bounds.\"");
		unit.OnTriggerAggroRange(collision.transform.parent.gameObject);
	}
}
