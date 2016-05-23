using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public EnemyBase[] enemiesToSpawn;
	public float timeBetweenSpawns = 3000f;

	public bool stopSpawning = false;

	float nextTime;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (enemiesToSpawn != null && !stopSpawning) {
			if (Time.time > nextTime) {

				int random = Random.Range (0, enemiesToSpawn.Length);

				nextTime = Time.time + timeBetweenSpawns / 1000;

				EnemyBase enemy = Instantiate (enemiesToSpawn [random], transform.position, transform.rotation) as EnemyBase;
				enemy.transform.parent = transform;

			}
		}

	}
}
