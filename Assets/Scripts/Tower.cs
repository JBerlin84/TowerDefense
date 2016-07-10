using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

	[Header("Tower")]
	public float attackSpeed;
	float nextAttackTime;
	public float range;

	[Header("Bullet")]
	public Transform muzzle;
	public Projectile projectile;
	public float speedBonus;
	public float damageBonus;

	Vector3 myPosition;

	// Use this for initialization
	void Start () {
		myPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		//TODO: Play idle animation of tower.
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (enemies.Length > 0) {
			for (int i = 0; i < enemies.Length; i++) {
				float distance = Vector3.Distance (myPosition, enemies [i].transform.position);
				if (distance < range) {
					// Enemy is within distance of turret, aim at enemy
					Vector3 target = enemies[i].transform.position;
					target.y = muzzle.transform.position.y;
					muzzle.LookAt (target);


					Debug.DrawLine (myPosition, enemies [i].transform.position, Color.red, 0.25f);
					Fire ();
					break;
				}
			}
		}
	}

	void Fire() {
		if (Time.time > nextAttackTime) {
			Projectile newProjectile = Instantiate (projectile, muzzle.position, muzzle.rotation) as Projectile;
			newProjectile.speed *= speedBonus;
			newProjectile.damage *= damageBonus;
			nextAttackTime = Time.time + attackSpeed;
		}
	}
}
