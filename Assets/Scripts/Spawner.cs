using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Transform))]
public class Spawner : MonoBehaviour {

	public Wave[] waves;
	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	float nextSpawnTime;

	Vector3 spawnPosition;

	void Start() {
		spawnPosition = GetComponent<Transform>().position;
		NextWave ();
	}

	void Update() {
		if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

			Enemy spawnedEnemy = Instantiate (currentWave.enemy, spawnPosition, Quaternion.identity) as Enemy;
		}
	}

	/// <summary>
	/// Sets all the params for the next wave.
	/// </summary>
	void NextWave() {
		currentWaveNumber++;
		currentWave = waves[currentWaveNumber - 1];

		enemiesRemainingToSpawn = currentWave.enemyCount;
	}

	/// <summary>
	/// Class for a wave.
	/// </summary>
	[System.Serializable]
	public class Wave {
		public int enemyCount;
		public float timeBetweenSpawns;
		public Enemy enemy;
	}
}
