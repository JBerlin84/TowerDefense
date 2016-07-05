using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	Camera viewCamera;

	public Transform enemy;

	void Start () {
		viewCamera = Camera.main;
	}

	void Update () {
		// Get the position of the gameboard we are pointing at.
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

		float rayDistance;
		if (groundPlane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);

			point = PointToTileCoord (point);

			Debug.DrawLine (ray.origin, point, Color.red);
			print (point);

			if (Input.GetMouseButtonUp (0)) {
				Transform newTower = Instantiate (enemy) as Transform;
				newTower.transform.position = point;
				Tree tree = newTower.GetComponent<Tree> ();

			}
		}
	}

	Vector3 PointToTileCoord(Vector3 point) {
		point.x = Mathf.Round (point.x);
		point.y = Mathf.Round (point.y);
		point.z = Mathf.Round (point.z);

		return point;
	}
}
