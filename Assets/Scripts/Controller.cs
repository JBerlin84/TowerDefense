/// <summary>
/// Controller class for the level
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(HUDController))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(UIController))]
public class Controller : MonoBehaviour {

	// Private components of the code.
	Camera viewCamera;
	PlayerController player;
	MapHandler mapHandler;

	[Header("Different world controllers")]
	public GameObject map;
	public PauseMenu pauseMenu;

	[Header("Different world objects")]
	public Transform entrancePortal;
	public Transform exitPortal;

	[Header("Tower types")]
	public Transform tower;

	void Start () {
		viewCamera = Camera.main;
		player = GetComponent<PlayerController> ();
		mapHandler = map.GetComponent<MapGenerator> ().fetchMapHandler ();
	}

	void Update () {
		if (!player.Dead) {
			CheckInput ();

			if (!pauseMenu.IsDisplaying) {
				// Get the position of the gameboard we are pointing at.
				Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
				Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

				float rayDistance;
				if (groundPlane.Raycast (ray, out rayDistance)) {
					Vector3 point = ray.GetPoint (rayDistance);

					point = PointToTileCoord (point);

					Debug.DrawLine (ray.origin, point, Color.red);

					if (Input.GetMouseButtonUp (0) && mapHandler.ValidBuildPosition(point)) {

						if (mapHandler.TakePosition (point)) {
							if (mapHandler.checkMapConnectivity ()) {
								Transform newTower = Instantiate (tower) as Transform;
								newTower.transform.position = point;
							} else {
								mapHandler.ReleasePosition (point);
							}
						} else {
							print("Some problem occured, i passed as valid position, but could not change point to taken: " + point.ToString());
						}
					}
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
	/// <summary>
	/// Tell player that he is hit.
	/// </summary>
	void PlayerTakeHit() {
		player.TakeHit ();
	}

	/// <summary>
	/// Tell player that he killed an enemy, and grant him resources.
	/// </summary>
	/// <param name="value">How much resources he has earned.</param>
	void PlayerAddResources(int value) {
		player.AddResources (value);
	}
}
