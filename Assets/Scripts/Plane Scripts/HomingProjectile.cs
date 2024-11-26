using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HomingProjectile : MonoBehaviour, IHomingProjectile
{
    /// <summary>
    /// Target to home towards
    /// </summary>
    protected Transform targetTransform;

    /// <summary>
    /// Direction to target
    /// </summary>
    protected Vector3 targetDirection;

    /// <summary>
    /// Tracks target when 'OVER' minimum distance
    /// </summary>
    [Range(0f, 50f)]
    [SerializeField] protected float MinHomingDistance;

    /// <summary>
    /// Damage that the projectile deals
    /// </summary>
    [SerializeField] protected int Damage;

    /// <summary>
    /// Speed of projectile
    /// </summary>
    [SerializeField] protected float Speed;

    public virtual void SetTarget(Transform target) { }
    public virtual bool CanContinueHoming() {  return false; }
    public void SetDamage(int n)
    {
        Damage = n;
    }
    public virtual void MoveForward() { }
}
