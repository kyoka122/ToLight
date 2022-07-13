// GENERATED AUTOMATICALLY FROM 'Assets/InputSystem/MyInputActionMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Game
{
    public class @MyInputActionMap : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @MyInputActionMap()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""MyInputActionMap"",
    ""maps"": [
        {
            ""name"": ""Action"",
            ""id"": ""7f85da6d-9a4e-4004-91bd-fdc2bc9cf29f"",
            ""actions"": [
                {
                    ""name"": ""LateralMovement"",
                    ""type"": ""Value"",
                    ""id"": ""1474733d-c516-4e7d-bbf5-f28dbf046e27"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DepthMovement"",
                    ""type"": ""Button"",
                    ""id"": ""5c0e6ce3-84f9-4f69-a1b1-512b8f8dfa99"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ActionButton"",
                    ""type"": ""Button"",
                    ""id"": ""ddfcb755-3de5-4e24-8644-edf908999ea4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LightSwitch"",
                    ""type"": ""Button"",
                    ""id"": ""21b8995e-5c2d-45a2-a083-08cb081be6f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""48517f8e-6200-4db1-963e-7301db657767"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0d175d31-3ac0-4d39-9713-4f586cf6df1c"",
                    ""path"": ""<Keyboard>/#(A)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""256ba0df-5718-4d6a-8ad7-98063031bde7"",
                    ""path"": ""<Keyboard>/#(D)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LateralMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""8b8d9937-d2c9-4fb8-8c28-c10babbcdbde"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DepthMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""1e8d96cd-7282-42cc-80e8-469c26a4f810"",
                    ""path"": ""<Keyboard>/#(S)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DepthMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""9590c5b0-bd2f-489e-b845-ecba71206ed0"",
                    ""path"": ""<Keyboard>/#(W)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DepthMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""44b98af0-0dbe-47be-a97c-b9c6b9af0d9e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ActionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3cc92d4-af22-450d-9bf8-951259623e47"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LightSwitch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Action
            m_Action = asset.FindActionMap("Action", throwIfNotFound: true);
            m_Action_LateralMovement = m_Action.FindAction("LateralMovement", throwIfNotFound: true);
            m_Action_DepthMovement = m_Action.FindAction("DepthMovement", throwIfNotFound: true);
            m_Action_ActionButton = m_Action.FindAction("ActionButton", throwIfNotFound: true);
            m_Action_LightSwitch = m_Action.FindAction("LightSwitch", throwIfNotFound: true);
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

        // Action
        private readonly InputActionMap m_Action;
        private IActionActions m_ActionActionsCallbackInterface;
        private readonly InputAction m_Action_LateralMovement;
        private readonly InputAction m_Action_DepthMovement;
        private readonly InputAction m_Action_ActionButton;
        private readonly InputAction m_Action_LightSwitch;
        public struct ActionActions
        {
            private @MyInputActionMap m_Wrapper;
            public ActionActions(@MyInputActionMap wrapper) { m_Wrapper = wrapper; }
            public InputAction @LateralMovement => m_Wrapper.m_Action_LateralMovement;
            public InputAction @DepthMovement => m_Wrapper.m_Action_DepthMovement;
            public InputAction @ActionButton => m_Wrapper.m_Action_ActionButton;
            public InputAction @LightSwitch => m_Wrapper.m_Action_LightSwitch;
            public InputActionMap Get() { return m_Wrapper.m_Action; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ActionActions set) { return set.Get(); }
            public void SetCallbacks(IActionActions instance)
            {
                if (m_Wrapper.m_ActionActionsCallbackInterface != null)
                {
                    @LateralMovement.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnLateralMovement;
                    @LateralMovement.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnLateralMovement;
                    @LateralMovement.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnLateralMovement;
                    @DepthMovement.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnDepthMovement;
                    @DepthMovement.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnDepthMovement;
                    @DepthMovement.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnDepthMovement;
                    @ActionButton.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnActionButton;
                    @ActionButton.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnActionButton;
                    @ActionButton.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnActionButton;
                    @LightSwitch.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnLightSwitch;
                    @LightSwitch.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnLightSwitch;
                    @LightSwitch.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnLightSwitch;
                }
                m_Wrapper.m_ActionActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LateralMovement.started += instance.OnLateralMovement;
                    @LateralMovement.performed += instance.OnLateralMovement;
                    @LateralMovement.canceled += instance.OnLateralMovement;
                    @DepthMovement.started += instance.OnDepthMovement;
                    @DepthMovement.performed += instance.OnDepthMovement;
                    @DepthMovement.canceled += instance.OnDepthMovement;
                    @ActionButton.started += instance.OnActionButton;
                    @ActionButton.performed += instance.OnActionButton;
                    @ActionButton.canceled += instance.OnActionButton;
                    @LightSwitch.started += instance.OnLightSwitch;
                    @LightSwitch.performed += instance.OnLightSwitch;
                    @LightSwitch.canceled += instance.OnLightSwitch;
                }
            }
        }
        public ActionActions @Action => new ActionActions(this);
        public interface IActionActions
        {
            void OnLateralMovement(InputAction.CallbackContext context);
            void OnDepthMovement(InputAction.CallbackContext context);
            void OnActionButton(InputAction.CallbackContext context);
            void OnLightSwitch(InputAction.CallbackContext context);
        }
    }
}
