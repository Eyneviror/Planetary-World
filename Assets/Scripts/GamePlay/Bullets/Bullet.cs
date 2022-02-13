using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private  int damage; //Most of the time 1.
	[SerializeField] private  ParticleSystem hitParticleEffect;

	private int senderInstanceId; 

	public virtual void Init ()
	{
		Destroy(gameObject, 4.0f);
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.GetInstanceID()!=senderInstanceId)
		{
			if (col.gameObject.TryGetComponent<ICanTakeDamage>(out ICanTakeDamage dammagable))
			{
				dammagable.TakeDamage(typeof(Bullet));
				SpawnParticleEffect();
				Destroy(gameObject);

			}
			GlobalEvents.Rise(new PlanetaryWorld.Events.OnBulletHit());
		}
		CollisionWith((col));

	}

	//Called when the bullet hits an object. Spawns the particle effect.
	private void SpawnParticleEffect ()
	{
		GameObject pe = Instantiate(hitParticleEffect.gameObject, transform.position, Quaternion.identity);
		//pe.transform.LookAt(Planet.p.transform);
		Destroy(pe, 2.0f);
	}

	protected virtual void CollisionWith(Collider2D col)
	{
		
	}
	
}
