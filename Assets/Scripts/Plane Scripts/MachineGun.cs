using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    [SerializeField] private int _maxAmmo;
    [SerializeField] private bool _isInfiniteAmmo;
    [SerializeField] private float _shootTimer;
    [SerializeField] private int _damage;

    [Header("Shoot Point")]
    [SerializeField] private Transform _shootPointTransform;

    [SerializeField] private float GunRange = 5;
    
    private Ray shootray;

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
    
    private void Update()
    {
        CooldownWeapon();
    }

    private void CooldownWeapon()
    {
        if (currentShootTime < ShootCooldown)
        {
            currentShootTime += Time.deltaTime;
        }
    }

    public override void Shoot()
    {
        if (!CheckIfWeaponReady())
            return;

        currentShootTime = 0.0f;
        shootray = new Ray(_shootPointTransform.position, _shootPointTransform.forward);
        Debug.DrawRay(_shootPointTransform.position, _shootPointTransform.forward * GunRange, Color.green, 1.5f);

        if (Physics.Raycast(shootray, out RaycastHit hit, GunRange))
        {
            //Call an IDamageable.Damage() interface
            if (hit.transform.TryGetComponent<IDamageable>(out IDamageable dmg))
            {
                dmg.Damage(WeaponDamage);
            }

        }
    }
}
