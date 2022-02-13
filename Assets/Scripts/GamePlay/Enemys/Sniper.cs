using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Sniper: Enemy, ICanTakeDamage
{

    [SerializeField] private AnimationCurve movementAnimation;
    [SerializeField] private float force;
    [SerializeField] private float angleSpeed;

    [Header("Weapon Stats")]
    [SerializeField] private float timeBetweenEnemySpawn;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float bulletSpeed;

    private float Radius;
    private float currentRadius;

    private Weapon weapon = new Weapon();

    public override void Init()
    {
        base.Init();
        weapon.Init(timeBetweenEnemySpawn, bulletPrefab, spawnPoint, bulletSpeed);

        force = UnityEngine.Random.Range(15, 35);
        angleSpeed = UnityEngine.Random.Range(-angleSpeed - 10, angleSpeed + 10);
    }

    public override void Die()
    {
        GameObject pe = Instantiate(deathParticleEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(pe, 2.0f);
        AudioManager.am.PlayEnemyDeath();
        GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnStartGame>(OnStartGameHandler);
        GlobalEvents.Unsubscribe<PlanetaryWorld.Events.OnEndGame>(OnEndGameHandler);
        Destroy(gameObject);
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

    private void Update()
    {
        currentRadius = transform.position.magnitude;
        if(canMove && !stunned)
        {

            Radius = movementAnimation.Evaluate(Time.realtimeSinceStartup/500) * force;
            transform.RotateAround(Vector2.zero, Vector3.forward,angleSpeed * Time.deltaTime);
            if(currentRadius != Radius)
            {
                float direction = Mathf.Clamp(currentRadius - Radius, -1, 1);
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime*direction);
                
            }
            LookAtPlanet();
            weapon.UpdateTimeShoot();
        }
    }


}

