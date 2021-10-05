// GENERATED AUTOMATICALLY FROM 'Assets/_MK_Scripts/Camera/CameraControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MonkeyKick.CameraTools
{
    public class @CameraControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @CameraControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""CameraControls"",
    ""maps"": [
        {
            ""name"": ""Overworld"",
            ""id"": ""0bb8b629-843c-4ea2-bc17-2d640e008e2a"",
            ""actions"": [
                {
                    ""name"": ""Rotation X"",
                    ""type"": ""PassThrough"",
                    ""id"": ""96527101-df07-4517-a266-84c3aba7acab"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Rotation Y"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6909538c-7441-49ff-bca7-479eb388f167"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""090ec263-5144-493b-a0f1-34a8f5e0bb6d"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotation X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1e58597-b6d9-426a-867a-609ee25115fd"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotation X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee4ce4c4-8f40-4689-8d94-d7ea9715d922"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotation Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bd184bd-e833-452d-a0b8-763b07765bfa"",
                    ""path"": ""<Mouse>/delta/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Rotation Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // Overworld
            m_Overworld = asset.FindActionMap("Overworld", throwIfNotFound: true);
            m_Overworld_RotationX = m_Overworld.FindAction("Rotation X", throwIfNotFound: true);
            m_Overworld_RotationY = m_Overworld.FindAction("Rotation Y", throwIfNotFound: true);
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

        // Overworld
        private readonly InputActionMap m_Overworld;
        private IOverworldActions m_OverworldActionsCallbackInterface;
        private readonly InputAction m_Overworld_RotationX;
        private readonly InputAction m_Overworld_RotationY;
        public struct OverworldActions
        {
            private @CameraControls m_Wrapper;
            public OverworldActions(@CameraControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @RotationX => m_Wrapper.m_Overworld_RotationX;
            public InputAction @RotationY => m_Wrapper.m_Overworld_RotationY;
            public InputActionMap Get() { return m_Wrapper.m_Overworld; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(OverworldActions set) { return set.Get(); }
            public void SetCallbacks(IOverworldActions instance)
            {
                if (m_Wrapper.m_OverworldActionsCallbackInterface != null)
                {
                    @RotationX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRotationX;
                    @RotationX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRotationX;
                    @RotationX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRotationX;
                    @RotationY.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRotationY;
                    @RotationY.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRotationY;
                    @RotationY.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRotationY;
                }
                m_Wrapper.m_OverworldActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @RotationX.started += instance.OnRotationX;
                    @RotationX.performed += instance.OnRotationX;
                    @RotationX.canceled += instance.OnRotationX;
                    @RotationY.started += instance.OnRotationY;
                    @RotationY.performed += instance.OnRotationY;
                    @RotationY.canceled += instance.OnRotationY;
                }
            }
        }
        public OverworldActions @Overworld => new OverworldActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
                return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IOverworldActions
        {
            void OnRotationX(InputAction.CallbackContext context);
            void OnRotationY(InputAction.CallbackContext context);
        }
    }
}
