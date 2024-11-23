using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shoots ray from player object and converts ray wolrd space to screen space
/// </summary>
public class CrosshairMover : MonoBehaviour
{
    [Header("Raycast Origin")]
    [SerializeField] private Transform _rayRootTransform;

    [Header("Crosshair Object")]
    [SerializeField] private GameObject _crosshairObject;

    private Camera _mainCamera;

    //Ray for positioning crosshair
    Ray _crosshairRay;
    [SerializeField] private float _crosshairDistance = 3;

    private void Start()
    {
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        ShootCrosshairRay();
        SetCrosshairToWorldSpace();
    }

    private void ShootCrosshairRay()
    {
        _crosshairRay = new Ray(_rayRootTransform.position, _rayRootTransform.forward);
    }
    private void SetCrosshairToWorldSpace()
    {
        _crosshairObject.transform.position = _mainCamera.WorldToScreenPoint(_crosshairRay.GetPoint(_crosshairDistance));
    }
}
