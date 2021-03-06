// GENERATED AUTOMATICALLY FROM 'Assets/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

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
            ""id"": ""dbb87051-dbe3-47fc-b00e-19794a1b5cd0"",
            ""actions"": [
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""7d83664f-3a57-4de5-ad8f-c6c4efde0322"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""67be2cf7-d16b-445f-8831-7a9f583e53c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""39d0d76a-51f6-4ef4-bf7c-2cf5eaf01b55"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tileflip"",
                    ""type"": ""Button"",
                    ""id"": ""cf386e15-82f4-4873-96a3-2af6420bce84"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""d59ab3a3-cfa8-4449-b6fa-855a75e0b479"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeSceneHax"",
                    ""type"": ""Button"",
                    ""id"": ""c499f455-7836-4999-b507-5cbfd2ea5c5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""aa3dd6c3-7b3b-44dd-a0c1-eddd6fe6491d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HPHAX"",
                    ""type"": ""Button"",
                    ""id"": ""01924b1d-0cfe-40bc-b065-61c6c78266c7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GAIAHAX"",
                    ""type"": ""Button"",
                    ""id"": ""d0005f8f-e3b3-4640-abd5-7fa426550313"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MONEYHAX"",
                    ""type"": ""Button"",
                    ""id"": ""75e50c6c-ae7c-462e-99e1-81b1022c9c2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""BERRYHAX"",
                    ""type"": ""Button"",
                    ""id"": ""411853fc-43cf-4887-a7d1-bbda6803d75c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Options"",
                    ""type"": ""Button"",
                    ""id"": ""5f2de234-69f6-4c89-bedc-58f0bd738bd7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""WATERHAX"",
                    ""type"": ""Button"",
                    ""id"": ""b71a9375-e588-4e84-b67a-8faf3447d2a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SAVEHAX"",
                    ""type"": ""Button"",
                    ""id"": ""9d0de407-83f6-4097-8c35-127253ba34ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LOADHAX"",
                    ""type"": ""Button"",
                    ""id"": ""7b093b5e-d4e9-4ce1-8bdd-1c42da888241"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SAVEMENUHAX"",
                    ""type"": ""Button"",
                    ""id"": ""578b2dc8-c214-4b61-a403-4de5d41db59b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RESETSAVEFILEHAX"",
                    ""type"": ""Button"",
                    ""id"": ""12a8c643-8004-41c9-8851-f93d3845abd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SPEEDHAX"",
                    ""type"": ""Button"",
                    ""id"": ""6e32fec6-e5aa-4205-9b4a-1284b53a62c9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b693a68f-0d58-4e9f-ac36-65c0be4ba3d5"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""04a99bcc-ea09-41b1-81e7-0b394e5d1aff"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a66687e0-1852-4057-80d0-6f77bca86a09"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChangeSceneHax"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1f978b61-f8e9-433d-b908-3223582e6456"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2ce1e43b-c1ac-4780-a52f-3a38d0356602"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4389650-9f03-4851-9caa-360eb7d061da"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Tileflip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d22cad6-e98a-45e9-baeb-9777d5c60f40"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Tileflip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b1d787e1-2420-4689-8d66-88622382d9cf"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8c303e4-f9c4-4e80-b43f-2c080d4c3c87"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b3701ef5-4d03-4164-8487-799e919318b2"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4734d97d-3ae5-4f85-8fa1-cb4d3531071e"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""dd0a58b9-13ae-411e-b6ae-16256f43e945"",
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
                    ""id"": ""6209aa52-b3bc-4061-a944-b3ad01bc1b9d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""111dd482-7f07-4de0-82c9-357a55fb37a8"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""5076b460-b38b-4b0d-980f-eefba565b2ce"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""233c1362-1465-4cbe-8240-ba858f1a5e74"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8ad8e50b-75fd-46cd-b16a-23c24b7b2359"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e592ffe0-dbeb-4e18-806b-603621814fc4"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""16c6f845-3fae-473f-8776-6397055f370c"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fa64f859-3761-452f-a101-a73fc61ac28c"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""c6e624d8-b73e-405a-addf-dc95c50faa3c"",
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
                    ""id"": ""cc3c6df4-1c6d-44d6-b0c0-5f9587c561ee"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""734bbe8b-fbc4-436a-af57-884485786f56"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8211f9a3-4352-4d26-8a67-ccc52d3fca78"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""efc481a0-d6e2-4888-a522-f86fcb22183a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4913f324-d325-498c-a116-ecf40cc27597"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HPHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""163726fe-0fc7-4a95-ad00-69529d501f14"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GAIAHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c38fb34-52ff-4c5f-bc83-32790b9175ba"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MONEYHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2542c2fb-f924-485c-9f41-8fc979ed49f8"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BERRYHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""893e1d1d-9fe4-48aa-a5ca-15ae242c8968"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Options"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08708d82-1362-4f33-be7e-42eccbde3016"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""WATERHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fd759f2-2600-4554-b8db-c01f8bdae264"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SAVEHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ee87993-88f7-4b03-9571-1b9b027ef2ca"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LOADHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9921bad9-d048-41a6-9653-03a627ca49c4"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SAVEMENUHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e34b9f8d-192f-4ad1-8480-d8d4b06f39b3"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RESETSAVEFILEHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc5ffe97-181a-4b10-8f6c-929e25bbddd2"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SPEEDHAX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""GenericUI"",
            ""id"": ""cbfb5d60-caf1-4a95-b2e9-3f8a0d2ebd34"",
            ""actions"": [
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""34ecaca0-613f-4d3e-a6e0-58b33c08a05f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""8deae492-926d-4798-a76c-b374a116c94a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Options"",
                    ""type"": ""Button"",
                    ""id"": ""a5d4d231-3841-433c-880b-ff41ddd3b607"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cbae3722-56d2-48e7-81a7-93373577903c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb0a7678-e6ff-4787-9b5c-2f2149816eb0"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7adbc4b8-edc7-4240-977b-e3698ac73f74"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e60c5b0-2c10-4ef4-a0dc-a9f14a38fd5c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7dceb620-e652-4709-9dda-f970c876f2f4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Options"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Battle"",
            ""id"": ""291d1f47-3d3b-4ceb-9aa6-3a94e07dda92"",
            ""actions"": [
                {
                    ""name"": ""Pass"",
                    ""type"": ""Button"",
                    ""id"": ""ba40d4f7-efa7-41a9-b8ec-9e65c2726dce"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockDice"",
                    ""type"": ""Button"",
                    ""id"": ""6aaed11d-12a4-405f-aaee-6ad513c908b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dodge"",
                    ""type"": ""Button"",
                    ""id"": ""dcef6587-d4ce-4b72-bb78-3ee3777584b5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Block"",
                    ""type"": ""Button"",
                    ""id"": ""f63e9544-a837-4483-946a-5c8691799a2a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ChangeSceneHax"",
                    ""type"": ""Button"",
                    ""id"": ""606c74eb-c141-4bca-a372-786b16de012a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""d48bd97a-7ce9-440c-81e9-89b4f4cd9533"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""KILLALL"",
                    ""type"": ""Button"",
                    ""id"": ""8c5d3a04-0135-47b0-857e-28f3b0134013"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a94a3d12-5ae1-44e0-b693-61a08d61c1de"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30561b8b-5054-4d36-add9-53fd1848bbf9"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Pass"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5e776335-819f-4303-9e67-0096cf070824"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LockDice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""575946ad-766a-42cc-9ecc-557311b0212c"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LockDice"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d813bdba-03ea-47f5-b826-ace8cebde516"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a99d2169-d0c0-4b32-b6f3-2c94ce5111c2"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dodge"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c22f97d6-1818-4ced-9ffc-af334c4788c4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0a4e4d3d-30be-443d-b5d1-a58c4459ae89"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62c16ff2-1e22-4826-bc74-7bb290a99c82"",
                    ""path"": ""<Keyboard>/f1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ChangeSceneHax"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0802b018-70a5-4fa7-8011-f1c679d24ce5"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""90df7428-77c6-41bb-87b7-af83d3fd453d"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c1acbe1-1024-4a41-aec8-79369bf350ae"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KILLALL"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""InventoryUI"",
            ""id"": ""ea8fd91e-d433-4f37-b95e-580c9402cc44"",
            ""actions"": [
                {
                    ""name"": ""InventoryLeft"",
                    ""type"": ""Button"",
                    ""id"": ""847097da-87a1-469f-a2e9-b86ea6ba380d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""InventoryRight"",
                    ""type"": ""Button"",
                    ""id"": ""f70adcf9-8287-4730-a9bb-42e2c4531d31"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""7859cbce-8fd9-4868-9862-1a44ba2930a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""9c81152d-f4c9-44cf-8647-39e33548ca48"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""fad0c178-7919-4954-bb4c-ba03f10d71bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ClearAbility"",
                    ""type"": ""Button"",
                    ""id"": ""9ccf84fa-1ce2-496c-8448-8486282836b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""31b3fa33-d014-4c01-9ae5-b2bb7528675a"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""InventoryLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44dd1512-6ec0-4697-beae-04f331b03c42"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""InventoryLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb191548-a59d-4482-a822-f336f761083e"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""InventoryRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""95e6f6bb-80be-4286-974c-c99e012f118a"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""InventoryRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2d3f4946-7f90-4ba0-b607-505bc9c25a8a"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0f799f6e-40ed-4b11-9d95-4bd4b18a898f"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""1547a470-b47f-4296-bb07-e9736ffd3d78"",
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
                    ""id"": ""d1b5c82c-edc6-4d6d-943b-e85f7dc1ec5b"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""b54e2dd3-322b-4c16-815e-dc515c33346a"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ea2e26c2-ed5b-4eb8-917a-bb8a601a9426"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ea35e05e-8315-40bb-a0f8-237371d2e067"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""a5d2dc84-7737-4e6e-9555-0098e8aff8f7"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d57ca003-171f-4f76-93f2-0e06754648cd"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62a80d1b-373a-4c95-9932-49da5bbf83ea"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""64e638d8-fb1c-4df0-a655-dcb2f0155455"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""c516ebda-de43-42e9-960a-19670dc5a93d"",
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
                    ""id"": ""4afec230-3553-4eb5-a5b8-6464d7a39ab2"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""20b2207c-f52b-4fa5-ae5d-e7fe9a53af49"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2ed44036-8a2e-470c-8a5c-281aca68f8e4"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""3f02ab37-53ff-4329-bad7-cc057f08b311"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""61bc99e9-1ff5-4972-9d05-4b87568d3f5c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d88d468f-2c94-41b7-8f4d-2b12f4386456"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1ae839f8-881e-4101-94aa-b9ed5875aca1"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""ClearAbility"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""139db0ee-997c-4d55-950a-24872662432e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ClearAbility"",
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
            ""devices"": []
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": []
        }
    ]
}");
        // Overworld
        m_Overworld = asset.FindActionMap("Overworld", throwIfNotFound: true);
        m_Overworld_Interact = m_Overworld.FindAction("Interact", throwIfNotFound: true);
        m_Overworld_Cancel = m_Overworld.FindAction("Cancel", throwIfNotFound: true);
        m_Overworld_Inventory = m_Overworld.FindAction("Inventory", throwIfNotFound: true);
        m_Overworld_Tileflip = m_Overworld.FindAction("Tileflip", throwIfNotFound: true);
        m_Overworld_Submit = m_Overworld.FindAction("Submit", throwIfNotFound: true);
        m_Overworld_ChangeSceneHax = m_Overworld.FindAction("ChangeSceneHax", throwIfNotFound: true);
        m_Overworld_Move = m_Overworld.FindAction("Move", throwIfNotFound: true);
        m_Overworld_HPHAX = m_Overworld.FindAction("HPHAX", throwIfNotFound: true);
        m_Overworld_GAIAHAX = m_Overworld.FindAction("GAIAHAX", throwIfNotFound: true);
        m_Overworld_MONEYHAX = m_Overworld.FindAction("MONEYHAX", throwIfNotFound: true);
        m_Overworld_BERRYHAX = m_Overworld.FindAction("BERRYHAX", throwIfNotFound: true);
        m_Overworld_Options = m_Overworld.FindAction("Options", throwIfNotFound: true);
        m_Overworld_WATERHAX = m_Overworld.FindAction("WATERHAX", throwIfNotFound: true);
        m_Overworld_SAVEHAX = m_Overworld.FindAction("SAVEHAX", throwIfNotFound: true);
        m_Overworld_LOADHAX = m_Overworld.FindAction("LOADHAX", throwIfNotFound: true);
        m_Overworld_SAVEMENUHAX = m_Overworld.FindAction("SAVEMENUHAX", throwIfNotFound: true);
        m_Overworld_RESETSAVEFILEHAX = m_Overworld.FindAction("RESETSAVEFILEHAX", throwIfNotFound: true);
        m_Overworld_SPEEDHAX = m_Overworld.FindAction("SPEEDHAX", throwIfNotFound: true);
        // GenericUI
        m_GenericUI = asset.FindActionMap("GenericUI", throwIfNotFound: true);
        m_GenericUI_Submit = m_GenericUI.FindAction("Submit", throwIfNotFound: true);
        m_GenericUI_Cancel = m_GenericUI.FindAction("Cancel", throwIfNotFound: true);
        m_GenericUI_Options = m_GenericUI.FindAction("Options", throwIfNotFound: true);
        // Battle
        m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
        m_Battle_Pass = m_Battle.FindAction("Pass", throwIfNotFound: true);
        m_Battle_LockDice = m_Battle.FindAction("LockDice", throwIfNotFound: true);
        m_Battle_Dodge = m_Battle.FindAction("Dodge", throwIfNotFound: true);
        m_Battle_Block = m_Battle.FindAction("Block", throwIfNotFound: true);
        m_Battle_ChangeSceneHax = m_Battle.FindAction("ChangeSceneHax", throwIfNotFound: true);
        m_Battle_Cancel = m_Battle.FindAction("Cancel", throwIfNotFound: true);
        m_Battle_KILLALL = m_Battle.FindAction("KILLALL", throwIfNotFound: true);
        // InventoryUI
        m_InventoryUI = asset.FindActionMap("InventoryUI", throwIfNotFound: true);
        m_InventoryUI_InventoryLeft = m_InventoryUI.FindAction("InventoryLeft", throwIfNotFound: true);
        m_InventoryUI_InventoryRight = m_InventoryUI.FindAction("InventoryRight", throwIfNotFound: true);
        m_InventoryUI_Inventory = m_InventoryUI.FindAction("Inventory", throwIfNotFound: true);
        m_InventoryUI_Move = m_InventoryUI.FindAction("Move", throwIfNotFound: true);
        m_InventoryUI_Submit = m_InventoryUI.FindAction("Submit", throwIfNotFound: true);
        m_InventoryUI_ClearAbility = m_InventoryUI.FindAction("ClearAbility", throwIfNotFound: true);
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
    private readonly InputAction m_Overworld_Interact;
    private readonly InputAction m_Overworld_Cancel;
    private readonly InputAction m_Overworld_Inventory;
    private readonly InputAction m_Overworld_Tileflip;
    private readonly InputAction m_Overworld_Submit;
    private readonly InputAction m_Overworld_ChangeSceneHax;
    private readonly InputAction m_Overworld_Move;
    private readonly InputAction m_Overworld_HPHAX;
    private readonly InputAction m_Overworld_GAIAHAX;
    private readonly InputAction m_Overworld_MONEYHAX;
    private readonly InputAction m_Overworld_BERRYHAX;
    private readonly InputAction m_Overworld_Options;
    private readonly InputAction m_Overworld_WATERHAX;
    private readonly InputAction m_Overworld_SAVEHAX;
    private readonly InputAction m_Overworld_LOADHAX;
    private readonly InputAction m_Overworld_SAVEMENUHAX;
    private readonly InputAction m_Overworld_RESETSAVEFILEHAX;
    private readonly InputAction m_Overworld_SPEEDHAX;
    public struct OverworldActions
    {
        private @PlayerControls m_Wrapper;
        public OverworldActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Interact => m_Wrapper.m_Overworld_Interact;
        public InputAction @Cancel => m_Wrapper.m_Overworld_Cancel;
        public InputAction @Inventory => m_Wrapper.m_Overworld_Inventory;
        public InputAction @Tileflip => m_Wrapper.m_Overworld_Tileflip;
        public InputAction @Submit => m_Wrapper.m_Overworld_Submit;
        public InputAction @ChangeSceneHax => m_Wrapper.m_Overworld_ChangeSceneHax;
        public InputAction @Move => m_Wrapper.m_Overworld_Move;
        public InputAction @HPHAX => m_Wrapper.m_Overworld_HPHAX;
        public InputAction @GAIAHAX => m_Wrapper.m_Overworld_GAIAHAX;
        public InputAction @MONEYHAX => m_Wrapper.m_Overworld_MONEYHAX;
        public InputAction @BERRYHAX => m_Wrapper.m_Overworld_BERRYHAX;
        public InputAction @Options => m_Wrapper.m_Overworld_Options;
        public InputAction @WATERHAX => m_Wrapper.m_Overworld_WATERHAX;
        public InputAction @SAVEHAX => m_Wrapper.m_Overworld_SAVEHAX;
        public InputAction @LOADHAX => m_Wrapper.m_Overworld_LOADHAX;
        public InputAction @SAVEMENUHAX => m_Wrapper.m_Overworld_SAVEMENUHAX;
        public InputAction @RESETSAVEFILEHAX => m_Wrapper.m_Overworld_RESETSAVEFILEHAX;
        public InputAction @SPEEDHAX => m_Wrapper.m_Overworld_SPEEDHAX;
        public InputActionMap Get() { return m_Wrapper.m_Overworld; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(OverworldActions set) { return set.Get(); }
        public void SetCallbacks(IOverworldActions instance)
        {
            if (m_Wrapper.m_OverworldActionsCallbackInterface != null)
            {
                @Interact.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInteract;
                @Cancel.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnCancel;
                @Inventory.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnInventory;
                @Tileflip.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnTileflip;
                @Tileflip.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnTileflip;
                @Tileflip.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnTileflip;
                @Submit.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSubmit;
                @ChangeSceneHax.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnChangeSceneHax;
                @ChangeSceneHax.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnChangeSceneHax;
                @ChangeSceneHax.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnChangeSceneHax;
                @Move.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMove;
                @HPHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnHPHAX;
                @HPHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnHPHAX;
                @HPHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnHPHAX;
                @GAIAHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnGAIAHAX;
                @GAIAHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnGAIAHAX;
                @GAIAHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnGAIAHAX;
                @MONEYHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMONEYHAX;
                @MONEYHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMONEYHAX;
                @MONEYHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnMONEYHAX;
                @BERRYHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnBERRYHAX;
                @BERRYHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnBERRYHAX;
                @BERRYHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnBERRYHAX;
                @Options.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOptions;
                @Options.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOptions;
                @Options.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnOptions;
                @WATERHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnWATERHAX;
                @WATERHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnWATERHAX;
                @WATERHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnWATERHAX;
                @SAVEHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSAVEHAX;
                @SAVEHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSAVEHAX;
                @SAVEHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSAVEHAX;
                @LOADHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnLOADHAX;
                @LOADHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnLOADHAX;
                @LOADHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnLOADHAX;
                @SAVEMENUHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSAVEMENUHAX;
                @SAVEMENUHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSAVEMENUHAX;
                @SAVEMENUHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSAVEMENUHAX;
                @RESETSAVEFILEHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRESETSAVEFILEHAX;
                @RESETSAVEFILEHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRESETSAVEFILEHAX;
                @RESETSAVEFILEHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnRESETSAVEFILEHAX;
                @SPEEDHAX.started -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSPEEDHAX;
                @SPEEDHAX.performed -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSPEEDHAX;
                @SPEEDHAX.canceled -= m_Wrapper.m_OverworldActionsCallbackInterface.OnSPEEDHAX;
            }
            m_Wrapper.m_OverworldActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Tileflip.started += instance.OnTileflip;
                @Tileflip.performed += instance.OnTileflip;
                @Tileflip.canceled += instance.OnTileflip;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @ChangeSceneHax.started += instance.OnChangeSceneHax;
                @ChangeSceneHax.performed += instance.OnChangeSceneHax;
                @ChangeSceneHax.canceled += instance.OnChangeSceneHax;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @HPHAX.started += instance.OnHPHAX;
                @HPHAX.performed += instance.OnHPHAX;
                @HPHAX.canceled += instance.OnHPHAX;
                @GAIAHAX.started += instance.OnGAIAHAX;
                @GAIAHAX.performed += instance.OnGAIAHAX;
                @GAIAHAX.canceled += instance.OnGAIAHAX;
                @MONEYHAX.started += instance.OnMONEYHAX;
                @MONEYHAX.performed += instance.OnMONEYHAX;
                @MONEYHAX.canceled += instance.OnMONEYHAX;
                @BERRYHAX.started += instance.OnBERRYHAX;
                @BERRYHAX.performed += instance.OnBERRYHAX;
                @BERRYHAX.canceled += instance.OnBERRYHAX;
                @Options.started += instance.OnOptions;
                @Options.performed += instance.OnOptions;
                @Options.canceled += instance.OnOptions;
                @WATERHAX.started += instance.OnWATERHAX;
                @WATERHAX.performed += instance.OnWATERHAX;
                @WATERHAX.canceled += instance.OnWATERHAX;
                @SAVEHAX.started += instance.OnSAVEHAX;
                @SAVEHAX.performed += instance.OnSAVEHAX;
                @SAVEHAX.canceled += instance.OnSAVEHAX;
                @LOADHAX.started += instance.OnLOADHAX;
                @LOADHAX.performed += instance.OnLOADHAX;
                @LOADHAX.canceled += instance.OnLOADHAX;
                @SAVEMENUHAX.started += instance.OnSAVEMENUHAX;
                @SAVEMENUHAX.performed += instance.OnSAVEMENUHAX;
                @SAVEMENUHAX.canceled += instance.OnSAVEMENUHAX;
                @RESETSAVEFILEHAX.started += instance.OnRESETSAVEFILEHAX;
                @RESETSAVEFILEHAX.performed += instance.OnRESETSAVEFILEHAX;
                @RESETSAVEFILEHAX.canceled += instance.OnRESETSAVEFILEHAX;
                @SPEEDHAX.started += instance.OnSPEEDHAX;
                @SPEEDHAX.performed += instance.OnSPEEDHAX;
                @SPEEDHAX.canceled += instance.OnSPEEDHAX;
            }
        }
    }
    public OverworldActions @Overworld => new OverworldActions(this);

    // GenericUI
    private readonly InputActionMap m_GenericUI;
    private IGenericUIActions m_GenericUIActionsCallbackInterface;
    private readonly InputAction m_GenericUI_Submit;
    private readonly InputAction m_GenericUI_Cancel;
    private readonly InputAction m_GenericUI_Options;
    public struct GenericUIActions
    {
        private @PlayerControls m_Wrapper;
        public GenericUIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Submit => m_Wrapper.m_GenericUI_Submit;
        public InputAction @Cancel => m_Wrapper.m_GenericUI_Cancel;
        public InputAction @Options => m_Wrapper.m_GenericUI_Options;
        public InputActionMap Get() { return m_Wrapper.m_GenericUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GenericUIActions set) { return set.Get(); }
        public void SetCallbacks(IGenericUIActions instance)
        {
            if (m_Wrapper.m_GenericUIActionsCallbackInterface != null)
            {
                @Submit.started -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnCancel;
                @Options.started -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnOptions;
                @Options.performed -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnOptions;
                @Options.canceled -= m_Wrapper.m_GenericUIActionsCallbackInterface.OnOptions;
            }
            m_Wrapper.m_GenericUIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Options.started += instance.OnOptions;
                @Options.performed += instance.OnOptions;
                @Options.canceled += instance.OnOptions;
            }
        }
    }
    public GenericUIActions @GenericUI => new GenericUIActions(this);

    // Battle
    private readonly InputActionMap m_Battle;
    private IBattleActions m_BattleActionsCallbackInterface;
    private readonly InputAction m_Battle_Pass;
    private readonly InputAction m_Battle_LockDice;
    private readonly InputAction m_Battle_Dodge;
    private readonly InputAction m_Battle_Block;
    private readonly InputAction m_Battle_ChangeSceneHax;
    private readonly InputAction m_Battle_Cancel;
    private readonly InputAction m_Battle_KILLALL;
    public struct BattleActions
    {
        private @PlayerControls m_Wrapper;
        public BattleActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pass => m_Wrapper.m_Battle_Pass;
        public InputAction @LockDice => m_Wrapper.m_Battle_LockDice;
        public InputAction @Dodge => m_Wrapper.m_Battle_Dodge;
        public InputAction @Block => m_Wrapper.m_Battle_Block;
        public InputAction @ChangeSceneHax => m_Wrapper.m_Battle_ChangeSceneHax;
        public InputAction @Cancel => m_Wrapper.m_Battle_Cancel;
        public InputAction @KILLALL => m_Wrapper.m_Battle_KILLALL;
        public InputActionMap Get() { return m_Wrapper.m_Battle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BattleActions set) { return set.Get(); }
        public void SetCallbacks(IBattleActions instance)
        {
            if (m_Wrapper.m_BattleActionsCallbackInterface != null)
            {
                @Pass.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnPass;
                @Pass.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnPass;
                @Pass.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnPass;
                @LockDice.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnLockDice;
                @LockDice.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnLockDice;
                @LockDice.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnLockDice;
                @Dodge.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnDodge;
                @Dodge.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnDodge;
                @Dodge.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnDodge;
                @Block.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnBlock;
                @Block.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnBlock;
                @Block.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnBlock;
                @ChangeSceneHax.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnChangeSceneHax;
                @ChangeSceneHax.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnChangeSceneHax;
                @ChangeSceneHax.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnChangeSceneHax;
                @Cancel.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnCancel;
                @KILLALL.started -= m_Wrapper.m_BattleActionsCallbackInterface.OnKILLALL;
                @KILLALL.performed -= m_Wrapper.m_BattleActionsCallbackInterface.OnKILLALL;
                @KILLALL.canceled -= m_Wrapper.m_BattleActionsCallbackInterface.OnKILLALL;
            }
            m_Wrapper.m_BattleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pass.started += instance.OnPass;
                @Pass.performed += instance.OnPass;
                @Pass.canceled += instance.OnPass;
                @LockDice.started += instance.OnLockDice;
                @LockDice.performed += instance.OnLockDice;
                @LockDice.canceled += instance.OnLockDice;
                @Dodge.started += instance.OnDodge;
                @Dodge.performed += instance.OnDodge;
                @Dodge.canceled += instance.OnDodge;
                @Block.started += instance.OnBlock;
                @Block.performed += instance.OnBlock;
                @Block.canceled += instance.OnBlock;
                @ChangeSceneHax.started += instance.OnChangeSceneHax;
                @ChangeSceneHax.performed += instance.OnChangeSceneHax;
                @ChangeSceneHax.canceled += instance.OnChangeSceneHax;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @KILLALL.started += instance.OnKILLALL;
                @KILLALL.performed += instance.OnKILLALL;
                @KILLALL.canceled += instance.OnKILLALL;
            }
        }
    }
    public BattleActions @Battle => new BattleActions(this);

    // InventoryUI
    private readonly InputActionMap m_InventoryUI;
    private IInventoryUIActions m_InventoryUIActionsCallbackInterface;
    private readonly InputAction m_InventoryUI_InventoryLeft;
    private readonly InputAction m_InventoryUI_InventoryRight;
    private readonly InputAction m_InventoryUI_Inventory;
    private readonly InputAction m_InventoryUI_Move;
    private readonly InputAction m_InventoryUI_Submit;
    private readonly InputAction m_InventoryUI_ClearAbility;
    public struct InventoryUIActions
    {
        private @PlayerControls m_Wrapper;
        public InventoryUIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @InventoryLeft => m_Wrapper.m_InventoryUI_InventoryLeft;
        public InputAction @InventoryRight => m_Wrapper.m_InventoryUI_InventoryRight;
        public InputAction @Inventory => m_Wrapper.m_InventoryUI_Inventory;
        public InputAction @Move => m_Wrapper.m_InventoryUI_Move;
        public InputAction @Submit => m_Wrapper.m_InventoryUI_Submit;
        public InputAction @ClearAbility => m_Wrapper.m_InventoryUI_ClearAbility;
        public InputActionMap Get() { return m_Wrapper.m_InventoryUI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryUIActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryUIActions instance)
        {
            if (m_Wrapper.m_InventoryUIActionsCallbackInterface != null)
            {
                @InventoryLeft.started -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventoryLeft;
                @InventoryLeft.performed -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventoryLeft;
                @InventoryLeft.canceled -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventoryLeft;
                @InventoryRight.started -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventoryRight;
                @InventoryRight.performed -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventoryRight;
                @InventoryRight.canceled -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventoryRight;
                @Inventory.started -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnInventory;
                @Move.started -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnMove;
                @Submit.started -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnSubmit;
                @ClearAbility.started -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnClearAbility;
                @ClearAbility.performed -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnClearAbility;
                @ClearAbility.canceled -= m_Wrapper.m_InventoryUIActionsCallbackInterface.OnClearAbility;
            }
            m_Wrapper.m_InventoryUIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @InventoryLeft.started += instance.OnInventoryLeft;
                @InventoryLeft.performed += instance.OnInventoryLeft;
                @InventoryLeft.canceled += instance.OnInventoryLeft;
                @InventoryRight.started += instance.OnInventoryRight;
                @InventoryRight.performed += instance.OnInventoryRight;
                @InventoryRight.canceled += instance.OnInventoryRight;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @ClearAbility.started += instance.OnClearAbility;
                @ClearAbility.performed += instance.OnClearAbility;
                @ClearAbility.canceled += instance.OnClearAbility;
            }
        }
    }
    public InventoryUIActions @InventoryUI => new InventoryUIActions(this);
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
        void OnInteract(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnTileflip(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnChangeSceneHax(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnHPHAX(InputAction.CallbackContext context);
        void OnGAIAHAX(InputAction.CallbackContext context);
        void OnMONEYHAX(InputAction.CallbackContext context);
        void OnBERRYHAX(InputAction.CallbackContext context);
        void OnOptions(InputAction.CallbackContext context);
        void OnWATERHAX(InputAction.CallbackContext context);
        void OnSAVEHAX(InputAction.CallbackContext context);
        void OnLOADHAX(InputAction.CallbackContext context);
        void OnSAVEMENUHAX(InputAction.CallbackContext context);
        void OnRESETSAVEFILEHAX(InputAction.CallbackContext context);
        void OnSPEEDHAX(InputAction.CallbackContext context);
    }
    public interface IGenericUIActions
    {
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnOptions(InputAction.CallbackContext context);
    }
    public interface IBattleActions
    {
        void OnPass(InputAction.CallbackContext context);
        void OnLockDice(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnChangeSceneHax(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnKILLALL(InputAction.CallbackContext context);
    }
    public interface IInventoryUIActions
    {
        void OnInventoryLeft(InputAction.CallbackContext context);
        void OnInventoryRight(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnClearAbility(InputAction.CallbackContext context);
    }
}
