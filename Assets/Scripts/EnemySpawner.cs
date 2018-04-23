using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{

	public List<Transform> SpawnPoints;
	public GameObject EnemyPrefab;
	public Tilemap notWalkable;
	public Tilemap map;
	private List<EnemyMovement> enemiesMovement;
	private double timeLapse;
	private double startTime;
	private bool on;
	private int indexSaved;
	private const int MaxAstarRequest = 10;

	private void Start()
	{
		enemiesMovement = new List<EnemyMovement>();
		//Astar.matrice = new Matrice(Vector2.zero, notWalkable);
		Astar.matrice = new Matrice(notWalkable,map);
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

		updateAliveEnemies();
		moveAliveEnemies();
	}

	private void updateAliveEnemies()
	{
		foreach (EnemyMovement en in enemiesMovement)
		{
			if (en.IsDead())
			{
				enemiesMovement.Remove(en);
				Destroy(en.gameObject);
			}
		}
	}

	private void moveAliveEnemies()
	{
		int i;
		int nbRequest = 0;
		for (i = 0; nbRequest < MaxAstarRequest && (i+indexSaved) < enemiesMovement.Count; i++)
		{
			if (enemiesMovement[indexSaved + i].needARequest())
			{
				enemiesMovement[indexSaved + i].RequestPath();
				nbRequest++;
			}
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
		GameObject newEnemy = Instantiate(EnemyPrefab,SpawnPoints[index].position,Quaternion.identity);
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
