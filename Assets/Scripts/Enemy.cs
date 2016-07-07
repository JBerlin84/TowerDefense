/// <summary>
/// Enemy class
/// </summary>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	NavMeshAgent pathfinder;
	Transform target;

	public float pathfindingRefreshRate = 0.25f;
	float nextPathFinding;

	// Use this for initialization
	void Start () {
		pathfinder = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Finish").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextPathFinding) {
			pathfinder.SetDestination (target.position);
			nextPathFinding = Time.time + pathfindingRefreshRate;
		}
	}
}
