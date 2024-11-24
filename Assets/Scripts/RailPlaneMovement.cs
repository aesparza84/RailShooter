using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlaneMovement : MonoBehaviour
{
    [Header("Plane Tracking Target")]
    [SerializeField] private Transform trackingTarget;
    private Vector3 defaultTargetPosition;
    private Vector3 targetAxisPosition;
    private Vector3 dirToTarget;
    [SerializeField] private float horizontalSmoothTime = 5;
    [SerializeField] private float verticalSmoothTime = 5;

    [Header("Plane Object")]
    [SerializeField] private Transform planeObject;
    private Vector3 planeAxisPosition;

    //Bounds for moving the aim target
    [Header("Aiming Bounds")]
    [SerializeField] private PlaneRailBounds _railBounds;
    private float X_AimBoundsOffset;
    private float Y_AimBoundsOffset;
    private Vector3 aimingBounds;

    [Header("Plane Rotations")]
    [SerializeField] private float MatchToTargetSpeed = 5;
    [SerializeField] private float MaxHorizontalLeanAngle = 60.0f;
    [SerializeField] private float MaxVerticalLeanAngle = 30.0f;
    private float currentTilt;
    private Vector3 currentTiltEuler;
    private Vector3 finalEuler;

    //Incoming input  direction
    private Vector2 inputDir;

    private void Start()
    {
        //Initialize default aim hint position
        defaultTargetPosition = trackingTarget.localPosition;

        //Define the bounds for aim hint
        X_AimBoundsOffset = _railBounds.GetWidth();
        Y_AimBoundsOffset = _railBounds.GetHeight();

        targetAxisPosition = trackingTarget.localPosition;
        targetAxisPosition.z = planeObject.localPosition.z;

        planeAxisPosition = planeObject.localPosition;        
    }
    public void SetInputDir(Vector2 incoming)
    {
        inputDir = incoming;
    }
    public void MovementOnAxis()
    {
        //Move the tracking target on input
        TrackingTargetMovement();

        //Have the planeObject match the tracking target with slight delay
        //Position
        MatchPlaneToAimPosition();

        //Rotations
        AimPlaneTowardsTarget();
        HorizontalInputLean();
        VerticalInputLean();
    }

    /// <summary>
    /// Moves the tracking target based on input. Limited by bounds X_AimBoundsOffset & Y_AimBoundsOffset
    /// </summary>
    private void TrackingTargetMovement()
    {
        if (inputDir != Vector2.zero)
        {
            aimingBounds = inputDir;
            aimingBounds.x *= X_AimBoundsOffset;
            aimingBounds.y *= Y_AimBoundsOffset;
            aimingBounds.z = defaultTargetPosition.z;
        }
        else
        {
            aimingBounds = defaultTargetPosition;
        }


        trackingTarget.localPosition = Vector3.Lerp(trackingTarget.localPosition, aimingBounds, MatchToTargetSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Sets PlaneObject to follow the tracking target's position. Speed set by MatchToTargetSpeed
    /// </summary>
    private void MatchPlaneToAimPosition()
    {
        targetAxisPosition = trackingTarget.localPosition;
        targetAxisPosition.z = planeObject.localPosition.z;

        planeObject.localPosition = Vector3.Lerp(planeObject.localPosition, targetAxisPosition, MatchToTargetSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Sets PlaneObjects rotation to face direction of tracking target
    /// </summary>
    private void AimPlaneTowardsTarget()
    {
        currentTiltEuler = planeObject.localEulerAngles;
        dirToTarget = trackingTarget.localPosition - planeObject.localPosition;

        currentTiltEuler.y = Quaternion.LookRotation(dirToTarget).eulerAngles.y; 
        planeObject.localEulerAngles = currentTiltEuler;
    }

    private void HorizontalInputLean()
    {
        currentTiltEuler = planeObject.localEulerAngles;

        if (inputDir.x != 0)
        {
            currentTilt = -inputDir.x * MaxHorizontalLeanAngle;
        }
        else
        {
            if(currentTilt != 0)
            {
                currentTilt = Mathf.Lerp(currentTilt, 0, horizontalSmoothTime * Time.deltaTime);
            }
        }

        currentTiltEuler.z = Mathf.LerpAngle(currentTiltEuler.z, currentTilt, horizontalSmoothTime * Time.deltaTime);
        planeObject.localEulerAngles = currentTiltEuler;
    }

    private void VerticalInputLean()
    {
        currentTiltEuler = planeObject.localEulerAngles;

        if (inputDir.y != 0)
        {
            currentTilt = -inputDir.y * MaxVerticalLeanAngle;
        }
        else
        {
            if (currentTilt != 0)
            {
                currentTilt = Mathf.Lerp(currentTilt, 0, verticalSmoothTime * Time.deltaTime);
            }
        }

        currentTiltEuler.x = Mathf.LerpAngle(currentTiltEuler.x, currentTilt, verticalSmoothTime * Time.deltaTime);
        planeObject.localEulerAngles = currentTiltEuler;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}
