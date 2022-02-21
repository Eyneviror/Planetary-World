
using System;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
//TODO Это тоже хуита
    [SerializeField] private float delay;
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform point;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float angleSpeed;
    [SerializeField] private float rotationOffset;

    private BaseTurret weapon = new BaseTurret();
    private float targetOfAngle;

    private void Start()
    {
        weapon.Init(delay, bullet, point, bulletSpeed);
    }

    private void Update()
    {
        //TODO Вынести нахуй в отдельный класс
        Vector2 point = Vector2.zero;
        Vector2 direction = new Vector3(point.x, point.y, 0) - transform.position;
        float angle = Vector3.Angle(Vector3.right, direction.normalized);
        if (direction.y < 0)
        {
            angle *= -1;
        }

        Quaternion to = Quaternion.Euler(0, 0, angle - rotationOffset);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, angleSpeed);
        targetOfAngle = Vector3.Angle(transform.up, direction);

        weapon.UpdateTargetOfAngle(targetOfAngle);
        weapon.UpdateTimeShoot();
    }
}
