using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : Unit {

	private GameObject target;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	protected override void Update() {
		base.Update();
	}

	public override void OnTriggerAggroRange(GameObject other) {
		if (aggroQueue.Count < aggroLimit && !aggroQueue.Contains(other)) {
			aggroQueue.Add(other);
			other.GetComponent<Unit>().Engage(gameObject);
		} else {
			interestedEnemies.Add(other);
		}
	}


}
