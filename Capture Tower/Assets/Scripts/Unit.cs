using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	protected enum State { Idle, Moving, Attacking, Dying };

	public int hp = 10;
	public float moveSpeed = 1f;
	public int damage = 1;
	public float attackSpeed = 1f;
	public int aggroLimit = 1;

	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	[SerializeField] protected List<GameObject> aggroQueue;
	[SerializeField] protected List<GameObject> interestedEnemies;
	[SerializeField] protected State state;

	// Use this for initialization
	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		aggroQueue = new List<GameObject>(aggroLimit);
		interestedEnemies = new List<GameObject>();
	}

	// Update is called once per frame
	protected virtual void Update() {
		if (state == State.Dying) {
			// Dying, do nothing else
			return;
		}
		if (aggroQueue.Count < aggroLimit && interestedEnemies.Count > 0) {
			GameObject nextEnemy = interestedEnemies[0];
			aggroQueue.Add(nextEnemy);
			interestedEnemies.RemoveAt(0);
		}
		if (aggroQueue.Count > 0 && state != State.Attacking) {
			if (aggroQueue.Count >= aggroLimit) {
				aggroQueue.Remove(null);
			}
			Debug.Log(name + " ATTACKING " + aggroQueue[0].name + "!");
			StartCoroutine("Attack", aggroQueue[0]);
		}
	}

	public virtual void OnTriggerAggroRange(GameObject other) {
	}

	public virtual void OnTriggerUnitBounds(GameObject other) {
	}

	public virtual void OnTriggerExitAggroRange(GameObject other) {
		interestedEnemies.Remove(other);
	}

	public virtual void OnTriggerExitUnitBounds(GameObject other) {
	}

	public virtual void OnClickUnitBounds() {

	}

	IEnumerator Attack(GameObject target) {
		Debug.Log("Calling attack coroutine");
		state = State.Attacking;
		animator.SetTrigger("Attack");

        if (target.transform.position.x < this.transform.position.x)
        {
            this.transform.right = Vector2.left;
        }
        else
        {
            this.transform.right = Vector2.right;
        }
		target.GetComponent<Unit>().Damage(damage);
		yield return new WaitForSeconds(attackSpeed);

		state = State.Idle;
	}

	// Engage gets called when an enemy unit begins attacking this one
	public void Engage(GameObject other) {
		// TODO: What happens ranged units engage with melee units?
		if (aggroQueue.Count < aggroLimit && !aggroQueue.Contains(other)) {
			aggroQueue.Add(other);
		} else {
			// If the engaged unit already has max aggro, put this enemy at the front of the interest list
			interestedEnemies.Insert(0, other);
		}
	}

	public void Forget(GameObject other) {
		aggroQueue.Remove(other);
		interestedEnemies.Remove(other);
	}

	// Lose [damage] amount of health
	public void Damage(int damage) {
		hp -= damage;
		if (hp <= 0) {
			StartCoroutine(Die());
		} else {
            if (animator != null)
            {
                animator.SetTrigger("Hurt");
            }
		}
	}

	IEnumerator Die() {
		state = State.Dying;
		StopCoroutine("Attack");
        if (animator != null) {
            animator.SetTrigger("Die");
		}
		foreach (GameObject o in aggroQueue) {
			o.GetComponent<Unit>().Forget(gameObject);
		}
		foreach (GameObject o in interestedEnemies) {
			o.GetComponent<Unit>().Forget(gameObject);
		}
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}
}
