using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour 
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private Transform turretSprite;
	[SerializeField] private float attackRate;
	
	[SerializeField] private float bulletSpeed;
	[SerializeField] private float bulletSpread = 1.0f;
	[SerializeField] private float timeAlive;

	private float attackTimer;
	private float timeAliveTimer;

	//Prefabs
	public GameObject bulletPrefab;

	private bool gameActive;
	void OnEnable ()
	{
		timeAliveTimer = 0.0f;
		attackTimer = 0.0f;
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Subscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
	}
    private void OnDisable()
    {
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
		GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
	}
    void Update ()
	{
		//If the game is running, then rotate around the planet and shoot.
		if(gameActive)
		{
			RotateTurret();

			if(attackTimer >= attackRate)
			{
				attackTimer = 0.0f;
				Shoot();
			}
		}

		//Once the turret has been active for time alive, disable it.
		if(timeAliveTimer >= timeAlive)
		{
			gameObject.SetActive(false);
		}

		attackTimer += Time.deltaTime;
		timeAliveTimer += Time.deltaTime;
	}

	public void Init(bool movement)
    {
		gameActive = movement;
    }
	//Rotates the turret around the planet.
	void RotateTurret ()
	{
		transform.eulerAngles += new Vector3(0, 0, -moveSpeed * Time.deltaTime);
	}

	//Shoots a bullet forward from the turret.
	void Shoot ()
	{
		GameObject bullet = Instantiate(bulletPrefab, turretSprite.transform.position, transform.rotation);

		Vector2 dir = turretSprite.transform.position.normalized * (bulletSpeed * Random.Range(1.0f, 1.1f));
		Vector3 offset = bullet.transform.right * Random.Range(-bulletSpread, bulletSpread);
		dir.x += offset.x;
		dir.y += offset.y;

		bullet.GetComponent<Rigidbody2D>().velocity = dir;
	}

	private void OnStartGameHandler(PlanetaryWorld.Events.OnStartGame ev)
    {
		gameActive = ev.ObjectsCanMove;
    }
	private void OnEndGameHandler(PlanetaryWorld.Events.OnEndGame ev)
	{
		gameActive = ev.ObjectsCanMove;
	}


}
