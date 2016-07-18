using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public int startHP;
	public int startResources;
	int hp;
	bool dead;
	int resources;

	public bool Dead {
		get { return dead; }
	}

	public int Hp {
		get { return hp; }
	}

	public int Resources {
		get { return resources; }
	}

	public void AddResources(int value) {
		if (value >= 0) {
			resources += value;
		}
	}

	public bool SubstractResources(int value) {
		if (resources - value >= 0) {
			resources -= value;
			return true;
		} else {
			return false;
		}
	}

	// Use this for initialization
	void Start () {
		hp = startHP;
		resources = startResources;
		dead = false;
	}

	public void TakeHit() {
		--hp;

		if (hp <= 0) {
			dead = true;
		}
	}
}
