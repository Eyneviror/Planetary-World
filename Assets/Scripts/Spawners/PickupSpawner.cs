using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
	//Prefabs
	[SerializeField] private Pickup speedFirePrefab;
	[SerializeField] private Pickup planetShieldPrefab;
	[SerializeField] private Pickup turretPrefab;

	[SerializeField] private float pickupMinSpawnTime;
	[SerializeField] private float pickupMaxSpawnTime;


	private float pickupSpawnTime;
	private float timer;
	private bool CanSpawn;

	private PickupHandlerEvents pickupHandler;

	public void Init()
	{ 
		pickupSpawnTime = Random.Range(pickupMinSpawnTime, pickupMaxSpawnTime);
	}

	void Update ()
	{
		timer += Time.deltaTime;

		if(timer >= pickupSpawnTime && CanSpawn)
		{
			SpawnPickup();
			timer = 0.0f;
			pickupSpawnTime = Random.Range(pickupMinSpawnTime, pickupMaxSpawnTime);
		}
	}

	//Spawns a pickup in the game.
	void SpawnPickup ()
	{
		GameObject pickupObjcet = Instantiate(GetRandomPickup(), GetRandomPositon(), Quaternion.identity);
		var pickup = pickupObjcet.GetComponent<Pickup>();
	}

	//Returns a random positon behind the player.
	Vector3 GetRandomPositon ()
	{
		Vector3 dir = Rocket.r.rocketSprite.transform.position.normalized;
		return -dir * Random.Range(8.0f, 15.0f);
	}

	//Returns a random pickup prefab.
	GameObject GetRandomPickup ()
	{
		float ranNum = Random.Range(1, 4);

        if (ranNum == 1)
            return speedFirePrefab.gameObject;
        else if (ranNum == 2)
            return planetShieldPrefab.gameObject;
        else
            return turretPrefab.gameObject;
	}


    private void OnEnable()
    {
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
    }
    private void OnDisable()
    {
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
	}
	private void OnStartGameHandler(PlanetaryWorld.Events.OnStartGame ev)
    {
		CanSpawn = ev.ObjectsCanMove;
    }
	private void OnEndGameHandler(PlanetaryWorld.Events.OnEndGame ev)
	{
		CanSpawn = ev.ObjectsCanMove;
	}
}