using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrosshairReader
{
    public Vector3 GetRaycastPoint();
    public void ShootOutRay();
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

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    /// <summary>
    /// Shoots ray from camera to crosshair
    /// </summary>
    public void ShootOutRay()
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
}
