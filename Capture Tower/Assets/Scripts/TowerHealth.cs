using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : Unit {

	// Update is called once per frame
	protected override void Update () {
		if (hp <= 0) {
            Destroy(gameObject);
        }
	}
}
