using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrosshairReader
{
    /// <summary>
    /// Returns the endpoint of the shoot-ray
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRaycastPoint();

    /// <summary>
    /// Sets shoot-ray pos/direction and shoots out
    /// </summary>
    public void CreateShootOutRay();

    /// <summary>
    /// Event that sends raycastHit when overlapping a target
    /// </summary>
    public event EventHandler<RaycastHit> OnTargetOverlapped;
}
public class CrosshairTargetFinder : MonoBehaviour, ICrosshairReader
{
    [Header("Crosshair Object")]
    [SerializeField] private GameObject _crosshairObject;
    private Ray crosshairShootRay;

    private const int DestrutableLayer = (1 << 6);

    //Ray Vectors
    Vector3 dirMagnitude;
    Vector3 endpoint;

    //Main camera reference
    private Camera _mainCamera;

    //Overlap event
    public event EventHandler<RaycastHit> OnTargetOverlapped;
    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        CheckForTargetOverlap();
    }

    /// <summary>
    /// Creates ray from camera to crosshair
    /// </summary>
    public void CreateShootOutRay()
    {
        endpoint = _mainCamera.ScreenToWorldPoint(_crosshairObject.transform.position);

        dirMagnitude = (endpoint - _mainCamera.transform.position);

        crosshairShootRay = new Ray(_mainCamera.transform.position, dirMagnitude);

        Debug.DrawRay(_mainCamera.transform.position, dirMagnitude, Color.blue, 1.5f);
    }

    /// <summary>
    /// Returns the endpoint of the crosshair screen to world raycast
    /// </summary>
    /// <returns></returns>
    public Vector3 GetRaycastPoint()
    {
        if (Physics.Raycast(crosshairShootRay, out RaycastHit hit, dirMagnitude.magnitude, DestrutableLayer))
        {
            return hit.point;
        }

        return crosshairShootRay.GetPoint(dirMagnitude.magnitude);
    }

    public void CheckForTargetOverlap()
    {
        if (Physics.Raycast(crosshairShootRay, out RaycastHit hit, dirMagnitude.magnitude, DestrutableLayer))
        {
            OnTargetOverlapped?.Invoke(this, hit);
        }
    }
}
