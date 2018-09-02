using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : Unit {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
	}

	public override void OnTriggerUnitBounds(GameObject other) {
		Destroy(other);
		Damage(1);
	}
}
