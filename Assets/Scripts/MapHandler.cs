using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapHandler {

	int[,] map;
	Coord size;
	Coord entrance;
	Coord exit;

	public Coord Size {
		get { return size; }
	}

	public Coord Entrance {
		get { return entrance; }
	}

	public Coord Exit {
		get { return exit; }
	}

	public int[,] mapMatrix {
		get { return map; }
	}

	public MapHandler(int sizeX, int sizeY, int entranceX, int entranceY, int exitX, int exitY, int[,] mapMatrix) {
		size = new Coord (sizeX, sizeY);
		entrance = new Coord (entranceX, entranceY);
		exit = new Coord (exitX, exitY);
		this.map = mapMatrix;
	}

	public MapHandler(Coord size, Coord entrance, Coord exit, int[,] mapMatrix) {
		this.size = size;
		this.entrance = entrance;
		this.exit = exit;
		this.map = mapMatrix;
	}

	public bool ValidBuildPosition(Vector3 pos) {

		int posX = Mathf.RoundToInt (pos.x);
		int posZ = Mathf.RoundToInt (pos.z);

		// out of bounds.
		if (posX < -size.x / 2 || posX > size.x / 2 || posZ < -size.y / 2 || posZ > size.y / 2) {
			Debug.Log ("Out of bounds");
			return false;
		}

		// trying to build on top of the entrance.
		Coord posEntrance = CoordinateToPosition(entrance.x, entrance.y);
		if (posX == posEntrance.x && posZ == posEntrance.y) {
			Debug.Log ("Spot is the entrance");
			return false;
		}
			
		// Trying to build on top of the exit.
		Coord posExit = CoordinateToPosition(exit.x, exit.y);
		if (posX == posExit.x && posZ == posExit.y) {
			Debug.Log ("Spot is the exit");
			return false;
		}

		// is the spot empty?
		if (map [posX + size.x / 2, posZ + size.y / 2] != 0) {
			Debug.Log ("Spot is taken");
			return false;
		}


		Debug.Log ("Valid position");
		return true;
	}

	Coord CoordinateToPosition(int x, int y) {
		// WARNING!!! THIS EXISTS IN TWO POSITIONS, MapGenerator.cs and MapHandler.cs
		return new Coord (-size.x / 2 + x, -size.y / 2 + y);
	}

	Coord PositionToCoordinate(Vector3 coordinate) {
		return new Coord (Mathf.RoundToInt(coordinate.x + size.x / 2), Mathf.RoundToInt(coordinate.z + size.y / 2));
	}

	public bool TakePosition(Vector3 position) {
		Coord pos = PositionToCoordinate (position);

		if (map [pos.x, pos.y] == 0) {
			map [pos.x, pos.y] = 1;
			return true;
		} else {
			return false;
		}
	}

	public bool ReleasePosition(Vector3 position) {
		Coord pos = PositionToCoordinate (position);

		if (map [pos.x, pos.y] == 1) {
			map [pos.x, pos.y] = 0;
			return true;
		} else {
			return false;
		}
	}

	public bool checkMapConnectivity() {
		// Count the number of tiles that has been built
		int builtTileCount = 0;
		for (int i = 0; i < size.x; i++) {
			for (int j = 0; j < size.y; j++) {
				if (map [i, j] == 1) {
					builtTileCount++;
				}
			}
		}

		// Count the number of free tiles.
		bool[,] mapFlags = new bool[size.x, size.y];
		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (entrance);
		mapFlags [entrance.x, entrance.y] = true;

		int accessibleTileCount = 1;
		//int builtTileCount = 0;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue ();

			for (int x = -1; x <= 1; x++) {
				for (int y = -1; y <= 1; y++) {
					int neighbourX = tile.x + x;
					int neighbourY = tile.y + y;

					if (x == 0 || y == 0) {
						if (neighbourX >= 0 && neighbourX < size.x && neighbourY >= 0 && neighbourY < size.y) {
							if (!mapFlags [neighbourX, neighbourY] && map [neighbourX, neighbourY] == 0) {
								mapFlags [neighbourX, neighbourY] = true;
								queue.Enqueue (new Coord (neighbourX, neighbourY));
								accessibleTileCount++;
							}
							// TODO: Count number of built tiles here in some way.
							//if (!mapFlags[neighbourX, neighbourY] && map [neighbourX, neighbourY] == 1) {
							//	mapFlags [neighbourX, neighbourY] = true;
							//	builtTileCount++;
							//}
						}
					}
				}
			}
		}

		// If the number of free tiles plus the number of built tiles equals the total number of tiles, all the map is accessible.
		//Debug.Log ("size.x * size.y: " + (size.x * size.y) + "\naccessibleTileCount: " + accessibleTileCount + "\nbuiltTileCount: " + builtTileCount + "\naccessibleTileCount + builtTileCount: " + (accessibleTileCount + builtTileCount));
		return (size.x * size.y == accessibleTileCount + builtTileCount);
	}
}
