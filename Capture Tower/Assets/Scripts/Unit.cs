using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public int hp = 10;
	public float moveSpeed = 1f;
	public int damage = 1;
	public float attackSpeed = 1f;
	public int aggroLimit = 1;

	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	protected Queue<GameObject> aggroQueue;
	protected List<GameObject> interestedEnemies;
	
	private bool readyToAttack = true;

	// Use this for initialization
	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		aggroQueue = new Queue<GameObject>(aggroLimit);
		interestedEnemies = new List<GameObject>();
	}

	// Update is called once per frame
	protected virtual void Update() {
		if (aggroQueue.Count < aggroLimit && interestedEnemies.Count > 0) {
			GameObject nextEnemy = interestedEnemies[0];
			aggroQueue.Enqueue(nextEnemy);
			interestedEnemies.RemoveAt(0);
		}
		if (aggroQueue.Count > 0 && readyToAttack) {
			try {
				Debug.Log(name + " ATTACKING " + aggroQueue.Peek().name + "!");
				StartCoroutine(Attack(aggroQueue.Peek()));
			} catch (MissingReferenceException) {
				// The targeted enemy has died, so skip trying to attack and remove the empty reference.
				aggroQueue.Dequeue();
			}
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

	IEnumerator Attack(GameObject target) {
		Debug.Log("Calling attack coroutine");
		readyToAttack = false;
		animator.SetTrigger("Attack");

		target.GetComponent<Unit>().Damage(damage);
		yield return new WaitForSeconds(attackSpeed);

		readyToAttack = true;
	}

	// Engage gets called when an enemy unit begins attacking this one
	public void Engage(GameObject other) {
		// TODO: What happens ranged units engage with melee units?
		if (aggroQueue.Count < aggroLimit && !aggroQueue.Contains(other)) {
			aggroQueue.Enqueue(other);
		} else {
			// If the engaged unit already has max aggro, put this enemy at the front of the interest list
			interestedEnemies.Insert(0, other);
		}
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
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
		Destroy(gameObject);
	}
}
