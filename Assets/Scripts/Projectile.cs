using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask hitMask;

	public float speed;
	public float damage;

	// Update is called once per frame
	void Update () {
		float distanceToTravel = speed * Time.deltaTime;

		CheckCollision (distanceToTravel);

		transform.Translate (Vector3.forward * distanceToTravel);
	}

	void CheckCollision(float distanceToTravel) {
		Ray ray = new Ray (transform.position, transform.forward);
		RaycastHit hit;

		if (Physics.Raycast (ray, out hit, distanceToTravel, hitMask, QueryTriggerInteraction.Collide)) {
			OnHitObject (hit);
		}
	}

	void OnHitObject(RaycastHit hit) {
		
		GameObject target = hit.collider.gameObject;
		if (target.tag.Equals("Enemy")) {
			target.GetComponent<Enemy> ().TakeHit (damage, hit);
		}
		// The projectile should be destroyed
		GameObject.Destroy (gameObject);
	}
}
