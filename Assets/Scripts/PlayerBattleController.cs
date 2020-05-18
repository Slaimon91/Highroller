using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBattleController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Change Scene Hax"))
        {
            FindObjectOfType<LevelLoader>().LoadOverworldScene();
        }
    }
}
