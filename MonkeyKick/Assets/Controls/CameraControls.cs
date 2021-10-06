// GENERATED AUTOMATICALLY FROM 'Assets/Controls/CameraControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MonkeyKick.Controls
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
            ""id"": ""8d7d753b-dd45-44d0-bdd6-a88e841a79f3"",
            ""actions"": [
                {
                    ""name"": ""Rotation X"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cf76bd0a-1a06-4636-9651-79d906e1dfa5"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1ae99ea4-9561-4031-ae5d-a81c286d7026"",
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
                    ""id"": ""2ea1e6e8-50f9-4270-bf11-1e50f4e3ce54"",
                    ""path"": ""<Mouse>/delta/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotation X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
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
        public struct OverworldActions
        {
            private @CameraControls m_Wrapper;
            public OverworldActions(@CameraControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @RotationX => m_Wrapper.m_Overworld_RotationX;
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
                }
                m_Wrapper.m_OverworldActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @RotationX.started += instance.OnRotationX;
                    @RotationX.performed += instance.OnRotationX;
                    @RotationX.canceled += instance.OnRotationX;
                }
            }
        }
        public OverworldActions @Overworld => new OverworldActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
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
        }
    }
}
