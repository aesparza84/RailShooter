using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    [Header("Weapon Parameters")]
    [SerializeField] private int _maxAmmo;
    [SerializeField] private bool _isInfiniteAmmo;
    [SerializeField] private float _shootTimer;
    [SerializeField] private int _damage;

    [Header("Shoot Point")]
    [SerializeField] private Transform _shootPointTransform;

    [Header("Crosshair")]
    [SerializeField] private GameObject crosshairObject; 
    private ICrosshairReader _crosshairReader;

    [Header("Weapon Range")]
    [SerializeField] private float GunRange = 5;

    private Vector3 dirToRayEnd;
    private Ray shootray;

    private void Start()
    {
        _crosshairReader = crosshairObject.GetComponent<CrosshairTargetFinder>();

        ApplyWeaponValues();
    }
    protected override void ApplyWeaponValues()
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

    public override void CooldownWeapon()
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

        //Get ray end point from crosshair
        Vector3 endPoint = _crosshairReader.GetRaycastPoint();
        dirToRayEnd = (endPoint - _shootPointTransform.position);

        shootray = new Ray(_shootPointTransform.position, dirToRayEnd);
        Debug.DrawRay(_shootPointTransform.position, dirToRayEnd.normalized * GunRange, Color.green, 1.5f);

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
