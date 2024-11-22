using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHub : MonoBehaviour
{
    private PlayerInputs _mappedInputs;

    //Input Actions
    private InputAction AxisMovement;
    private InputAction MainAttack;
    private InputAction SecondaryAttack;

    //Input Delegates
    public Action<Vector2> OnMovement;
    private void OnEnable()
    {
        if (_mappedInputs == null)
            _mappedInputs = new PlayerInputs();

        AxisMovement = _mappedInputs.JetInputs.AxisMovement;
        AxisMovement.performed += OnAxisMovement;
        AxisMovement.canceled += OnAxisMovement;
        AxisMovement.Enable();

        MainAttack = _mappedInputs.JetInputs.MainAttack;
        MainAttack.performed += MainAttack_performed;
        MainAttack.Enable();

        SecondaryAttack = _mappedInputs.JetInputs.SecondaryAttack;
        SecondaryAttack.started += SecondaryAttack_started;
        SecondaryAttack.canceled += SecondaryAttack_canceled;
        SecondaryAttack.Enable();
    }
    private void OnDisable()
    {
        AxisMovement.performed -= OnAxisMovement;
        SecondaryAttack.started -= SecondaryAttack_started;
        SecondaryAttack.canceled -= SecondaryAttack_canceled;
        MainAttack.performed -= MainAttack_performed;
    }
    private void SecondaryAttack_canceled(InputAction.CallbackContext obj)
    {

    }
    private void SecondaryAttack_started(InputAction.CallbackContext obj)
    {

    }
    private void MainAttack_performed(InputAction.CallbackContext obj)
    {

    }
    private void OnAxisMovement(InputAction.CallbackContext obj)
    {
        Debug.Log("On movement reccived");
        OnMovement?.Invoke(obj.ReadValue<Vector2>());
    }
}
