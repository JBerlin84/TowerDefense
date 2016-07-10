/// <summary>
/// Enemy class
/// </summary>

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour {

	NavMeshAgent pathfinder;
	Transform target;

	[Header("AI")]
	public float pathfindingRefreshRate = 0.25f;
	float nextPathFinding;

	[Header("Enemy variables")]
	public int value;
	public int hp;
	public int speed;
	public float scale;

	// Use this for initialization
	void Start () {
		pathfinder = GetComponent<NavMeshAgent> ();
		target = GameObject.FindGameObjectWithTag ("Finish").transform;
		pathfinder.speed = speed;
		transform.localScale = transform.localScale * scale;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > nextPathFinding) {
			pathfinder.SetDestination (target.position);
			nextPathFinding = Time.time + pathfindingRefreshRate;
		}
	}
}
