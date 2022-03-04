using  UnityEngine;
using  System;
    public class Destroyer :Enemy,ICanTakeDamage
    {
        
    [SerializeField] private AnimationCurve movementAnimation;
    [SerializeField] private float force;
    [SerializeField] private float angleSpeed;

    [Header("Weapon Stats")]
    [SerializeField] private EnemyTurret[] turrets;

    private float Radius;
    private float currentRadius;


    public override void Init()
    {
        base.Init();
        for (int i = 0 ; i<turrets.Length;i++)
        {
           turrets[i].Init();
        }
        force = UnityEngine.Random.Range(15, 35);
        angleSpeed = UnityEngine.Random.Range(-angleSpeed - 10, angleSpeed + 10);
    }

    public override void Die()
    {
        GameObject pe = Instantiate(deathParticleEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(pe, 2.0f);
        AudioManager.am.PlayEnemyDeath();
        //TODO: Надо убрать эту хуиту, а то она почти везде. Вынести в отдельный обработчик. 
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
            else
            {
                
            }
            LookAtPlanet();
            //weapon.UpdateTimeShoot();
        }
    }
    }
