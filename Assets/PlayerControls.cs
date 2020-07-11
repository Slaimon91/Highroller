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
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b693a68f-0d58-4e9f-ac36-65c0be4ba3d5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
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
                    ""path"": ""<Keyboard>/v"",
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
                    ""path"": ""<Gamepad>/buttonNorth"",
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
        // GenericUI
        m_GenericUI = asset.FindActionMap("GenericUI", throwIfNotFound: true);
        m_GenericUI_Submit = m_GenericUI.FindAction("Submit", throwIfNotFound: true);
        m_GenericUI_Cancel = m_GenericUI.FindAction("Cancel", throwIfNotFound: true);
        // Battle
        m_Battle = asset.FindActionMap("Battle", throwIfNotFound: true);
        m_Battle_Pass = m_Battle.FindAction("Pass", throwIfNotFound: true);
        m_Battle_LockDice = m_Battle.FindAction("LockDice", throwIfNotFound: true);
        m_Battle_Dodge = m_Battle.FindAction("Dodge", throwIfNotFound: true);
        m_Battle_Block = m_Battle.FindAction("Block", throwIfNotFound: true);
        m_Battle_ChangeSceneHax = m_Battle.FindAction("ChangeSceneHax", throwIfNotFound: true);
        m_Battle_Cancel = m_Battle.FindAction("Cancel", throwIfNotFound: true);
        // InventoryUI
        m_InventoryUI = asset.FindActionMap("InventoryUI", throwIfNotFound: true);
        m_InventoryUI_InventoryLeft = m_InventoryUI.FindAction("InventoryLeft", throwIfNotFound: true);
        m_InventoryUI_InventoryRight = m_InventoryUI.FindAction("InventoryRight", throwIfNotFound: true);
        m_InventoryUI_Inventory = m_InventoryUI.FindAction("Inventory", throwIfNotFound: true);
        m_InventoryUI_Move = m_InventoryUI.FindAction("Move", throwIfNotFound: true);
        m_InventoryUI_Submit = m_InventoryUI.FindAction("Submit", throwIfNotFound: true);
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
            }
        }
    }
    public OverworldActions @Overworld => new OverworldActions(this);

    // GenericUI
    private readonly InputActionMap m_GenericUI;
    private IGenericUIActions m_GenericUIActionsCallbackInterface;
    private readonly InputAction m_GenericUI_Submit;
    private readonly InputAction m_GenericUI_Cancel;
    public struct GenericUIActions
    {
        private @PlayerControls m_Wrapper;
        public GenericUIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Submit => m_Wrapper.m_GenericUI_Submit;
        public InputAction @Cancel => m_Wrapper.m_GenericUI_Cancel;
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
    public struct InventoryUIActions
    {
        private @PlayerControls m_Wrapper;
        public InventoryUIActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @InventoryLeft => m_Wrapper.m_InventoryUI_InventoryLeft;
        public InputAction @InventoryRight => m_Wrapper.m_InventoryUI_InventoryRight;
        public InputAction @Inventory => m_Wrapper.m_InventoryUI_Inventory;
        public InputAction @Move => m_Wrapper.m_InventoryUI_Move;
        public InputAction @Submit => m_Wrapper.m_InventoryUI_Submit;
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
    }
    public interface IGenericUIActions
    {
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IBattleActions
    {
        void OnPass(InputAction.CallbackContext context);
        void OnLockDice(InputAction.CallbackContext context);
        void OnDodge(InputAction.CallbackContext context);
        void OnBlock(InputAction.CallbackContext context);
        void OnChangeSceneHax(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IInventoryUIActions
    {
        void OnInventoryLeft(InputAction.CallbackContext context);
        void OnInventoryRight(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
    }
}
