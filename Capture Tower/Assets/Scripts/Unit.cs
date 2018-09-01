using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	public bool isFriendly = false;
	public Path path;
	public float speed = 3f;

	// Use this for initialization
	private void Awake() {
		path = GetComponent<Path>();
	}

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
