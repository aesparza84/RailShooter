using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncher : Weapon
{
    [Header("Projectile Prefab")]
    [SerializeField] private GameObject missilePrefab;

    [Header("Shoot Point")]
    [SerializeField] private Transform shootPoint;

    [Header("Weapon Parameters")]
    [SerializeField] private int _maxAmmo;
    [SerializeField] private bool _isInfiniteAmmo;
    [SerializeField] private float _shootTimer;

    [Header("Projectile Decorator")]
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private int _minHomingDistance;

    //Target to send missile to
    private Transform TargetTransform;

    private void Start()
    {
        ApplyWeaponValues();
    }
    protected override void ApplyWeaponValues()
    {
        MaxAmmoCount = _maxAmmo;
        InfiniteAmmo = _isInfiniteAmmo;
        ShootCooldown = _shootTimer;
        WeaponDamage = _damage;
    }
    private void MissileDecorator(IHomingProjectile missile)
    {
        missile.SetDamage(_damage);
        missile.SetSpeed(_speed);
        missile.SetMinHomingDistance(_minHomingDistance);
    }

    public void UpdateTarget(Transform t)
    {
        TargetTransform = t;
    }
    public override void Shoot()
    {
        if (!CheckIfWeaponReady())
            return;



        //Shoot missile 
        GameObject m = Instantiate(missilePrefab, shootPoint.position, shootPoint.rotation);
        HomingMissile missile = m.GetComponent<HomingMissile>();

        missile.SetTarget(TargetTransform);
        MissileDecorator(missile);
    }

    public override void CooldownWeapon()
    {
        if (currentShootTime < ShootCooldown)
        {
            currentShootTime += Time.deltaTime;
        }
    }

    private void Update()
    {
        CooldownWeapon();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot();
        }
    }
}
