using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public List<TileflipTable> tables = new List<TileflipTable>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            float pickedNumber = Random.Range(0, totalWeight);


            for (int i = 0; i < weightTable.Count; i++)
            {
                if (pickedNumber <= weightTable[i])
                {
                    for (int k = 0; k < matchedTable.encounters[i].list.Count; k++)
                    {
                        Debug.Log(matchedTable.encounters[i].list[k]);
                    }
                    //FindObjectOfType<LevelLoader>().LoadBattleScene();
                    Debug.Log("\n");
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
        LaunchBattle(tableToSend);
    }
}
