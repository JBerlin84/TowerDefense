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

	}
}
