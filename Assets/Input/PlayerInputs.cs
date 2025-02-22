//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/PlayerInputs.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputs: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""JetInputs"",
            ""id"": ""0da333bd-f65c-40c1-bed3-7eecf348499c"",
            ""actions"": [
                {
                    ""name"": ""AxisMovement"",
                    ""type"": ""Value"",
                    ""id"": ""08672cd3-2903-41ea-8205-b283f56faae1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone(min=0.3)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MainAttack"",
                    ""type"": ""Button"",
                    ""id"": ""86abe2a7-0190-4cff-a96c-6724d319ede5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryAttack"",
                    ""type"": ""Button"",
                    ""id"": ""7a837169-03f8-4214-9bb6-354f0ad0b86e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2b5064ec-d709-4324-88ad-c16ebd54bece"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AxisMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1b3bd6b3-780b-4869-9104-0456fae37cf0"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MainAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5420b72f-5e7c-4a03-aa74-1e3524d41ec4"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryAttack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // JetInputs
        m_JetInputs = asset.FindActionMap("JetInputs", throwIfNotFound: true);
        m_JetInputs_AxisMovement = m_JetInputs.FindAction("AxisMovement", throwIfNotFound: true);
        m_JetInputs_MainAttack = m_JetInputs.FindAction("MainAttack", throwIfNotFound: true);
        m_JetInputs_SecondaryAttack = m_JetInputs.FindAction("SecondaryAttack", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // JetInputs
    private readonly InputActionMap m_JetInputs;
    private List<IJetInputsActions> m_JetInputsActionsCallbackInterfaces = new List<IJetInputsActions>();
    private readonly InputAction m_JetInputs_AxisMovement;
    private readonly InputAction m_JetInputs_MainAttack;
    private readonly InputAction m_JetInputs_SecondaryAttack;
    public struct JetInputsActions
    {
        private @PlayerInputs m_Wrapper;
        public JetInputsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @AxisMovement => m_Wrapper.m_JetInputs_AxisMovement;
        public InputAction @MainAttack => m_Wrapper.m_JetInputs_MainAttack;
        public InputAction @SecondaryAttack => m_Wrapper.m_JetInputs_SecondaryAttack;
        public InputActionMap Get() { return m_Wrapper.m_JetInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JetInputsActions set) { return set.Get(); }
        public void AddCallbacks(IJetInputsActions instance)
        {
            if (instance == null || m_Wrapper.m_JetInputsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_JetInputsActionsCallbackInterfaces.Add(instance);
            @AxisMovement.started += instance.OnAxisMovement;
            @AxisMovement.performed += instance.OnAxisMovement;
            @AxisMovement.canceled += instance.OnAxisMovement;
            @MainAttack.started += instance.OnMainAttack;
            @MainAttack.performed += instance.OnMainAttack;
            @MainAttack.canceled += instance.OnMainAttack;
            @SecondaryAttack.started += instance.OnSecondaryAttack;
            @SecondaryAttack.performed += instance.OnSecondaryAttack;
            @SecondaryAttack.canceled += instance.OnSecondaryAttack;
        }

        private void UnregisterCallbacks(IJetInputsActions instance)
        {
            @AxisMovement.started -= instance.OnAxisMovement;
            @AxisMovement.performed -= instance.OnAxisMovement;
            @AxisMovement.canceled -= instance.OnAxisMovement;
            @MainAttack.started -= instance.OnMainAttack;
            @MainAttack.performed -= instance.OnMainAttack;
            @MainAttack.canceled -= instance.OnMainAttack;
            @SecondaryAttack.started -= instance.OnSecondaryAttack;
            @SecondaryAttack.performed -= instance.OnSecondaryAttack;
            @SecondaryAttack.canceled -= instance.OnSecondaryAttack;
        }

        public void RemoveCallbacks(IJetInputsActions instance)
        {
            if (m_Wrapper.m_JetInputsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IJetInputsActions instance)
        {
            foreach (var item in m_Wrapper.m_JetInputsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_JetInputsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public JetInputsActions @JetInputs => new JetInputsActions(this);
    public interface IJetInputsActions
    {
        void OnAxisMovement(InputAction.CallbackContext context);
        void OnMainAttack(InputAction.CallbackContext context);
        void OnSecondaryAttack(InputAction.CallbackContext context);
    }
}
