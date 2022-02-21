using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Weapon
{
	 private protected float timeBetweenEnemySpawn;
	 private protected Bullet bulletPrefab;
	 private protected Transform spawnPoint;
	 private protected float spawnTimer;
	 private protected bool canSpawn;
	 private protected float speed;
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
	public virtual void UpdateTimeShoot()
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
	protected void Shoot()
	{
		Bullet bullet = GameObject.Instantiate(bulletPrefab,spawnPoint.position,spawnPoint.rotation);
		bullet.Init();
		Vector2 dir = spawnPoint.position.normalized * (25 * UnityEngine.Random.Range(1.0f, 1.1f)*-1);
		bullet.GetComponent<Rigidbody2D>().velocity = dir*speed;
		AudioManager.am.PlayShoot();
	}

}

