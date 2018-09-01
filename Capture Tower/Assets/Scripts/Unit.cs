using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

	public float speed = 3f;

	protected SpriteRenderer spriteRenderer;
	protected Animator animator;

	// Use this for initialization
	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update() {
	}
}
