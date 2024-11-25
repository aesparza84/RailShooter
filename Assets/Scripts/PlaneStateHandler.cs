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

    //Player state
    private PlayerState _planeStateHandler;

    //Players weapon
    private IWeapon weapon;
    private void Start()
    {
        _inputHub = GetComponent<InputHub>();
        _inputHub.OnMovement += ReadMovement;
        _inputHub.OnMainFire += MainWeaponFire;

        if (_movement == null)
            _movement = playerObject.GetComponent<RailPlaneMovement>();

        if (weapon == null)
            weapon = playerObject.GetComponent<Weapon>();

        _planeStateHandler = PlayerState.PLAYING;
    }
    private void OnDisable()
    {
        _inputHub.OnMovement -= ReadMovement;
        _inputHub.OnMainFire -= MainWeaponFire;
    }
    private void ReadMovement(Vector2 v)
    {
        _movement.SetInputDir(v); 
    }

    private void MainWeaponFire()
    {
        if (_planeStateHandler == PlayerState.PLAYING)
            weapon.Shoot();
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
