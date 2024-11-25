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

    private bool MainFireHeld;

    //Input Delegates
    public Action<Vector2> OnMovement;
    public Action OnMainFire;
    private void OnEnable()
    {
        if (_mappedInputs == null)
            _mappedInputs = new PlayerInputs();

        AxisMovement = _mappedInputs.JetInputs.AxisMovement;
        AxisMovement.performed += OnAxisMovement;
        AxisMovement.canceled += OnAxisMovement;
        AxisMovement.Enable();

        MainAttack = _mappedInputs.JetInputs.MainAttack;
        MainAttack.started += MainAttack_started;
        MainAttack.canceled += MainAttack_canceled;
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
        MainAttack.started -= MainAttack_started;
        MainAttack.canceled -= MainAttack_canceled;
    }
    private void SecondaryAttack_canceled(InputAction.CallbackContext obj)
    {

    }
    private void SecondaryAttack_started(InputAction.CallbackContext obj)
    {

    }
    private void MainAttack_started(InputAction.CallbackContext obj)
    {
        MainFireHeld = true;
        OnMainFire?.Invoke();
    }
    private void MainAttack_canceled(InputAction.CallbackContext obj)
    {
        MainFireHeld = false;
    }

    private void Update()
    {
        CheckForButtonHolds();
    }
    private void CheckForButtonHolds()
    {
        if (MainFireHeld)
            OnMainFire?.Invoke();
    }
    private void OnAxisMovement(InputAction.CallbackContext obj)
    {
        Debug.Log("On movement reccived");
        OnMovement?.Invoke(obj.ReadValue<Vector2>());
    }
}
