using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    public abstract void Damage(int n);
    public abstract void Die();
}
