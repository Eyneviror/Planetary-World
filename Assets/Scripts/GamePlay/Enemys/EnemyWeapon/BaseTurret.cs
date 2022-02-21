using  UnityEngine;

public class BaseTurret : Weapon
{
    private float targetOfAngle;

    public override void UpdateTimeShoot()
    {
        if (canSpawn && Mathf.Abs(targetOfAngle)<=5)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= timeBetweenEnemySpawn)
            {
                spawnTimer = 0.0f;
                Shoot();
            }
        }
    }

    public void UpdateTargetOfAngle(float angle)
    {
        targetOfAngle = angle;
    }
}
