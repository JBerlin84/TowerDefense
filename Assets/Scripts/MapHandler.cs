using UnityEngine;
using System.Collections;

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
}
