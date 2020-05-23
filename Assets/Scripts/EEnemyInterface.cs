using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EEnemyInterface
{
    void EnemySetup();
    void Assign(bool status);
    void EnemyAction();

    bool GetDeathStatus();

    int GetDiceKeyNumber();

    string GetUnitName();

    DiceKey GetDiceKey();

    void SetDiceKeyGO(DiceKey diceKey);
}
