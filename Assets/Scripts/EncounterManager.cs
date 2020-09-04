using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<TileflipTable> tables = new List<TileflipTable>();
    [SerializeField] BattleStartupInfo battleStartupInfo;

    private void ActivateTile(TileflipTable matchedTable)
    {
        if(matchedTable == null)
        {
            return;
        }

        float totalWeight = 0;
        totalWeight += matchedTable.gaiaChance;
        totalWeight += matchedTable.HPChance;
        totalWeight += matchedTable.monsterChance;

        if(totalWeight > 100)
        {
            Debug.LogError("Sum of chances for flipped tile is over 100");
        }

        float pickedNumber = Random.Range(0, totalWeight);

        if(pickedNumber < matchedTable.gaiaChance)
        {
            FindObjectOfType<PlayerController>().LanuchGaiaRewardbox(matchedTable.gaiaRewardAmount);
            return;
        }

        pickedNumber -= matchedTable.gaiaChance;

        if (pickedNumber < matchedTable.HPChance)
        {
            FindObjectOfType<PlayerController>().LanuchHPRewardbox(matchedTable.HPRewardAmount);
            return;
        }

        pickedNumber -= matchedTable.HPChance;

        if (pickedNumber < matchedTable.monsterChance)
        {
            LaunchBattle(matchedTable);
            return;
        }
    }

    private void LaunchBattle(TileflipTable matchedTable)
    {
        if (matchedTable != null)
        {
            float totalWeight = 0;
            List<int> weightTable = new List<int>();
            foreach (Encounter enc in matchedTable.encounters)
            {
                totalWeight += enc.weight;
                weightTable.Add(enc.weight);
            }

            if (totalWeight > 100)
            {
                Debug.LogError("Sum of monster encounter chances for flipped tile is over 100");
            }

            float pickedNumber = Random.Range(0, totalWeight);
            battleStartupInfo.enemies.Clear();

            for (int i = 0; i < weightTable.Count; i++)
            {
                if (pickedNumber <= weightTable[i])
                {
                    for (int k = 0; k < matchedTable.encounters[i].list.Count; k++)
                    {
                        battleStartupInfo.enemies.Add(matchedTable.encounters[i].list[k]);
                    }
                    battleStartupInfo.battleBackground = matchedTable.battleBackground;
                    FindObjectOfType<LevelLoader>().LoadBattleScene();
                    return;
                }
                else
                {
                    pickedNumber -= weightTable[i];
                }
            }
        }
    }

    public void GreenforestGrass()
    {
        TileflipTable tableToSend = tables.Find(x => x.name == ("GreenforestGrass"));
        ActivateTile(tableToSend);
    }

    public void GreenforestSwamp()
    {
        TileflipTable tableToSend = tables.Find(x => x.name == ("GreenforestSwamp"));
        ActivateTile(tableToSend);
    }
    public void GreenforestWater()
    {
        TileflipTable tableToSend = tables.Find(x => x.name == ("GreenforestWater"));
        ActivateTile(tableToSend);
    }
}
