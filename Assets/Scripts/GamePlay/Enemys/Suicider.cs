using System;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Suicider : Enemy, ICanTakeDamage
{
    public override void Die()
    {
        GameObject pe = Instantiate(deathParticleEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(pe, 2.0f);
        AudioManager.am.PlayEnemyDeath();
        GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
        GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
        Destroy(gameObject);
    }

    public override void Init()
    {
        base.Init();
    }

    private void Update()
    {
        //Move to planet.
        if (!stunned && canMove)
            transform.position = Vector3.MoveTowards(
                transform.position,
                Vector3.zero,
                moveSpeed * Time.deltaTime);
        LookAtPlanet();
    }

    public void TakeDamage(Type other)
    {

        int currentDamage = DamageType.Get(other);
        if (health - currentDamage <= 0)
            Die();
        else
        {
            health -= currentDamage;
            StartCoroutine(stunnedRoutine());
            AudioManager.am.PlayEnemyHit();
        }

        StartCoroutine(VisibaleEffects.StunRoutine(sr));

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent<Planet>(out Planet planet))
        {
            if (!planet.HasShield())
            {
                planet.TakeDamage(typeof(Enemy));
                Die();
            }
        }
    }
}
