using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HUDController : MonoBehaviour {

	public Text HP;
	public Text resources;

	PlayerController player;

	void Start() {
		player = GetComponentInParent<PlayerController> ();
	}

	// Update is called once per frame
	void Update () {
		HP.text = "HP: " + player.Hp;
		resources.text = "Resources: " + player.Resources;
	}
}
