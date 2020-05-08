using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerBattleController : MonoBehaviour
{
    [SerializeField] int maxHP;
    [SerializeField] int currentHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CrossPlatformInputManager.GetButtonDown("Cancel"))
        {
            FindObjectOfType<LevelLoader>().LoadOverworldScene();
        }
    }
}
