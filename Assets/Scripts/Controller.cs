using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	Camera viewCamera;

	void Start () {
		viewCamera = Camera.main;
	}

	void Update () {
		// Get position of gameboard we are pointing at.
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.zero);

		float rayDistance;
		if (groundPlane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);
			Debug.DrawLine (ray.origin, point, Color.red);
		}
	}
}
