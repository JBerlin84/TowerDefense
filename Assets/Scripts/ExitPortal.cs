﻿/// <summary>
/// The code handling the exit portal from the world.
/// </summary>

using UnityEngine;
using System.Collections;

public class ExitPortal : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag.Equals("Enemy")) {
			Enemy enemy = other.GetComponent<Enemy> ();
			enemy.PortalOut ();
		}
	}
}
