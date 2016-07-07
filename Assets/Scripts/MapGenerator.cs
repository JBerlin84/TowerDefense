/// <summary>
/// Map generator.
/// Attach this to an empty game object.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {

	public Transform tilePrefab;
	public Transform wallPrefab;
	public Vector2 mapSize = new Vector2(10, 10);
	public string holderName = "Generated Map";

	public Coord positionEntrance;
	public Coord positionExit;

	[Range(0,1)]
	public float outline;

	void Start() {
		GenerateMap ();
	}

	/// <summary>
	/// Map coordinate.
	/// </summary>
	[System.Serializable]
	public struct Coord	{
		public int x;
		public int y;

		public Coord(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}

	/// <summary>
	/// The four different possible rotations.
	/// </summary>
	enum Rotation {
		Up, Down, Left, Right
	}

	/// <summary>
	/// Generates the map.
	/// </summary>
	public void GenerateMap() {
		// If there exists a map, destroy it.
		if (transform.FindChild (holderName)) {
			DestroyImmediate (transform.FindChild (holderName).gameObject);
		}

		// Collect all the generated objects for the just for cleanliness. Also helps with editor script
		Transform mapHolder = new GameObject (holderName).transform;
		mapHolder.parent = transform;

		// Create the tilemap.
		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++) {
				Vector3 tilePosition = CoordinateToPosition (x, y);
				Transform newTile = Instantiate (tilePrefab, tilePosition, Quaternion.Euler (Vector3.right * 90)) as Transform;
				newTile.localScale = Vector3.one * (1 - outline);

				newTile.parent = mapHolder;
			}
		}

		// Create entrance and exit portals
		//TODO: Rotation should not be hard coded here.
		//TODO: The portals should also be created here.
		CreatePortal(positionEntrance, Rotation.Down, mapHolder);
		CreatePortal(positionExit, Rotation.Up, mapHolder);

		//CreateFrame (mapHolder);

		// Create the frame
		for (int x = 0; x < mapSize.x; x++) {
			for (int y = 0; y < mapSize.y; y++) {
				if ((x < 1 || x >= mapSize.x - 1) || (y < 1 || y >= mapSize.y - 1)) {
					if (x != (int)positionEntrance.x && y != (int)positionEntrance.y || x != (int)positionExit.x && y != (int)positionExit.y) {
						Vector3 pos = CoordinateToPosition (x, y);
						Transform framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
						framePiece.parent = mapHolder;
					}
				}
			}
		}
	}

	/// <summary>
	/// Creates the frame for the game board.
	/// </summary>
	/// <param name="mapHolder">Map holder.</param>
	/*
	private void CreateFrame(Transform mapHolder) {
		Vector3 pos = new Vector3 (-Mathf.Floor(mapSize.x / 2), 0);
		Transform framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
		framePiece.localScale = new Vector3 (1, 1, mapSize.y);
		framePiece.parent = mapHolder;

		// Right wall
		pos = new Vector3 (Mathf.Floor(mapSize.x / 2), 0);
		framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
		framePiece.localScale = new Vector3 (1, 1, mapSize.y);
		framePiece.parent = mapHolder;

		// Left wall
		pos = new Vector3 (0, 0, -Mathf.Floor(mapSize.y / 2));
		framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
		framePiece.localScale = new Vector3 (mapSize.x, 1, 1);
		framePiece.parent = mapHolder;

		// Right wall
		pos = new Vector3 (0, 0, Mathf.Floor(mapSize.y / 2));
		framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
		framePiece.localScale = new Vector3 (mapSize.x, 1, 1);
		framePiece.parent = mapHolder;
	}*/

	private void CreatePortal(Coord position, Rotation rotation, Transform mapHolder) {
		List<Coord> entrance = generatePortalCoords (position.x, position.y, rotation);
		for (int i = 0; i < entrance.Count; i++) {
			Vector3 pos = CoordinateToPosition (entrance[i].x, entrance[i].y);
			Transform framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
			framePiece.parent = mapHolder;
		}

	}

	/// <summary>
	/// Generates a portal in given position and direction
	/// </summary>
	/// <returns>List of coordinates for the portal.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="r">The rotation the opening should be heading.</param>
	private List<Coord> generatePortalCoords(int x, int y, Rotation r) {
		int dx = 0;
		int dy = 0;

		switch (r) {
		case Rotation.Up:
			dy -= 1;
			break;
		case Rotation.Down:
			dy += 1;
			break;
		case Rotation.Left:
			dx += 1;
			break;
		case Rotation.Right:
			dx -= 1;
			break;
		}
			
		List<Coord> coords = new List<Coord> ();
		if (r == Rotation.Up || r == Rotation.Down) {
			coords.Add (new Coord (x, y + dy));
			coords.Add (new Coord (x + 1, y + dy));
			coords.Add (new Coord (x - 1, y + dy));
		} else {
			coords.Add (new Coord (x + dx, y));
			coords.Add (new Coord (x + dx, y + 1));
			coords.Add (new Coord (x + dx, y - 1));
		}

		return coords;
	}

	/// <summary>
	/// Translates an x,y coordinate to a Vector3 position.
	/// </summary>
	/// <returns>Vector3 representing the x,y coordinate.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	Vector3 CoordinateToPosition(int x, int y) {
		return new Vector3 (-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
	}
}
