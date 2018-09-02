using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerHealth : Unit {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (hp <= 0) {
            Destroy(gameObject);
        }
	}
}
