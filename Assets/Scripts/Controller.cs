/// <summary>
/// Controller class for the level
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {

	Camera viewCamera;
	bool isPaused;

	public Transform enemy;
	public GameObject pauseMenu;

	void Start () {
		viewCamera = Camera.main;
		isPaused = false;
	}

	void Update () {

		CheckInput();

		// Get the position of the gameboard we are pointing at.
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

		float rayDistance;
		if (groundPlane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);

			point = PointToTileCoord (point);

			Debug.DrawLine (ray.origin, point, Color.red);

			if (Input.GetMouseButtonUp (0)) {
				Transform newTower = Instantiate (enemy) as Transform;
				newTower.transform.position = point;
			}
		}
	}

	/// <summary>
	/// Handles key inputs from the user.
	/// </summary>
	void CheckInput() {
		print ("is being called");
		// If escape is pressed, exit
		// TODO: This should probably be a game menu of some sort.
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPaused) {
				Time.timeScale = 1f;
				pauseMenu.SetActive (false);
				isPaused = false;
			} else {
				Time.timeScale = 0f;
				pauseMenu.SetActive (true);
				isPaused = true;
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
}
