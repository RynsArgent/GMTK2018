using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public int hp = 2;
	public float moveSpeed = 3f;
	public float attackSpeed = 1f;

	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	protected GameObject target = null;
	
	private bool targetInRange = false;
	public bool readyToAttack = true;

	// Use this for initialization
	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	protected virtual void Update() {
		if (target != null && targetInRange && readyToAttack) {
			Debug.Log(name + " attacking " + target.name + "!");
			StartCoroutine(Attack());
		}
	}

	public void OnTriggerAggroRange(GameObject other) {


	}

	public void OnTriggerUnitBounds(GameObject other) {
		target = other;
		targetInRange = true;
	}

	IEnumerator Attack() {
		readyToAttack = false;
		animator.SetTrigger("Attack");
		target.GetComponent<Unit>().Damage(1);
		if (target == null) targetInRange = false;
		yield return new WaitForSeconds(attackSpeed);
		readyToAttack = true;
	}

	public void Damage(int damage) {
		hp -= damage;
		if (hp <= 0) {
			StartCoroutine(Die());
		} else {
			animator.SetTrigger("Hurt");
		}
	}

	IEnumerator Die() {
		animator.SetTrigger("Die");
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}
}
