using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int healthPoints;
    public int maxHealthPoints;
    public int gaia;
    public int maxGaia;
    public int currency;
    public int xp;
    public int level;
    public int nrOfBattles;
    public string currentOWScene;
    public int currentSavefile;

    public Vector3 position;
    public Vector2 direction;

    public PlayerData(PlayerController playerController)
    {
        healthPoints = playerController.playerValues.healthPoints;
        maxHealthPoints = playerController.playerValues.maxHealthPoints;
        gaia = playerController.playerValues.gaia;
        maxGaia = playerController.playerValues.maxGaia;
        currency = playerController.playerValues.currency;
        xp = playerController.playerValues.xp;
        level = playerController.playerValues.level;
        nrOfBattles = playerController.playerValues.nrOfBattles;
        currentOWScene = playerController.playerValues.currentOWScene;
        currentSavefile = playerController.playerValues.currentSavefile;

        position = playerController.transform.position;
        direction = playerController.GetCurrentDirection();
    }
}
