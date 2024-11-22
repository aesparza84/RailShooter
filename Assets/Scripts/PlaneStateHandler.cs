using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { PLAYING, DEAD, NONE}
public class PlaneStateHandler : MonoBehaviour
{
    [Header("Crosshair")]
    [SerializeField] private CrosshairMover _crosshairMover;

    private PlaneStateHandler _planeStateHandler;
}
