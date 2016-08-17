using UnityEngine;
using System.Collections;

public class Dread : Tower {

	// Update is called once per frame
	protected override void Update () {
		//TODO: Play idle animation of tower.

		mainTurret.transform.Rotate (Vector3.up * Time.deltaTime * idleRotationSpeed);

		if (Time.time > nextAttackTime) {
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
			if (enemies.Length > 0)
			{
				for (int i = 0; i < enemies.Length; i++) {
					float distance = Vector3.Distance (myPosition, enemies [i].transform.position);
					if (distance < range) {
						// Enemy is within distance of turret, aim at enemy
						Vector3 target = enemies [i].transform.position;
						target.y = muzzle.transform.position.y;

						// Rotate tower towards enemy
						Vector3 relativePos = target - transform.position;
						relativePos.y = transform.position.y;
						mainTurret.transform.rotation = Quaternion.LookRotation (relativePos);

						muzzle.LookAt (target);
						Debug.DrawLine (muzzle.transform.position, target);

						Fire ();
						break;
					}
				}
			}
		}
	}

	protected override void Fire() {
		base.Fire ();

		if (Time.time > nextAttackTime)
		{
			Projectile newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile;
			newProjectile.speed *= speedBonus;
			newProjectile.damage *= damageBonus;
			nextAttackTime = Time.time + attackSpeed;
		}
	}
}