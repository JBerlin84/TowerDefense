using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public LayerMask hitMask;

	public string projectileName;
	public float speed;
	public float damage;
	public float lifetime;

	protected float killtime;

	public float Lifetime {
		get { return lifetime; }
	}

	protected virtual void Start() {
		killtime = Time.time + lifetime;
	}

	// Update is called once per frame
	protected virtual void Update () {
		float distanceToTravel = speed * Time.deltaTime;

		CheckCollision (distanceToTravel);

		transform.Translate (Vector3.forward * distanceToTravel);

		if (Time.time > killtime) {
			GameObject.Destroy (gameObject);
		}
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

	public override string ToString() {
		string data;

		data = projectileName + "\n";
		data += "Speed: " + speed +"\n";
		data += "Damage" + damage +"\n";

		return data;
	}
}
