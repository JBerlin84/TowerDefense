/// <summary>
/// The code handling the entrance portal to the world.
/// </summary>

using UnityEngine;
using System.Collections;

public class EntrancePortal : MonoBehaviour {

	public Wave[] waves;
	Wave currentWave;
	int currentWaveNumber;

	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	Vector3 spawnPosition;

	GameObject worldController;

	void Start() {
		spawnPosition = GetComponent<Transform>().position;
		worldController = GameObject.FindGameObjectWithTag ("GameController") as GameObject;
		NextWave ();
	}

	void Update() {
		if (enemiesRemainingToSpawn > 0 && Time.time > nextSpawnTime) {
			enemiesRemainingToSpawn--;
			nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

			Enemy spawnedEnemy = Instantiate (currentWave.enemy, spawnPosition, Quaternion.identity) as Enemy;
			spawnedEnemy.OnKilled += OnEnemyKilled;
			spawnedEnemy.OnPortalled += OnEnemyPortalled;
		}
	}

	void OnEnemyKilled(int value) {
		PlayerController playerController = worldController.GetComponent<PlayerController> ();
		playerController.AddResources (value);

		enemyRemoved ();
	}

	void OnEnemyPortalled() {
		PlayerController playerController = worldController.GetComponent<PlayerController> ();
		playerController.TakeHit ();

		enemyRemoved ();
	}

	/// <summary>
	/// This should always happens when an enemy is removed from the game board,
	/// no matter if it is teleported or killed.
	/// </summary>
	void enemyRemoved() {
		enemiesRemainingAlive--;

		if (enemiesRemainingAlive <= 0) {
			NextWave ();
		}
	}

	/// <summary>
	/// Sets all the params for the next wave.
	/// </summary>
	void NextWave() {
		currentWaveNumber++;
		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves[currentWaveNumber - 1];

			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;
		}
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
