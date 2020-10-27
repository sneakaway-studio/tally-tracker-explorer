// GENERATED AUTOMATICALLY FROM 'Assets/_Project/Input/TallyInputSystem.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TallyInputSystem : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TallyInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""TallyInputSystem"",
    ""maps"": [
        {
            ""name"": ""Debug"",
            ""id"": ""74bbc17b-b05a-4e6d-937d-6df58efa5eca"",
            ""actions"": [
                {
                    ""name"": ""MonsterAdd"",
                    ""type"": ""Button"",
                    ""id"": ""2e1c8681-c476-4d7d-9444-8b8c71d0c6f1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim1"",
                    ""type"": ""Button"",
                    ""id"": ""54c0976a-8bd1-4f8c-b843-1f15414067be"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim2"",
                    ""type"": ""Button"",
                    ""id"": ""1020b600-ff88-4efb-88b4-ee04164d300f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim3"",
                    ""type"": ""Button"",
                    ""id"": ""06a5984b-d932-42f7-9c90-136d6870f777"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim4"",
                    ""type"": ""Button"",
                    ""id"": ""65689a2e-ac10-4982-ac15-d3c1f32ab0ec"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim5"",
                    ""type"": ""Button"",
                    ""id"": ""587161f6-22ad-4a00-8285-f6335bb964d6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim6"",
                    ""type"": ""Button"",
                    ""id"": ""11913c34-79d6-4be6-a198-172086cc3004"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Anim7"",
                    ""type"": ""Button"",
                    ""id"": ""7c90a263-6be6-4162-8d2f-75efd73514bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""4753bdcc-96a4-4280-bc71-a79cf710715e"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MonsterAdd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""371bd49e-889f-4624-9c72-421db4e274ea"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a2dd5d10-b207-45c9-95de-30e8b314fbad"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""070d6892-b87f-426e-9c1b-32e1b1c0afda"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9cdad74e-dbc9-41ee-8acf-4fbb673cd827"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""05780e6c-0aa1-4cda-9cf0-3c71b2406843"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40ac839d-1a0c-467d-a3e7-a41b637f77f8"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""24a79d89-aec1-4396-a14c-049d698c43d5"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Anim7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""d195a62a-457b-460b-9179-b65765f7f1ec"",
            ""actions"": [
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""bea56675-7e11-4bf5-9b26-ea711d24529d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ControlToggle"",
                    ""type"": ""Button"",
                    ""id"": ""97c27117-c28d-4a7b-8968-f8bbf3005bf5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""FeedToggle"",
                    ""type"": ""Button"",
                    ""id"": ""dafd4647-22fc-4801-ac2b-553fb7fbd333"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TimelineToggle"",
                    ""type"": ""Button"",
                    ""id"": ""8d1b0fed-6d87-49f9-b7dd-4816b629fbf8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""003f3976-2c5a-497c-b2f4-949de26d595f"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""766cb40e-8027-4b36-93ff-b0b0252da797"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3a3b936-5deb-4ff1-abd3-b6243c407d78"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ControlToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4c168153-1109-4ebb-92b3-f862a5156ba8"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FeedToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""409334c9-50f5-4f38-af66-6bf58ea44d42"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TimelineToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Player"",
            ""id"": ""b93fa1bf-987d-4c15-8011-9df621ee8965"",
            ""actions"": [
                {
                    ""name"": ""HorizontalMovement"",
                    ""type"": ""Value"",
                    ""id"": ""bd53890f-9e3c-4b28-81cd-8baef2a0a6f2"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""VerticalMovement"",
                    ""type"": ""Value"",
                    ""id"": ""4322f29c-e188-4777-9f5a-2a775b7564b7"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectLeft"",
                    ""type"": ""Button"",
                    ""id"": ""642eb606-4a97-4d33-b73b-45620453c96d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectRight"",
                    ""type"": ""Button"",
                    ""id"": ""96718a59-70f6-4072-8c60-62f078ce7280"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomIn"",
                    ""type"": ""Button"",
                    ""id"": ""674cc3c2-ee52-4af9-8da6-f4ffe4b0a7ae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ZoomOut"",
                    ""type"": ""Button"",
                    ""id"": ""5d09888a-b4c6-4dfa-8230-8ed03b2b451f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tap"",
                    ""type"": ""Value"",
                    ""id"": ""b7d0dfea-7e61-4761-a836-497cf05c647c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""38da5860-c3a4-4452-b7a5-dc625a0c750c"",
                    ""path"": ""<Gamepad>/rightStick/x"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""e5355b1f-39ef-4c6d-a27d-5bcf356d2b66"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""ef1b0302-cb75-477d-95a2-1f8be034fdf1"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f9f30591-4b95-4b95-a99d-7e7b5a0b39c1"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HorizontalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0eedeee7-12a6-43cc-8b26-b98282ae10c7"",
                    ""path"": ""<Gamepad>/rightStick/y"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone"",
                    ""groups"": """",
                    ""action"": ""VerticalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""85ef5a22-d686-44be-acef-bc63c9c90cbd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMovement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""86337789-af6e-4e07-856d-a0b66bc1c7ca"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0200a15f-5241-4a7a-9572-fd6a5d91f759"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""VerticalMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""91bc4016-3b05-4f64-991d-f468db222b53"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e8286f7-c946-4227-ba9a-2eeb14c70dd1"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone(min=0.9)"",
                    ""groups"": """",
                    ""action"": ""SelectLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8243ac7f-1b18-40ac-ad62-7c65a80554c0"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bd171bd5-503f-41eb-9ddc-1eac11ab4a4e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone(min=0.9)"",
                    ""groups"": """",
                    ""action"": ""SelectRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffa8e996-cbf6-4c29-8047-ab5ea597c2fc"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c6abec59-4081-475c-bfe0-fd45bdb4dded"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone(min=0.9)"",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53133cea-56df-4dfd-bead-1725b0d50780"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f09a615-7bc7-45bb-9fc9-14e6e4332c00"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": ""Press"",
                    ""processors"": ""AxisDeadzone(min=0.9)"",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""SwipeUp"",
                    ""id"": ""0dd22a9f-9c48-457b-af52-093b60a2d763"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b5a9229d-d71d-408d-8a2f-c03dba49497f"",
                    ""path"": ""<Touchscreen>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""AxisDeadzone(min=20,max=50)"",
                    ""groups"": """",
                    ""action"": ""ZoomIn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""SwipeDown"",
                    ""id"": ""d7b94bd9-6906-4154-a7b3-1bcb7fb4facc"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f27b491d-2e23-470d-8108-b53702dcc2aa"",
                    ""path"": ""<Touchscreen>/delta/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert,AxisDeadzone(min=20,max=50)"",
                    ""groups"": """",
                    ""action"": ""ZoomOut"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""2cad8674-651e-4f79-943a-a423904d9aa7"",
                    ""path"": ""<Touchscreen>/primaryTouch/startPosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_MonsterAdd = m_Debug.FindAction("MonsterAdd", throwIfNotFound: true);
        m_Debug_Anim1 = m_Debug.FindAction("Anim1", throwIfNotFound: true);
        m_Debug_Anim2 = m_Debug.FindAction("Anim2", throwIfNotFound: true);
        m_Debug_Anim3 = m_Debug.FindAction("Anim3", throwIfNotFound: true);
        m_Debug_Anim4 = m_Debug.FindAction("Anim4", throwIfNotFound: true);
        m_Debug_Anim5 = m_Debug.FindAction("Anim5", throwIfNotFound: true);
        m_Debug_Anim6 = m_Debug.FindAction("Anim6", throwIfNotFound: true);
        m_Debug_Anim7 = m_Debug.FindAction("Anim7", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Quit = m_UI.FindAction("Quit", throwIfNotFound: true);
        m_UI_ControlToggle = m_UI.FindAction("ControlToggle", throwIfNotFound: true);
        m_UI_FeedToggle = m_UI.FindAction("FeedToggle", throwIfNotFound: true);
        m_UI_TimelineToggle = m_UI.FindAction("TimelineToggle", throwIfNotFound: true);
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_HorizontalMovement = m_Player.FindAction("HorizontalMovement", throwIfNotFound: true);
        m_Player_VerticalMovement = m_Player.FindAction("VerticalMovement", throwIfNotFound: true);
        m_Player_SelectLeft = m_Player.FindAction("SelectLeft", throwIfNotFound: true);
        m_Player_SelectRight = m_Player.FindAction("SelectRight", throwIfNotFound: true);
        m_Player_ZoomIn = m_Player.FindAction("ZoomIn", throwIfNotFound: true);
        m_Player_ZoomOut = m_Player.FindAction("ZoomOut", throwIfNotFound: true);
        m_Player_Tap = m_Player.FindAction("Tap", throwIfNotFound: true);
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

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_MonsterAdd;
    private readonly InputAction m_Debug_Anim1;
    private readonly InputAction m_Debug_Anim2;
    private readonly InputAction m_Debug_Anim3;
    private readonly InputAction m_Debug_Anim4;
    private readonly InputAction m_Debug_Anim5;
    private readonly InputAction m_Debug_Anim6;
    private readonly InputAction m_Debug_Anim7;
    public struct DebugActions
    {
        private @TallyInputSystem m_Wrapper;
        public DebugActions(@TallyInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @MonsterAdd => m_Wrapper.m_Debug_MonsterAdd;
        public InputAction @Anim1 => m_Wrapper.m_Debug_Anim1;
        public InputAction @Anim2 => m_Wrapper.m_Debug_Anim2;
        public InputAction @Anim3 => m_Wrapper.m_Debug_Anim3;
        public InputAction @Anim4 => m_Wrapper.m_Debug_Anim4;
        public InputAction @Anim5 => m_Wrapper.m_Debug_Anim5;
        public InputAction @Anim6 => m_Wrapper.m_Debug_Anim6;
        public InputAction @Anim7 => m_Wrapper.m_Debug_Anim7;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @MonsterAdd.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnMonsterAdd;
                @MonsterAdd.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnMonsterAdd;
                @MonsterAdd.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnMonsterAdd;
                @Anim1.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim1;
                @Anim1.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim1;
                @Anim1.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim1;
                @Anim2.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim2;
                @Anim2.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim2;
                @Anim2.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim2;
                @Anim3.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim3;
                @Anim3.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim3;
                @Anim3.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim3;
                @Anim4.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim4;
                @Anim4.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim4;
                @Anim4.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim4;
                @Anim5.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim5;
                @Anim5.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim5;
                @Anim5.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim5;
                @Anim6.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim6;
                @Anim6.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim6;
                @Anim6.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim6;
                @Anim7.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim7;
                @Anim7.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim7;
                @Anim7.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAnim7;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MonsterAdd.started += instance.OnMonsterAdd;
                @MonsterAdd.performed += instance.OnMonsterAdd;
                @MonsterAdd.canceled += instance.OnMonsterAdd;
                @Anim1.started += instance.OnAnim1;
                @Anim1.performed += instance.OnAnim1;
                @Anim1.canceled += instance.OnAnim1;
                @Anim2.started += instance.OnAnim2;
                @Anim2.performed += instance.OnAnim2;
                @Anim2.canceled += instance.OnAnim2;
                @Anim3.started += instance.OnAnim3;
                @Anim3.performed += instance.OnAnim3;
                @Anim3.canceled += instance.OnAnim3;
                @Anim4.started += instance.OnAnim4;
                @Anim4.performed += instance.OnAnim4;
                @Anim4.canceled += instance.OnAnim4;
                @Anim5.started += instance.OnAnim5;
                @Anim5.performed += instance.OnAnim5;
                @Anim5.canceled += instance.OnAnim5;
                @Anim6.started += instance.OnAnim6;
                @Anim6.performed += instance.OnAnim6;
                @Anim6.canceled += instance.OnAnim6;
                @Anim7.started += instance.OnAnim7;
                @Anim7.performed += instance.OnAnim7;
                @Anim7.canceled += instance.OnAnim7;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Quit;
    private readonly InputAction m_UI_ControlToggle;
    private readonly InputAction m_UI_FeedToggle;
    private readonly InputAction m_UI_TimelineToggle;
    public struct UIActions
    {
        private @TallyInputSystem m_Wrapper;
        public UIActions(@TallyInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @Quit => m_Wrapper.m_UI_Quit;
        public InputAction @ControlToggle => m_Wrapper.m_UI_ControlToggle;
        public InputAction @FeedToggle => m_Wrapper.m_UI_FeedToggle;
        public InputAction @TimelineToggle => m_Wrapper.m_UI_TimelineToggle;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Quit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnQuit;
                @ControlToggle.started -= m_Wrapper.m_UIActionsCallbackInterface.OnControlToggle;
                @ControlToggle.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnControlToggle;
                @ControlToggle.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnControlToggle;
                @FeedToggle.started -= m_Wrapper.m_UIActionsCallbackInterface.OnFeedToggle;
                @FeedToggle.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnFeedToggle;
                @FeedToggle.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnFeedToggle;
                @TimelineToggle.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTimelineToggle;
                @TimelineToggle.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTimelineToggle;
                @TimelineToggle.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTimelineToggle;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
                @ControlToggle.started += instance.OnControlToggle;
                @ControlToggle.performed += instance.OnControlToggle;
                @ControlToggle.canceled += instance.OnControlToggle;
                @FeedToggle.started += instance.OnFeedToggle;
                @FeedToggle.performed += instance.OnFeedToggle;
                @FeedToggle.canceled += instance.OnFeedToggle;
                @TimelineToggle.started += instance.OnTimelineToggle;
                @TimelineToggle.performed += instance.OnTimelineToggle;
                @TimelineToggle.canceled += instance.OnTimelineToggle;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_HorizontalMovement;
    private readonly InputAction m_Player_VerticalMovement;
    private readonly InputAction m_Player_SelectLeft;
    private readonly InputAction m_Player_SelectRight;
    private readonly InputAction m_Player_ZoomIn;
    private readonly InputAction m_Player_ZoomOut;
    private readonly InputAction m_Player_Tap;
    public struct PlayerActions
    {
        private @TallyInputSystem m_Wrapper;
        public PlayerActions(@TallyInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @HorizontalMovement => m_Wrapper.m_Player_HorizontalMovement;
        public InputAction @VerticalMovement => m_Wrapper.m_Player_VerticalMovement;
        public InputAction @SelectLeft => m_Wrapper.m_Player_SelectLeft;
        public InputAction @SelectRight => m_Wrapper.m_Player_SelectRight;
        public InputAction @ZoomIn => m_Wrapper.m_Player_ZoomIn;
        public InputAction @ZoomOut => m_Wrapper.m_Player_ZoomOut;
        public InputAction @Tap => m_Wrapper.m_Player_Tap;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @HorizontalMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMovement;
                @HorizontalMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnHorizontalMovement;
                @VerticalMovement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalMovement;
                @VerticalMovement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalMovement;
                @VerticalMovement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnVerticalMovement;
                @SelectLeft.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectLeft;
                @SelectLeft.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectLeft;
                @SelectRight.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectRight;
                @SelectRight.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectRight;
                @SelectRight.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSelectRight;
                @ZoomIn.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomIn;
                @ZoomIn.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomIn;
                @ZoomIn.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomIn;
                @ZoomOut.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomOut;
                @ZoomOut.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomOut;
                @ZoomOut.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnZoomOut;
                @Tap.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTap;
                @Tap.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTap;
                @Tap.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTap;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @HorizontalMovement.started += instance.OnHorizontalMovement;
                @HorizontalMovement.performed += instance.OnHorizontalMovement;
                @HorizontalMovement.canceled += instance.OnHorizontalMovement;
                @VerticalMovement.started += instance.OnVerticalMovement;
                @VerticalMovement.performed += instance.OnVerticalMovement;
                @VerticalMovement.canceled += instance.OnVerticalMovement;
                @SelectLeft.started += instance.OnSelectLeft;
                @SelectLeft.performed += instance.OnSelectLeft;
                @SelectLeft.canceled += instance.OnSelectLeft;
                @SelectRight.started += instance.OnSelectRight;
                @SelectRight.performed += instance.OnSelectRight;
                @SelectRight.canceled += instance.OnSelectRight;
                @ZoomIn.started += instance.OnZoomIn;
                @ZoomIn.performed += instance.OnZoomIn;
                @ZoomIn.canceled += instance.OnZoomIn;
                @ZoomOut.started += instance.OnZoomOut;
                @ZoomOut.performed += instance.OnZoomOut;
                @ZoomOut.canceled += instance.OnZoomOut;
                @Tap.started += instance.OnTap;
                @Tap.performed += instance.OnTap;
                @Tap.canceled += instance.OnTap;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    public interface IDebugActions
    {
        void OnMonsterAdd(InputAction.CallbackContext context);
        void OnAnim1(InputAction.CallbackContext context);
        void OnAnim2(InputAction.CallbackContext context);
        void OnAnim3(InputAction.CallbackContext context);
        void OnAnim4(InputAction.CallbackContext context);
        void OnAnim5(InputAction.CallbackContext context);
        void OnAnim6(InputAction.CallbackContext context);
        void OnAnim7(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnQuit(InputAction.CallbackContext context);
        void OnControlToggle(InputAction.CallbackContext context);
        void OnFeedToggle(InputAction.CallbackContext context);
        void OnTimelineToggle(InputAction.CallbackContext context);
    }
    public interface IPlayerActions
    {
        void OnHorizontalMovement(InputAction.CallbackContext context);
        void OnVerticalMovement(InputAction.CallbackContext context);
        void OnSelectLeft(InputAction.CallbackContext context);
        void OnSelectRight(InputAction.CallbackContext context);
        void OnZoomIn(InputAction.CallbackContext context);
        void OnZoomOut(InputAction.CallbackContext context);
        void OnTap(InputAction.CallbackContext context);
    }
}
