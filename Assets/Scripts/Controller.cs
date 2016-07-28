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
	UIController uiController;

	[Header("Different world controllers")]
	public GameObject map;
	public PauseMenu pauseMenu;

	[Header("Different world objects")]
	public Transform entrancePortal;
	public Transform exitPortal;

	//[Header("Tower types")]
	//public Transform tower;

	void Start () {
		viewCamera = Camera.main;
		player = GetComponent<PlayerController> ();
		mapHandler = map.GetComponent<MapGenerator> ().fetchMapHandler ();
		uiController = GetComponent<UIController> ();
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

					// Check if mousebutton is pressed, that there is a tower chosen to be built and that the position to build on is not taken.
					if (Input.GetMouseButtonUp (0) && uiController.IsATowerChosen () && mapHandler.ValidBuildPosition(point)) {
						Tower tower = uiController.GetSelectedTower ();
						if (tower.price <= player.Resources) {								// Check wether we can afford to build the tower.
							if (mapHandler.TakePosition (point)) {							// Set the position to taken so we can check connectivity of map.
								if (mapHandler.checkMapConnectivity ()) {					// Check the connectivity.
									Tower newTower = Instantiate (tower) as Tower;			// Instantiate new tower on given position
									newTower.transform.position = point;
									player.SubstractResources (newTower.price);				// Remove resources from player.
								} else {													// If the map connectivity test fails.
									mapHandler.ReleasePosition (point);						// Remove the position again.
								}
							} else {
								print("Some problem occured, i passed as valid position, but could not change point to taken: " + point.ToString());
							}
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
			if (uiController.IsATowerChosen ()) {
				uiController.ClearSelectedTower ();
			} else if (pauseMenu.IsDisplaying) {
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
