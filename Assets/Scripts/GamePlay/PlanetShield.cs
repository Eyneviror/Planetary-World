using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShield : MonoBehaviour 
{
	public SpriteRenderer sr;

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.TryGetComponent<Enemy>(out Enemy enemy))
		{
			enemy.Die();
			StartCoroutine(VisibaleEffects.StunRoutine(sr));
		}
	}
}
