using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState { PLAYING, DEAD, NONE}
public class PlaneStateHandler : MonoBehaviour
{
    //Inputs
    private InputHub _inputHub;

    //Movement component
    private RailPlaneMovement _movement;

    //Player state
    private PlayerState _planeStateHandler;

    private void Start()
    {
        _inputHub = GetComponent<InputHub>();
        _inputHub.OnMovement += ReadMovement;


        _movement = GetComponent<RailPlaneMovement>();

        _planeStateHandler = PlayerState.PLAYING;
    }
    private void OnDisable()
    {
        _inputHub.OnMovement -= ReadMovement;
    }
    private void ReadMovement(Vector2 v)
    {
        _movement.SetInputDir(v); 
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
