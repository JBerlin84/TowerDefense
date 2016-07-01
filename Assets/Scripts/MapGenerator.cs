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

	public Vector2 positionEntrance;
	public Vector2 positionExit;

	[Range(0,1)]
	public float outline;

	//List<Coord> tileCoordinates;

	void Start() {
		GenerateMap ();
	}

	/// <summary>
	/// Map coordinate.
	/// </summary>
	public struct Coord	{
		public int x;
		public int y;

		public Coord(int x, int y) {
			this.x = x;
			this.y = y;
		}
	}

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
		List<Coord> entrance = generatePortal ((int)positionEntrance.x, (int)positionEntrance.y, Rotation.Down);
		for (int i = 0; i < entrance.Count; i++) {
			Vector3 pos = CoordinateToPosition (entrance[i].x, entrance[i].y);
			Transform framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
			framePiece.parent = mapHolder;
		}

		List<Coord>exit = generatePortal ((int)positionExit.x, (int)positionExit.y, Rotation.Up);
		for (int i = 0; i < exit.Count; i++) {
			Vector3 pos = CoordinateToPosition (exit[i].x, exit[i].y);
			Transform framePiece = Instantiate (wallPrefab, pos + Vector3.up * 0.5f, Quaternion.identity) as Transform;
			framePiece.parent = mapHolder;
		}

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
	/// Generates a portal in given position and direction
	/// </summary>
	/// <returns>List of coordinates for the portal.</returns>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="r">The rotation the opening should be heading.</param>
	private List<Coord> generatePortal(int x, int y, Rotation r) {
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
			
		List<Coord> entrance = new List<Coord> ();
		if (r == Rotation.Up || r == Rotation.Down) {
			entrance.Add (new Coord (x, y + dy));
			entrance.Add (new Coord (x + 1, y + dy));
			entrance.Add (new Coord (x - 1, y + dy));
			/*if (r == Rotation.Up) {
				entrance.Add (new Coord (x + 1, y + dy + 1));
				entrance.Add (new Coord (x - 1, y + dy + 1));
			} else {
				entrance.Add (new Coord (x + 1, y + dy - 1));
				entrance.Add (new Coord (x - 1, y + dy - 1));
			}*/
		} else {
			entrance.Add (new Coord (x + dx, y));
			entrance.Add (new Coord (x + dx, y + 1));
			entrance.Add (new Coord (x + dx, y - 1));
			/*if (r == Rotation.Left) {
				entrance.Add (new Coord (x + dx - 1, y + 1));
				entrance.Add (new Coord (x + dx - 1, y - 1));
			} else {
				entrance.Add (new Coord (x + dx + 1, y + 1));
				entrance.Add (new Coord (x + dx + 1, y - 1));
			}*/
		}

		return entrance;
	}

	Vector3 CoordinateToPosition(int x, int y) {
		return new Vector3 (-mapSize.x / 2 + 0.5f + x, 0, -mapSize.y / 2 + 0.5f + y);
	}
}
