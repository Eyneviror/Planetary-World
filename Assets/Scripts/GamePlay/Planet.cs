using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour , ICanTakeDamage 
{

	public event Action<int> OnHPChanged;

	public int Health;
	[SerializeField] private ParticleSystem deathParticleEffect;
	[SerializeField] private PlanetShield shield;

	private SpriteRenderer sprite;
	


    private void Start()
    {
		sprite = GetComponent<SpriteRenderer>();
    }


    private void OnEnable()
    {
        
    }
	public void TakeDamage(Type typeObject)
	{
		int dmg = DamageType.Get(typeObject);

		if (Health - dmg <= 0)
		{

			GlobalEvents.Rise(new PlanetaryWorld.Events.OnDefeatObjectNotHeath());
			GameObject pe = Instantiate(deathParticleEffect.gameObject, transform.position, Quaternion.identity);
			Destroy(pe, 11);
		}
		else
		{
			GlobalEvents.Rise(new PlanetaryWorld.Events.OnTakedDamage());
			StartCoroutine(VisibaleEffects.StunRoutine(sprite));
			Health -= dmg;
			OnHPChanged?.Invoke(Health);
			AudioManager.am.PlayEnemyHit();
		}
	}
	public bool HasShield()
    {
		return shield.isActiveAndEnabled;
    }

	public void CreateShield()
    {
		StartCoroutine(ShieldRoutine());
    }
	private IEnumerator ShieldRoutine()
	{
		shield.gameObject.SetActive(true);
		yield return new WaitForSeconds(8.0f);
		shield.gameObject.SetActive(false);

	}
}
