using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private AirForceController airForceController;
    [SerializeField] private Transform projectileSpawnTransform;
    public GameObject bullet;
    private Vector2 aimDirection = Vector2.right; // 초기값 1

    private void Awake()
    {
        airForceController = GetComponent<AirForceController>();
    }

    private void Start()
    {
        airForceController.OnAttackEvent += Shoot;
        airForceController.OnLookEvent += Aim;
    }

    private void Aim(Vector2 direction)
    {
        aimDirection = direction;
    }

    private void Shoot(AttackSO attackSO)
    {
        // 발사체 옵션
        float projectilesAngleSpace = attackSO.multipleProjectilesAngle;
        int numberOfProjectilesPerShot = attackSO.numberofProjectilesPerShot;

        // 중간부터 펼쳐지는게 아니라 minangle부터 커지면서 쏘는 것으로 설계 
        float minAngle = -(numberOfProjectilesPerShot / 2f) * projectilesAngleSpace + 0.5f * attackSO.multipleProjectilesAngle;

        for (int i = 0; i < numberOfProjectilesPerShot; i++)
        {
            float angle = minAngle + projectilesAngleSpace * i;
            float randomSpread = Random.Range(-attackSO.spread, attackSO.spread);
            angle += randomSpread;
            CreateProjectile(attackSO, angle);
        }
    }

    private void CreateProjectile(AttackSO attackSO, float angle)
    {
        GameObject projectile = Instantiate(bullet);

        projectile.transform.position = projectileSpawnTransform.position;
        ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
        projectileController.InitializeAttack(RotateVector2(aimDirection, angle), attackSO);
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        // 벡터 회전 = Q * V
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
