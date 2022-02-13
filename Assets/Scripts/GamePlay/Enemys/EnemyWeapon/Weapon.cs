using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Weapon
{
	private float timeBetweenEnemySpawn;
	private Bullet bulletPrefab;
	private Transform spawnPoint;
	private float spawnTimer;
	private bool canSpawn;
	private float speed;

	public void Init(float delay,Bullet bullet,Transform point)
    {
		timeBetweenEnemySpawn = delay;
		bulletPrefab = bullet;
		spawnPoint = point;
		canSpawn = true;
    }
	public void Init(float delay,Bullet bullet,Transform point, float bulletSpeed)
	{
		timeBetweenEnemySpawn = delay;
		bulletPrefab = bullet;
		spawnPoint = point;
		canSpawn = true;
		speed = bulletSpeed;

	}
	public void UpdateTimeShoot()
	{
		if (canSpawn)
		{
			spawnTimer += Time.deltaTime;

			if (spawnTimer >= timeBetweenEnemySpawn)
			{
				spawnTimer = 0.0f;
				Shoot();
			}
		}

	}
	private void Shoot()
	{
		Bullet bullet = GameObject.Instantiate(bulletPrefab,spawnPoint.position,spawnPoint.rotation);
		bullet.Init();
		Vector2 dir = spawnPoint.position.normalized * (25 * UnityEngine.Random.Range(1.0f, 1.1f)*-1);
		bullet.GetComponent<Rigidbody2D>().velocity = dir*speed;
		AudioManager.am.PlayShoot();
	}

}

