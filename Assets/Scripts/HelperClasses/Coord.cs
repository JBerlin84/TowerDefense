using UnityEngine;
using System.Collections;

[System.Serializable]
public class Coord {
	public int x;
	public int y;

	public Coord(int x, int y) {
		this.x = x;
		this.y = y;
	}

	public static bool operator == (Coord c1, Coord c2) {
		return c1.x == c2.x && c1.y == c2.y;
	}

	public static bool operator != (Coord c1, Coord c2) {
		return !(c1 == c2);
	}

	public override bool Equals(object obj) {
		return x == ((Coord)obj).x && y == ((Coord)obj).y;
	}

	public override int GetHashCode() {
		return x + y;
	}
}
