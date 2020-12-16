using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Adding a tiletype = add to enum, add to testgroundtype(), add to enountermanager GO in hierarchy

[System.Serializable]
public enum GroundType { FOD_Grass, FOD_Water, FOD_Glasses, FOD_Start }

public class EncounterManager : MonoBehaviour
{
    public List<TileflipTable> tables = new List<TileflipTable>();
    [SerializeField] BattleStartupInfo battleStartupInfo;
    private bool waitForAnim = false;
    private GameObject selectedTile;
    [SerializeField] PlayerValues playerValues;

    IEnumerator ActivateTile(TileflipTable matchedTable)
    {
        if(matchedTable == null)
        {
            yield return null;
        }

        float totalWeight = 0;
        int pickedOption = 0;
        totalWeight += matchedTable.gaiaChance;
        totalWeight += matchedTable.HPChance;
        totalWeight += matchedTable.monsterChance;

        if(totalWeight > 100)
        {
            Debug.LogError("Sum of chances for flipped tile is over 100");
        }

        float pickedNumber = Random.Range(0, totalWeight);
        TileflipVisual tfv = selectedTile.GetComponent<TileflipVisual>();
        tfv.onFlipAnimationDoneCallback += WaitForAnimDone;

        if (pickedNumber < matchedTable.HPChance && playerValues.healthPoints != playerValues.maxHealthPoints)
        {
            pickedOption = 2;
            pickedNumber = 999;
            tfv.TriggerHPAnimation();
        }

        pickedNumber -= matchedTable.HPChance;

        if (pickedNumber < matchedTable.gaiaChance && playerValues.gaia != playerValues.maxGaia)
        {
            pickedOption = 1;
            pickedNumber = 999;
            tfv.TriggerGaiaAnimation();
        }

        pickedNumber -= matchedTable.gaiaChance;

        if (pickedNumber < matchedTable.monsterChance)
        {
            pickedOption = 3;
            tfv.TriggerMonsterAnimation();
        }

        waitForAnim = true;
        FindObjectOfType<PlayerController>().RemoveFlipSquares();

        yield return StartCoroutine(WaitForFlipAnimation());

        tfv.onFlipAnimationDoneCallback -= WaitForAnimDone;

        if(pickedOption == 1)
        {
            FindObjectOfType<LaunchRewards>().LanuchGaiaRewardbox(matchedTable.gaiaRewardAmount);
        }
        else if (pickedOption == 2)
        {
            FindObjectOfType<LaunchRewards>().LanuchHPRewardbox(matchedTable.HPRewardAmount);
        }
        else if (pickedOption == 3)
        {
            LaunchBattle(matchedTable);
        }

        yield return null;
    }

    IEnumerator WaitForFlipAnimation()
    {
        while(waitForAnim)
        {
            yield return null;
        }

        yield return null;
    }

    public void WaitForAnimDone()
    {
        waitForAnim = false;
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

    public IEnumerator LaunchCustomBattle(GameObject monsterIConVisualGO, List<GameObject> enemies, Sprite background)
    {
        selectedTile = monsterIConVisualGO;
        TileflipVisual tfv = selectedTile.GetComponent<TileflipVisual>();
        tfv.onFlipAnimationDoneCallback += WaitForAnimDone;
        tfv.TriggerMonsterAnimation();
        waitForAnim = true;
        yield return StartCoroutine(WaitForFlipAnimation());
        battleStartupInfo.enemies.Clear();
        for (int k = 0; k < enemies.Count; k++)
        {
            battleStartupInfo.enemies.Add(enemies[k]);
        }
        battleStartupInfo.battleBackground = background;
        yield return null;
        FindObjectOfType<LevelLoader>().LoadBattleScene();
    }

    public TileflipTable TestGroundType(GroundType groundTile, GameObject tile, bool isTest)
    {
        selectedTile = tile;

        switch(groundTile)
        {
            case GroundType.FOD_Grass:
                return SearchTile(isTest, "FOD_Grass");
            case GroundType.FOD_Water:
                return SearchTile(isTest, "FOD_Water");
            case GroundType.FOD_Glasses:
                return SearchTile(isTest, "FOD_Glasses");
            case GroundType.FOD_Start:
                return SearchTile(isTest, "FOD_Start");
            default:
                selectedTile = null;
                return null;
        }
    }

    public TileflipTable SearchTile(bool isTest, string tileName)
    {
        
        TileflipTable tableToSend = tables.Find(x => x.name == tileName);
        if(isTest)
        {
            return tableToSend;
        }
        else
        {
            StartCoroutine(ActivateTile(tableToSend));
            return null;
        }
    }
}
