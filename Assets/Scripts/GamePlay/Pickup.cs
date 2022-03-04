using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pickup : MonoBehaviour, ICanTakeDamage
{
	[SerializeField] private int health;
	[SerializeField] private float timeAlive;
	[SerializeField] private SpriteRenderer sr;
	[SerializeField] private PickupType type;

	public event Action<PickupType> OnPickupAplayed;

	void Update ()
	{
		timeAlive -= Time.deltaTime;

		if(timeAlive <= 0.0f)
			StartCoroutine(Shrink());
	}

	//Called when a bullet hits the pickup.

	//Called when the health reaches less than or equal to 0.
	public void ApplyPickup()
	{
		GlobalEvents.Rise(new PlanetaryWorld.Events.OnPickupAplayed(type));

		AudioManager.am.PlayGetPickup();
		Destroy(gameObject);
	}

	//Slowly shrinks the pickup before it gets destroyed after it's lived out it's max time alive.
	IEnumerator Shrink ()
	{
		//Scale it down overtime, then destroy it.
		while(transform.localScale.x > 0.0f)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 0.5f);
			yield return null;
		}

		Destroy(gameObject);
	}

    public void TakeDamage(Type t)
    {
		int dmg = DamageType.Get(t);
		if (health - dmg <= 0)
		{
			ApplyPickup();
		}
		else
		{
			health -= dmg;
			transform.localScale += new Vector3(0.2f, 0.2f, 0.0f);
		}

		GlobalEvents.Rise(new PlanetaryWorld.Events.OnTakedDamage());
		StartCoroutine(VisibaleEffects.StunRoutine(sr));
	}
}