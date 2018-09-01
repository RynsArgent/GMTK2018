using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public float moveSpeed = 3f;
	public float attackSpeed = 1f;

	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	protected GameObject target;
	
	private bool targetInRange = false;

	// Use this for initialization
	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	protected virtual void Update() {
		if (target != null && targetInRange) {
			Attack();
		}
	}

	public void OnTriggerAggroRange(GameObject other) {
		Debug.Log(name + ": \"" + other.name + " entered aggro range.\"");

	}

	public void OnTriggerUnitBounds(GameObject other) {
		Debug.Log(name + ": \"" + other.name + " entered unit bounds.\"");
		target = other;
		targetInRange = true;
	}

	protected void MoveToTarget() {
		transform.root.position = Vector3.MoveTowards(transform.root.position, target.transform.position, moveSpeed * Time.deltaTime);

		if (transform.root.position == target.transform.position) {
			//currentWp++;
			//if (currentWp == waypoints.Length) {
			//	animator.SetBool("Move", false);
			//	return;
			//}
			//targetWp = waypoints[currentWp];
		}
	}

	void Attack() {
		animator.SetTrigger("Attack");
		Destroy(target);
		target = null;
		targetInRange = false;
		return;
	}
}
