using UnityEngine;
using System.Collections;

public class LaserRig : Tower {

	// Update is called once per frame
	protected override void Update () {
		//TODO: Play idle animation of tower.
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

		mainTurret.transform.Rotate (Vector3.up * Time.deltaTime * idleRotationSpeed);

		if (enemies.Length > 0)
		{
			for (int i = 0; i < enemies.Length; i++)
			{
				float distance = Vector3.Distance(myPosition, enemies[i].transform.position);
				if (distance < range)
				{
					// Enemy is within distance of turret, aim at enemy
					Vector3 target = enemies[i].transform.position;
					target.y = muzzle.transform.position.y;

					// Rotate tower towards enemy
					Vector3 relativePos = target - transform.position;
					relativePos.y = transform.position.y;
					mainTurret.transform.rotation = Quaternion.LookRotation(relativePos);

					muzzle.LookAt(target);
					Debug.DrawLine(muzzle.transform.position, target);

					Fire(muzzle.transform, enemies[i]);
					break;
				}
			}
		}
	}

	void Fire(Transform muzzleTarget, GameObject hitTarget) {
		base.Fire ();

		if (Time.time > nextAttackTime)
		{
			Laser newLaser = Instantiate(projectile, muzzle.position, muzzle.rotation) as Laser;
			newLaser.speed *= speedBonus;
			newLaser.damage *= damageBonus;

			newLaser.startPosition = muzzle.position;
			newLaser.endPosition = hitTarget.transform.position;
			newLaser.hitTarget = hitTarget.GetComponent<Enemy> ();
			newLaser.muzzleTarget = muzzleTarget;

			nextAttackTime = Time.time + attackSpeed;
		}
	}
}