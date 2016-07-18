﻿/// <summary>
/// Controller class for the level
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HUDController))]
[RequireComponent(typeof(PlayerController))]
public class Controller : MonoBehaviour {

	// Private components of the code.
	Camera viewCamera;
	PlayerController player;

	[Header("Different world controllers")]
	public PauseMenu pauseMenu;

	[Header("Different world objects")]
	public Transform entrancePortal;
	public Transform exitPortal;

	[Header("Tower types")]
	public Transform tower;

	void Start () {
		viewCamera = Camera.main;
		player = GetComponent<PlayerController> ();
	}

	void Update () {
		if (!player.Dead) {
			CheckInput ();

			// Get the position of the gameboard we are pointing at.
			Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
			Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

			float rayDistance;
			if (groundPlane.Raycast (ray, out rayDistance)) {
				Vector3 point = ray.GetPoint (rayDistance);

				point = PointToTileCoord (point);

				Debug.DrawLine (ray.origin, point, Color.red);

				if (Input.GetMouseButtonUp (0)) {
					Transform newTower = Instantiate (tower) as Transform;
					newTower.transform.position = point;
				}
			}
		} else {
			// Display score screen here
			print("Score screen");
		}
	}

	/// <summary>
	/// Handles key inputs from the user.
	/// </summary>
	void CheckInput() {
		// If escape is pressed, exit
		// TODO: This should probably be a game menu of some sort.
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (pauseMenu.IsDisplaying) {
				Time.timeScale = 1f;
				pauseMenu.Close ();
			} else {
				Time.timeScale = 0f;
				pauseMenu.Display ();
			}
		}
	}

	/// <summary>
	/// Convert a mouse point coord to a definitive tile coordinate.
	/// </summary>
	/// <returns>The two tile coordinate.</returns>
	/// <param name="point">Point to convert</param>
	Vector3 PointToTileCoord(Vector3 point) {
		point.x = Mathf.Round (point.x);
		point.y = Mathf.Round (point.y);
		point.z = Mathf.Round (point.z);

		return point;
	}

	void PlayerTakeHit() {
		player.TakeHit ();
	}

	void PlayerAddResources(int value) {
		player.AddResources (value);
	}
}
