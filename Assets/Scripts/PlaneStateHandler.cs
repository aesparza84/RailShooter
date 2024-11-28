using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { PLAYING, DEAD, NONE}
public class PlaneStateHandler : MonoBehaviour
{
    //Inputs
    private InputHub _inputHub;

    //Movement component
    [Header("Player GameObject")]
    [SerializeField] private GameObject playerObject;
    private RailPlaneMovement _movement;

    [Header("Crosshair")]
    [SerializeField] private GameObject crosshairObject;
    private ICrosshairReader _crosshairReader;


    //Player state
    private PlayerState _planeStateHandler;

    //Players weapons
    private Weapon _mainWeapon; //MachineGun : Weapon
    private MissileLauncherController _secondaryWeapon; //MissileLauncher : Weapon
    private void Start()
    {
        _crosshairReader = crosshairObject.GetComponent<CrosshairTargetFinder>();

        _inputHub = GetComponent<InputHub>();
        _inputHub.OnMovement += ReadMovement;
        _inputHub.OnMainFire += MainWeaponFire;
        _inputHub.OnSecondaryFire += SecondaryWeaponFire;

        if (_movement == null)
            _movement = playerObject.GetComponent<RailPlaneMovement>();

        if (_mainWeapon == null)
            _mainWeapon = playerObject.GetComponent<Weapon>();

        if (_secondaryWeapon == null)
            _secondaryWeapon = playerObject.GetComponent<MissileLauncherController>();


        _planeStateHandler = PlayerState.PLAYING;
    }
    private void OnDisable()
    {
        _inputHub.OnMovement -= ReadMovement;
        _inputHub.OnMainFire -= MainWeaponFire;
        _inputHub.OnSecondaryFire -= SecondaryWeaponFire;
    }
    private void ReadMovement(Vector2 v)
    {
        _movement.SetInputDir(v); 
    }

    private void MainWeaponFire()
    {
        if (_planeStateHandler == PlayerState.PLAYING)
            _mainWeapon.Shoot();
    }
    private void SecondaryWeaponFire()
    {
        if (_planeStateHandler == PlayerState.PLAYING)
            _secondaryWeapon.Shoot();
    }

    private void Update()
    {
        LogicStateMachine();
    }

    private void LogicStateMachine()
    {
        switch (_planeStateHandler)
        {
            case PlayerState.PLAYING:

                _crosshairReader.CreateShootOutRay();

                //Read Inputs for all plane related things
                _movement.MovementOnAxis();
                
                break;
            case PlayerState.DEAD:

                //NOT HERE YET--------------------------
                //Stop all input reading
                //Check Respawn events, etc.

                break;
            case PlayerState.NONE:

                //NOT HERE YET--------------------------
                //Stop all input reading

                break;
            default:
                break;
        }
    }
}
