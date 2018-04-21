using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

	public List<Vector2> SpawnPoints;
	public GameObject EnemyPrefab;
	private double timeLapse;
	private double startTime;
	private bool on;

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
	}

	private void spawnEnemy()
	{
		Debug.Log("Je spawn un enemy");
		int index = Random.Range(0, SpawnPoints.Count);
		Instantiate(EnemyPrefab,SpawnPoints[index],Quaternion.identity);
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
