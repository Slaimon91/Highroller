using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface EEnemyInterface
{

    void EnemySetup();
    void Assign(bool status);
    void EnemyAction();

    void TriggerDeath();
    Sprite GetIcon();

    bool GetDeathStatus();

    int GetDiceKeyNumber();

    string GetUnitName();

    DiceKey GetDiceKey();

    void SetDiceKeyGO(DiceKey diceKey);
}
