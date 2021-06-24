// GENERATED AUTOMATICALLY FROM 'Assets/_GAME/Camera/CameraControls.inputactions'

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
                    ""name"": ""OrbitCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2603d25a-f87e-4b89-a37b-7629558e3f6c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9ff256b4-3f24-4055-934e-80326d256ece"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OrbitCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1e7ca59a-6933-4025-b5b1-2a57c6b38cd9"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OrbitCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Overworld
            m_Overworld = asset.FindActionMap("Overworld", throwIfNotFound: true);
            m_Overworld_OrbitCamera = m_Overworld.FindAction("OrbitCamera", throwIfNotFound: true);
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
        private readonly InputAction m_Overworld_OrbitCamera;
        public struct OverworldActions
        {
            private @CameraControls m_Wrapper;
            public OverworldActions(@CameraControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @OrbitCamera => m_Wrapper.m_Overworld_OrbitCamera;
            public InputActionMap Get() { return m_Wrapper.m_Overworld; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(OverworldActions set) { return set.Get(); }
            public void SetCallbacks(IOverworldActions instance)
            {
                if (m_Wrapper.m_OverworldActionsCallbackInterface != null)
                {
                    @OrbitCamera.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOrbitCamera;
                    @OrbitCamera.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOrbitCamera;
                    @OrbitCamera.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOrbitCamera;
                }
                m_Wrapper.m_OverworldActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @OrbitCamera.started += instance.OnOrbitCamera;
                    @OrbitCamera.performed += instance.OnOrbitCamera;
                    @OrbitCamera.canceled += instance.OnOrbitCamera;
                }
            }
        }
        public OverworldActions @Overworld => new OverworldActions(this);
        public interface IOverworldActions
        {
            void OnOrbitCamera(InputAction.CallbackContext context);
        }
    }
}
