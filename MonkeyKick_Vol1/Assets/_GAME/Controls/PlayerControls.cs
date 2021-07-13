// GENERATED AUTOMATICALLY FROM 'Assets/_GAME/Controls/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace MonkeyKick.Controls
{
    public class @PlayerControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Overworld"",
            ""id"": ""83a52c2a-e6ff-4023-8cbf-f6413fc8efe9"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c70ada0f-1d28-4625-8b75-f78ee454d8a0"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f5a3c2fd-425e-430b-b639-fb9628596b61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""d6d26187-ede1-4b28-877d-cd9648c06ece"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""b0e59310-abf6-4905-8d72-30384bc9802a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""4f9e3c70-0255-406b-b6f0-df62e9d6df33"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""dc9757c9-4374-4a11-9c7b-000bce742a53"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f87bf968-472c-4c0a-88ba-a80248771d39"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""626b1ca7-9aa1-41b2-b419-e405bf340b91"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""1c935ec2-e8e2-4315-99f1-bec1524da609"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c511939f-7c9f-4a95-9672-fa7f5349a3d4"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""850067fd-d471-4584-a252-7d4b4a1a3598"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b6201ab4-6f61-47db-a40d-f18b57fb0209"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c4faec51-c24d-4133-bd7b-2a373209b102"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""BattleMenu"",
            ""id"": ""16cdfe5f-e841-4b31-a14c-329cb737d561"",
            ""actions"": [
                {
                    ""name"": ""MoveSelector"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f65c6656-3475-408a-b7dc-a67621156f98"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""8f29e22d-22f5-49d0-b534-628204175fd1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""5d1cce2a-e34b-4c89-b06c-36ff6bc3ab62"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""ae500acd-c83a-4750-8fcc-22b030edb1ff"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelector"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""11f12163-c2be-4277-9b16-1af408e78990"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""fa785ed4-92a6-498f-b1f2-ec6b63abedce"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""90adb974-5b51-48d5-aafb-c3f1af0546f5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8ae3ca7a-0a3c-4472-9b75-bca62456066a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6a762904-2685-43cf-9e55-eef461f930de"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MoveSelector"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36aaba0a-cf59-4f65-b7e1-e324e2ea4d8f"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""82865bf2-feba-429b-8c16-b77a02d5898a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f77954cb-dcb8-44bc-ba2c-7e68fb77d425"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e89eb9a9-3516-4ef4-ba9c-29b3aaaa1241"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09f2eb00-7293-471a-b6ee-608e6107e215"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c933ee86-0592-43a7-99d1-b987ed038188"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""245ed576-a781-403c-8ba4-cc0c5061520d"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Battle"",
            ""id"": ""ef39955d-3984-4725-9eb2-0f27ba0c88bc"",
            ""actions"": [
                {
                    ""name"": ""Joystick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""698b6525-b3da-4825-bcba-9e533f2cd8ed"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""North"",
                    ""type"": ""Button"",
                    ""id"": ""420e11fa-8212-4830-8d4c-adb55910fb39"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""South"",
                    ""type"": ""Button"",
                    ""id"": ""42033926-110f-4bed-9b8b-405f2e0d0d79"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""East"",
                    ""type"": ""Button"",
                    ""id"": ""95086235-9115-4a52-bd22-24f81fa2d29e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""West"",
                    ""type"": ""Button"",
                    ""id"": ""4ac21978-3180-4839-b0bb-b62f6616bdd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7f2a138c-c74b-4f8c-9f22-053fe5d504f0"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""75314aa4-a4ec-446c-84e2-4418e5bb08e0"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2bede422-3c69-425d-9478-8fe9992515b4"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f2956779-a4c5-49c8-978c-c7529dc6651e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3820fff6-07b6-428f-a8b8-f5c5278bdd9d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""d26db4c2-9ed4-43e5-9961-c9a8d94a5c43"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Joystick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""08b907d6-61bf-43d7-92d9-1127a58c71e8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""South"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56835c98-55c1-4c6e-a0f6-4571a8bac123"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""South"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e2b7fcf1-1181-4e11-95bb-8a0a4409be14"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""North"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""770804be-2b18-428f-abda-29fd29c71a34"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""North"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da4dc003-93bf-4c4d-8948-9c8305e4745c"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""East"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32f86bf2-7134-408b-b84f-a753fd556895"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""East"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6637ea21-8c1b-47c8-b2ea-0de6bf8a03b2"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""West"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c181a13-4cce-4997-84b6-b8e20ae9d06f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard & Mouse"",
                    ""action"": ""West"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard & Mouse"",
            ""bindingGroup"": ""Keyboard & Mouse"",
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
            m_Overworld_Move = m_Overworld.FindAction("Move", throwIfNotFound: true);
            m_Overworld_Jump = m_Overworld.FindAction("Jump", throwIfNotFound: true);
            m_Overworld_Sprint = m_Overworld.FindAction("Sprint", throwIfNotFound: true);
            // BattleMenu
            m_BattleMenu = asset.FindActionMap("BattleMenu", throwIfNotFound: true);
            m_BattleMenu_MoveSelector = m_BattleMenu.FindAction("MoveSelector", throwIfNotFound: true);
            m_BattleMenu_Select = m_BattleMenu.FindAction("Select", throwIfNotFound: true);
            m_BattleMenu_Cancel = m_BattleMenu.FindAction("Cancel", throwIfNotFound: true);
            // Battle
            m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
            m_Battle_Joystick = m_Battle.FindAction("Joystick", throwIfNotFound: true);
            m_Battle_North = m_Battle.FindAction("North", throwIfNotFound: true);
            m_Battle_South = m_Battle.FindAction("South", throwIfNotFound: true);
            m_Battle_East = m_Battle.FindAction("East", throwIfNotFound: true);
            m_Battle_West = m_Battle.FindAction("West", throwIfNotFound: true);
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
        private readonly InputAction m_Overworld_Move;
        private readonly InputAction m_Overworld_Jump;
        private readonly InputAction m_Overworld_Sprint;
        public struct OverworldActions
        {
            private @PlayerControls m_Wrapper;
            public OverworldActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_Overworld_Move;
            public InputAction @Jump => m_Wrapper.m_Overworld_Jump;
            public InputAction @Sprint => m_Wrapper.m_Overworld_Sprint;
            public InputActionMap Get() { return m_Wrapper.m_Overworld; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(OverworldActions set) { return set.Get(); }
            public void SetCallbacks(IOverworldActions instance)
            {
                if (m_Wrapper.m_OverworldActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                    @Jump.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnJump;
                    @Sprint.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSprint;
                    @Sprint.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSprint;
                    @Sprint.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSprint;
                }
                m_Wrapper.m_OverworldActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Sprint.started += instance.OnSprint;
                    @Sprint.performed += instance.OnSprint;
                    @Sprint.canceled += instance.OnSprint;
                }
            }
        }
        public OverworldActions @Overworld => new OverworldActions(this);

        // BattleMenu
        private readonly InputActionMap m_BattleMenu;
        private IBattleMenuActions m_BattleMenuActionsCallbackInterface;
        private readonly InputAction m_BattleMenu_MoveSelector;
        private readonly InputAction m_BattleMenu_Select;
        private readonly InputAction m_BattleMenu_Cancel;
        public struct BattleMenuActions
        {
            private @PlayerControls m_Wrapper;
            public BattleMenuActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveSelector => m_Wrapper.m_BattleMenu_MoveSelector;
            public InputAction @Select => m_Wrapper.m_BattleMenu_Select;
            public InputAction @Cancel => m_Wrapper.m_BattleMenu_Cancel;
            public InputActionMap Get() { return m_Wrapper.m_BattleMenu; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BattleMenuActions set) { return set.Get(); }
            public void SetCallbacks(IBattleMenuActions instance)
            {
                if (m_Wrapper.m_BattleMenuActionsCallbackInterface != null)
                {
                    @MoveSelector.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnMoveSelector;
                    @MoveSelector.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnMoveSelector;
                    @MoveSelector.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnMoveSelector;
                    @Select.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSelect;
                    @Select.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSelect;
                    @Select.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnSelect;
                    @Cancel.started -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_BattleMenuActionsCallbackInterface.OnCancel;
                }
                m_Wrapper.m_BattleMenuActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveSelector.started += instance.OnMoveSelector;
                    @MoveSelector.performed += instance.OnMoveSelector;
                    @MoveSelector.canceled += instance.OnMoveSelector;
                    @Select.started += instance.OnSelect;
                    @Select.performed += instance.OnSelect;
                    @Select.canceled += instance.OnSelect;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                }
            }
        }
        public BattleMenuActions @BattleMenu => new BattleMenuActions(this);

        // Battle
        private readonly InputActionMap m_Battle;
        private IBattleActions m_BattleActionsCallbackInterface;
        private readonly InputAction m_Battle_Joystick;
        private readonly InputAction m_Battle_North;
        private readonly InputAction m_Battle_South;
        private readonly InputAction m_Battle_East;
        private readonly InputAction m_Battle_West;
        public struct BattleActions
        {
            private @PlayerControls m_Wrapper;
            public BattleActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Joystick => m_Wrapper.m_Battle_Joystick;
            public InputAction @North => m_Wrapper.m_Battle_North;
            public InputAction @South => m_Wrapper.m_Battle_South;
            public InputAction @East => m_Wrapper.m_Battle_East;
            public InputAction @West => m_Wrapper.m_Battle_West;
            public InputActionMap Get() { return m_Wrapper.m_Battle; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(BattleActions set) { return set.Get(); }
            public void SetCallbacks(IBattleActions instance)
            {
                if (m_Wrapper.m_BattleActionsCallbackInterface != null)
                {
                    @Joystick.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnJoystick;
                    @Joystick.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnJoystick;
                    @Joystick.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnJoystick;
                    @North.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnNorth;
                    @North.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnNorth;
                    @North.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnNorth;
                    @South.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnSouth;
                    @South.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnSouth;
                    @South.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnSouth;
                    @East.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnEast;
                    @East.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnEast;
                    @East.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnEast;
                    @West.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnWest;
                    @West.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnWest;
                    @West.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnWest;
                }
                m_Wrapper.m_BattleActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Joystick.started += instance.OnJoystick;
                    @Joystick.performed += instance.OnJoystick;
                    @Joystick.canceled += instance.OnJoystick;
                    @North.started += instance.OnNorth;
                    @North.performed += instance.OnNorth;
                    @North.canceled += instance.OnNorth;
                    @South.started += instance.OnSouth;
                    @South.performed += instance.OnSouth;
                    @South.canceled += instance.OnSouth;
                    @East.started += instance.OnEast;
                    @East.performed += instance.OnEast;
                    @East.canceled += instance.OnEast;
                    @West.started += instance.OnWest;
                    @West.performed += instance.OnWest;
                    @West.canceled += instance.OnWest;
                }
            }
        }
        public BattleActions @Battle => new BattleActions(this);
        private int m_KeyboardMouseSchemeIndex = -1;
        public InputControlScheme KeyboardMouseScheme
        {
            get
            {
                if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard & Mouse");
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
            void OnMove(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnSprint(InputAction.CallbackContext context);
        }
        public interface IBattleMenuActions
        {
            void OnMoveSelector(InputAction.CallbackContext context);
            void OnSelect(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
        }
        public interface IBattleActions
        {
            void OnJoystick(InputAction.CallbackContext context);
            void OnNorth(InputAction.CallbackContext context);
            void OnSouth(InputAction.CallbackContext context);
            void OnEast(InputAction.CallbackContext context);
            void OnWest(InputAction.CallbackContext context);
        }
    }
}
