using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UIElements;

public class CrosshairMover : MonoBehaviour
{
    [Header("Crosshair Object")]
    [SerializeField] private GameObject _crosshairObject;
    private Vector3 crosshairResetPosition;
    private Vector3 crosshairRawPosition;
    private Vector3 crosshairWorldPosition;
    private Vector3 dirToCrossHairWorldPos;

    //Cached main camera
    private Camera _mainCamera;

    //World pos ray-hit
    RaycastHit hit;
    private Vector3 hitPosition;

    private void Start()
    {
        _mainCamera = Camera.main;

        crosshairResetPosition = new Vector3(0, 100, 0);
        crosshairRawPosition = Vector3.zero;
        crosshairWorldPosition= Vector3.zero;
        dirToCrossHairWorldPos= Vector3.zero;
    }

    public void SetCrosshairPoint(Vector2 v)
    {
        if (v != Vector2.zero)
        {
            Vector3 newPos = new Vector3(_crosshairObject.transform.position.x + v.x,
                                     _crosshairObject.transform.position.y + v.y, 0);

            _crosshairObject.transform.position = newPos;
        }
        else
        {
            _crosshairObject.transform.position = Vector3.Lerp(_crosshairObject.transform.position, crosshairResetPosition, 5 * Time.deltaTime);
        }
    }

    private void Update()
    {
        ConvertCrosshairToWorld();
    }

    private void ConvertCrosshairToWorld()
    {
        /*
        - Convert crosshair position to world space
        - Cast ray from camera to worls space positoin
        */

        crosshairRawPosition = _crosshairObject.transform.position;
        crosshairRawPosition.z = _mainCamera.farClipPlane;
        crosshairWorldPosition = _mainCamera.ScreenToWorldPoint(crosshairRawPosition);
        dirToCrossHairWorldPos = (crosshairWorldPosition - _mainCamera.transform.position).normalized;

        if (Physics.Raycast(_mainCamera.transform.position, dirToCrossHairWorldPos, out hit, Mathf.Infinity))
        {
            hitPosition = hit.point;
        }
    }

    public Vector3 GetCrosshairHitPoint()
    {
        return hitPosition;
    }
}
