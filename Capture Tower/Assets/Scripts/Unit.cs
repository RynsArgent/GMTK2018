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
    public int manaLoot = 10;

	public AudioClip hitsound1;
	public AudioClip hitsound2;

	protected SpriteRenderer spriteRenderer;
	protected Animator animator;
	[SerializeField] protected GameObject target;
	[SerializeField] protected List<GameObject> aggroQueue;
	[SerializeField] protected List<GameObject> interestedEnemies;
	[SerializeField] protected State state;

    public GameObject deathEffectPrefab;
    public GameObject onReceiveHitEffectPrefab;

    // Use this for initialization
    void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		aggroQueue = new List<GameObject>(aggroLimit);
		interestedEnemies = new List<GameObject>();
	}

	// Update is called once per frame
	protected virtual void Update() {
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

    private GameObject attackTarget;
	IEnumerator Attack(GameObject target) {
		Debug.Log("Calling attack coroutine");
		state = State.Attacking;
		animator.SetTrigger("Attack");

        if (target.transform.position.x < this.transform.position.x) {
            this.transform.right = Vector2.left;
        } else {
            this.transform.right = Vector2.right;
        }
        attackTarget = target;
		yield return new WaitForSeconds(attackSpeed);   

        state = State.Idle;
	}

    public void OnAttackFrame()
    {
        if (attackTarget != null)
        {
            attackTarget.GetComponent<Unit>().Damage(damage);
            attackTarget = null;
        }
    }

	// Check if this unit has aggro capacity left to aggro another enemy
	// Returns true if other can be added
	public virtual bool Engage(GameObject other) {
		return false;
	}

	public void Forget(GameObject other) {
		aggroQueue.Remove(other);
		interestedEnemies.Remove(other);
	}

	// Lose [damage] amount of health
	public void Damage(int damage) {
		SoundManager.instance.RandomizeSfx(hitsound1, hitsound2);
		hp -= damage;
		if (hp <= 0) {
			StartCoroutine(Die());
		} else {
            if (animator != null)
            {
                animator.SetTrigger("Hurt");
            }
            if (onReceiveHitEffectPrefab != null)
            {
                GameObject.Instantiate<GameObject>(onReceiveHitEffectPrefab, this.transform.position, Quaternion.identity);
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
        if (deathEffectPrefab != null)
        {
            GameObject.Instantiate<GameObject>(deathEffectPrefab, this.transform.position, Quaternion.identity);
        }
        if (gameObject.tag == "Enemy")
        {
            GameController.Mana += manaLoot;
        }
        Destroy(gameObject);
        yield return null;
	}
}
