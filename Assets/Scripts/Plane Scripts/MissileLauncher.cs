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
    [SerializeField] private int _damage;
    private void Start()
    {
        ApplyWeaponValues();
    }
    private void ApplyWeaponValues()
    {
        MaxAmmoCount = _maxAmmo;
        InfiniteAmmo = _isInfiniteAmmo;
        ShootCooldown = _shootTimer;
        WeaponDamage = _damage;
    }
    public override void Shoot()
    {
        if (!CheckIfWeaponReady())
            return;

        Transform t = GameObject.FindGameObjectWithTag("Player").transform;

        //Shoot missile 
        GameObject m = Instantiate(missilePrefab, shootPoint.position, shootPoint.rotation);
        m.GetComponent<HomingMissile>().SetTarget(t);
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
