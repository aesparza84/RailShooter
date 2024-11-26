using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHomingProjectile 
{
    public void SetTarget(Transform target);
    public void SetDamage(int n);
    public bool CanContinueHoming();
    public void MoveForward();
}
