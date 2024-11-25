using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    /// <summary>
    /// Ammunition count, if not infinite 
    /// </summary>
    protected int AmmoCount;

    /// <summary>
    /// Ammunition cap, if not infinite
    /// </summary>
    protected int MaxAmmoCount;

    /// <summary>
    /// Inifinite ammo flag
    /// </summary>
    protected bool InfiniteAmmo;

    /// <summary>
    /// The amount of damage this weapon does
    /// </summary>
    protected int WeaponDamage;

    /// <summary>
    /// Cooldown before each shot
    /// </summary>
    protected float ShootCooldown;
    protected float currentShootTime;
    public virtual void Shoot() { }

    /// <summary>
    /// Checks conditions to see if weapon can fire
    /// </summary>
    /// <returns></returns>
    protected bool CheckIfWeaponReady()
    {
        //Check if we have ammo
        if (!InfiniteAmmo)
        {
            if (AmmoCount == 0)
                return false;
        }

        //Check if timer is ready
        if (currentShootTime < ShootCooldown)
            return false;

        return true;
    } 
}
