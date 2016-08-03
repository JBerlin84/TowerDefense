using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public abstract class Tower : MonoBehaviour {

	[Header("Tower")]
	public string towerName;
	public int price;
	public float attackSpeed;
	protected float nextAttackTime;
	public float range;
	public AudioClip fireSound;
	[Range(0,1)] public float fireLowRange;
	[Range(0,1)] public float fireHighRange;
	[Range(0,2)] public float lowPitchRange;
	[Range(0,2)] public float highPitchRange;

	[Header("Bullet")]
	public Transform muzzle;
	public Projectile projectile;
	public float speedBonus;
	public float damageBonus;

	protected Vector3 myPosition;
	protected AudioSource audioSource;

	// Use this for initialization
	void Start () {
		myPosition = this.transform.position;
		audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	abstract protected void Update (); /* {
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
					Debug.DrawLine (muzzle.transform.position, target);

					Fire ();
					break;
				}
			}
		}
	}*/

	/// <summary>
	/// Fire tower.
	/// </summary>
	virtual protected void Fire() {
		if (Time.time > nextAttackTime) {
			float vol = Random.Range (fireLowRange, fireHighRange);
			float pitch = Random.Range (lowPitchRange, highPitchRange);
			audioSource.pitch = pitch;
			audioSource.PlayOneShot(fireSound, vol);
		}
		/*
		if (Time.time > nextAttackTime) {
			Projectile newProjectile = Instantiate (projectile, muzzle.position, muzzle.rotation) as Projectile;
			newProjectile.speed *= speedBonus;
			newProjectile.damage *= damageBonus;
			nextAttackTime = Time.time + attackSpeed;
		}*/
	}

	/// <summary>
	/// Returns a string that represents the current tower.
	/// </summary>
	/// <returns>A string that represents the current tower.</returns>
	/// <filterpriority>2</filterpriority>
	public override string ToString() {
		string data;

		data = towerName + "\n";
		data += "-----------------\n";
		data += "Resources: " + price + "\n";
		data += "Attack speed: " + attackSpeed + "\n";
		data += "Range: " + range + "\n";
		data += "\n";
		data += "Speed bonus: " + speedBonus + "\n";
		data += "Damage bonus: " + damageBonus + "\n";
		data += "\n";
		data += "Projectile\n";
		data += "----------\n";
		data += projectile.ToString ();

		return data;
	}
}
