using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	public float attackSpeed;
	float nextAttackTime;
	public float range;
	public int damage;

	Vector3 myPosition;

	// Use this for initialization
	void Start () {
		myPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		// We only need to do this if we can attack, otherwise it is just a waste of calculations.
		if (Time.time > nextAttackTime) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

			if (enemies.Length > 0) {
				for (int i = 0; i < enemies.Length; i++) {
					float distance = Vector3.Distance (myPosition, enemies [i].transform.position);
					if (distance < range) {
						Debug.DrawLine (myPosition, enemies [i].transform.position, Color.red, 0.25f);
						print ("attacking");
						break;
					}
				}
			}

			nextAttackTime = Time.time + attackSpeed;
		}
	}
}
