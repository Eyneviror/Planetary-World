using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Rect spawnBoundry; //The edges of which the enemies can spawn on.
	[SerializeField] private float timeBetweenEnemySpawn;
	[SerializeField] private float spawnTimer;
	[SerializeField] private Transform enemyParent;
	private bool CanSpawn;
	private EnemyFactory enemyfactory;

	public void Init(EnemyFactory factory)
	{
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);

		enemyfactory = factory;
	}
	public void UpdateTimeSpawn ()
	{
		if (CanSpawn)
		{
			spawnTimer += Time.deltaTime;

			if (spawnTimer >= timeBetweenEnemySpawn)
			{
				spawnTimer = 0.0f;
				SpawnEnemy();
			}
		}

	}
	private void SpawnEnemy ()
	{
        float spawnDirection = UnityEngine.Random.Range(1, 5); //Left, Up, Right, Down.
        Vector3 spawnPos = Vector3.zero;

        //Get spawn pos based of screen direction.
        if (spawnDirection == 1)
            	spawnPos = new Vector3(spawnBoundry.xMin, UnityEngine.Random.Range(spawnBoundry.yMin, spawnBoundry.yMax), 0);
            else if(spawnDirection == 2)
            	spawnPos = new Vector3(UnityEngine.Random.Range(spawnBoundry.xMin, spawnBoundry.xMax), spawnBoundry.yMax, 0);
            else if(spawnDirection == 3)
            	spawnPos = new Vector3(spawnBoundry.xMax, UnityEngine.Random.Range(spawnBoundry.yMin, spawnBoundry.yMax), 0);
            else
            spawnPos = new Vector3(UnityEngine.Random.Range(spawnBoundry.xMin, spawnBoundry.xMax), spawnBoundry.yMin, 0);

		//Spawn the enemy.
		SpawnData data = enemyfactory.GetRandom();
		if (data.count > 1)
		{
			for (int i = 0; i < data.count; i++)
			{

				Enemy enemy = Instantiate(data.EnemyObject, spawnPos, Quaternion.identity, enemyParent.transform);
				enemy.Init();
				spawnPos.x += i;
			}
		}
		else
		{
			Enemy enemy = Instantiate(data.EnemyObject, spawnPos, Quaternion.identity, enemyParent.transform);
			enemy.Init();
		}
	}
	private void OnStartGameHandler(PlanetaryWorld.Events.OnStartGame ev)
    {
		CanSpawn = ev.ObjectsCanMove;
    }
	private void OnEndGameHandler(PlanetaryWorld.Events.OnEndGame ev)
	{
		CanSpawn = ev.ObjectsCanMove;
	}
	private void OnDisable()
    {
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
	}
}
