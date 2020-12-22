using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileflipInfobox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI name;
    [SerializeField] TextMeshProUGUI HPChance;
    [SerializeField] TextMeshProUGUI gaiaChance;
    [SerializeField] TextMeshProUGUI monsterChance;

    private void Awake()
    {
        if(FindObjectOfType<AreaTitlebox>() != null)
        {
            FindObjectOfType<AreaTitlebox>().Remove();
        }
    }
    public void AssignInfo(string nameText, string HPText, string gaiaText, string monsterText)
    {
        name.text = nameText;
        HPChance.text = HPText;
        gaiaChance.text = gaiaText;
        monsterChance.text = monsterText;
    }
}
