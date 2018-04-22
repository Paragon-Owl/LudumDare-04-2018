using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{

	public List<Vector2> SpawnPoints;
	public GameObject EnemyPrefab;
	public Tilemap originalMap;
	private List<EnemyMovement> enemiesMovement;
	private double timeLapse;
	private double startTime;
	private bool on;
	private int indexSaved;
	private const int MaxAstarRequest = 20;

	private void Start()
	{
		enemiesMovement = new List<EnemyMovement>();
		Astar.matrice = new Matrice(Vector2.zero, originalMap);
	}
	private void FixedUpdate()
	{
		if (on)
		{
			if (Time.time - startTime > timeLapse)
			{
				spawnEnemy();
				startTime = Time.time;
			}
		}

		moveAliveEnemies();
	}

	private void moveAliveEnemies()
	{
		int i;
		for (i = 0; i < MaxAstarRequest && (i+indexSaved) < enemiesMovement.Count; i++)
		{
			enemiesMovement[indexSaved + i].RequestPath();
		}

		indexSaved += i;
		if (indexSaved == enemiesMovement.Count)
		{
			indexSaved = 0;
		}
	}

	private void spawnEnemy()
	{
		int index = Random.Range(0, SpawnPoints.Count);
		GameObject newEnemy = Instantiate(EnemyPrefab,SpawnPoints[index],Quaternion.identity);
		enemiesMovement.Add(newEnemy.GetComponent<EnemyMovement>());
	}

	public void startSpawner(double timeForSpawn)
	{
		if (!on)
		{
			Debug.Log("Je lance le Spawner");
			timeLapse = timeForSpawn;
			startTime = Time.time;
			on = true;	
		}
	}

	public void stopSpawner()
	{
		if (on)
		{
			Debug.Log("Je stop le spawner");
			on = false;	
		}
	}
}
