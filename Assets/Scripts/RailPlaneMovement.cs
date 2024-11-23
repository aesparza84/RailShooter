using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailPlaneMovement : MonoBehaviour
{
    [Header("Plane Tracking Target")]
    [SerializeField] private Transform trackingTarget;
    private Vector3 defaultTargetPosition;
    private Vector3 targetAxisPosition;

    [Header("Plane Object")]
    [SerializeField] private Transform planeObject;
    private Vector3 planeAxisPosition;

    //Bounds for moving the aim target
    [Header("Aiming Bounds")]
    [SerializeField] private float X_AimBoundsOffset = 5;
    [SerializeField] private float Y_AimBoundsOffset = 3;
    private Vector3 aimingBounds;

    [SerializeField] private float MatchToTargetSpeed = 5;
    [SerializeField] private float MaxHorizontalLeanAngle = 60.0f;
    [SerializeField] private float MaxVerticalLeanAngle = 30.0f;
    private float currentTilt;
    private Vector3 currentTiltEuler;

    //Incoming input  direction
    private Vector2 inputDir;

    private void Start()
    {
        //Initialize default aim hint position
        defaultTargetPosition = trackingTarget.localPosition;

        //Define the bounds for aim hint


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

        //have the planeObject match the tracking target with slight delay


        MatchPlaneToAimPosition();
        HorizontalInputLean();
        VerticalInputLean();
    }

    private void TrackingTargetMovement()
    {
        if (inputDir != Vector2.zero)
        {
            aimingBounds = inputDir;
            aimingBounds.x *= X_AimBoundsOffset;
            aimingBounds.y *= Y_AimBoundsOffset;
        }
        else
        {
            aimingBounds = defaultTargetPosition;
        }

        trackingTarget.localPosition = Vector3.Lerp(trackingTarget.localPosition, aimingBounds, MatchToTargetSpeed * Time.deltaTime);
    }
    private void MatchPlaneToAimPosition()
    {
        targetAxisPosition = trackingTarget.localPosition;
        targetAxisPosition.z = planeObject.localPosition.z;

        planeObject.localPosition = Vector3.Lerp(planeObject.localPosition, targetAxisPosition, MatchToTargetSpeed * Time.deltaTime);
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
                currentTilt = Mathf.Lerp(currentTilt, 0, 5 * Time.deltaTime);
            }
        }

        currentTiltEuler.z = Mathf.LerpAngle(currentTiltEuler.z, currentTilt, 5 * Time.deltaTime);

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
                currentTilt = Mathf.Lerp(currentTilt, 0, 5 * Time.deltaTime);
            }
        }

        currentTiltEuler.x = Mathf.LerpAngle(currentTiltEuler.x, currentTilt, 5 * Time.deltaTime);

        planeObject.localEulerAngles = currentTiltEuler;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}
