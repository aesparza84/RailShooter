using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : HomingProjectile
{


    private void Start()
    {
    
    }
    private void OnEnable()
    {

    }
    private void OnDisable()
    {
        
    }
    private void Update()
    {
        if (targetTransform != null)
            targetDirection = (targetTransform.position - transform.position).normalized;

        MoveForward();   
    }

    //Interface
    public override bool CanContinueHoming()
    {
        float distance = Vector3.Distance(transform.position, targetTransform.position);
        return distance > MinHomingDistance;
    }
    public override void SetTarget(Transform target)
    {
        targetTransform = target;
    }
    public override void MoveForward()
    {
        Vector3 nextPos = Vector3.zero;

        if (CanContinueHoming())
        {
            transform.LookAt(targetTransform.position);
        }

        nextPos = transform.position + (transform.forward * Speed * Time.deltaTime);

        transform.position = nextPos;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
