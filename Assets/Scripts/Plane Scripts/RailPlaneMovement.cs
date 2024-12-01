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
    [SerializeField] private float TargetMoveSpeed = 5;
    [SerializeField] private float ModifiedTargetMoveSpeed = 5;
    [SerializeField] private bool RecenterOnNoInput;
    [SerializeField] private bool RecenterVerticalOnNoInput;
    [SerializeField] private bool RecenterHorizontalOnNoInput;

    [SerializeField] private float HorizontalInputMultiplier = 1;
    [SerializeField] private float VerticalInputMultiplier = 1;

    [Header("Plane Object")]
    [SerializeField] private Transform planeObject;
    private Vector3 planeLocalPosition;
    private float X_planeClamp;
    private float Y_planeClamp;

    //Bounds for moving the aim target
    [Header("Aiming Bounds")]
    [SerializeField] private PlaneRailBounds _railBounds;
    private float X_AimBoundsOffset;
    private float Y_AimBoundsOffset;
    private float X_CurrentBounds;
    private float Y_CurrentBounds;
    private Vector3 aimingBounds;

    [Header("Plane Rotations")]
    [SerializeField] private float horizontalSmoothTime = 5;
    [SerializeField] private float verticalSmoothTime = 5;
    [SerializeField] private float MatchToTargetPosSpeed = 5;
    [SerializeField] private float MatchToTargetRotSpeed = 5;
    [SerializeField] private float MaxHorizontalLeanAngle = 60.0f;
    [SerializeField] private float MaxVerticalLeanAngle = 30.0f;
    private float currentHorizontalTilt;
    private float currentVerticalTilt;
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

        X_planeClamp = X_AimBoundsOffset * 0.8f;
        Y_planeClamp = Y_AimBoundsOffset;

        targetAxisPosition = trackingTarget.localPosition;
        targetAxisPosition.z = planeObject.localPosition.z;


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

        AimPlaneTowardsTarget();
        HorizontalInputLean();
        VerticalInputLean();
    }

    /// <summary>
    /// Moves the tracking target based on input. Limited by bounds X_AimBoundsOffset & Y_AimBoundsOffset
    /// </summary>
    private void TrackingTargetMovement()
    {
        if (ModifiedTargetMoveSpeed != TargetMoveSpeed)
            ModifiedTargetMoveSpeed = TargetMoveSpeed;

        if (inputDir != Vector2.zero)
        {
            aimingBounds = inputDir;
            //aimingBounds.x *= X_AimBoundsOffset;
            //aimingBounds.y *= Y_AimBoundsOffset;

            X_CurrentBounds += aimingBounds.x * HorizontalInputMultiplier;
            Y_CurrentBounds += aimingBounds.y * VerticalInputMultiplier;

            X_CurrentBounds = Mathf.Clamp(X_CurrentBounds, -X_AimBoundsOffset, X_AimBoundsOffset);
            Y_CurrentBounds = Mathf.Clamp(Y_CurrentBounds, -Y_AimBoundsOffset, Y_AimBoundsOffset);

            aimingBounds.x = X_CurrentBounds;
            aimingBounds.y = Y_CurrentBounds;

            aimingBounds.z = defaultTargetPosition.z;

            
            ModifiedTargetMoveSpeed = TargetMoveSpeed * inputDir.magnitude;
        }
        else
        {            
            if (RecenterHorizontalOnNoInput)
                aimingBounds.x = defaultTargetPosition.x;

            if (RecenterVerticalOnNoInput)
                aimingBounds.y = defaultTargetPosition.y;

            if (RecenterOnNoInput)
                aimingBounds = defaultTargetPosition;
        }

        trackingTarget.localPosition = Vector3.Lerp(trackingTarget.localPosition, aimingBounds, ModifiedTargetMoveSpeed * Time.deltaTime);
        aimingBounds = trackingTarget.localPosition;
    }

    /// <summary>
    /// Sets PlaneObject to follow the tracking target's position. Speed set by MatchToTargetSpeed
    /// </summary>
    private void MatchPlaneToAimPosition()
    {
        targetAxisPosition = trackingTarget.localPosition;
        targetAxisPosition.z = planeObject.localPosition.z;
        planeLocalPosition = planeObject.localPosition;

        //planeLocalPosition = Vector3.Lerp(planeLocalPosition, targetAxisPosition, MatchToTargetPosSpeed * Time.deltaTime);
        //planeLocalPosition.x = Mathf.Clamp(planeLocalPosition.x, -X_planeClamp, X_planeClamp);
        //planeLocalPosition.y = Mathf.Clamp(planeLocalPosition.y, -Y_planeClamp, Y_planeClamp);

        planeObject.localPosition = Vector3.Lerp(planeObject.localPosition, targetAxisPosition, MatchToTargetPosSpeed * Time.deltaTime);
        //planeObject.localPosition = planeLocalPosition;
    }

    /// <summary>
    /// Sets PlaneObjects rotation to face direction of tracking target
    /// </summary>
    private void AimPlaneTowardsTarget()
    {
        currentTiltEuler = planeObject.localEulerAngles;
        dirToTarget = trackingTarget.localPosition - planeObject.localPosition;

        float newY = Quaternion.LookRotation(dirToTarget).eulerAngles.y;
        currentTiltEuler.y = Mathf.LerpAngle(currentTiltEuler.y, newY, MatchToTargetRotSpeed * Time.deltaTime);
        planeObject.localEulerAngles = currentTiltEuler;
    }

    private void HorizontalInputLean()
    {
        currentTiltEuler = planeObject.localEulerAngles;

        if (inputDir.x != 0)
        {
            currentHorizontalTilt = -inputDir.x * MaxHorizontalLeanAngle;
        }
        else
        {
            if (currentHorizontalTilt != 0)
            {
                //currentTilt = Mathf.Lerp(currentTilt, 0, horizontalSmoothTime * Time.deltaTime);
                currentHorizontalTilt = 0;
            }
        }

        currentTiltEuler.z = Mathf.LerpAngle(currentTiltEuler.z, currentHorizontalTilt, horizontalSmoothTime * Time.deltaTime);
        planeObject.localEulerAngles = currentTiltEuler;
    }

    private void VerticalInputLean()
    {
        currentTiltEuler = planeObject.localEulerAngles;

        if (inputDir.y != 0)
        {
            currentVerticalTilt = -inputDir.y * MaxVerticalLeanAngle;
        }
        else
        {
            if (currentVerticalTilt != 0)
            {
                //currentTilt = Mathf.Lerp(currentTilt, 0, verticalSmoothTime * Time.deltaTime);
                currentVerticalTilt = 0;
            }
        }

        currentTiltEuler.x = Mathf.LerpAngle(currentTiltEuler.x, currentVerticalTilt, verticalSmoothTime * Time.deltaTime);
        planeObject.localEulerAngles = currentTiltEuler;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
    }
}
