using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GeneralInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthValueText;
    [SerializeField] TextMeshProUGUI gaiaValueText;
    [SerializeField] TextMeshProUGUI currencyValueText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expScoreText;
    [SerializeField] GameObject emptyBarPrefab;
    [SerializeField] GameObject filledBarPrefab;
    [SerializeField] GameObject xpBarHolder;
    [SerializeField] PlayerValues playerValues;
    [SerializeField] ExpPerLevel expPerLevel;

    private List<GameObject> xpBar = new List<GameObject>();

    private void OnEnable()
    {
        healthValueText.text = playerValues.healthPoints + "/" + playerValues.maxHealthPoints;
        gaiaValueText.text = playerValues.gaia + "/" + playerValues.maxGaia;
        currencyValueText.text = playerValues.currency + "";
        DisplayXP();
    }

    private void DisplayXP()
    {
        levelText.text = "Lvl " + playerValues.level.ToString();
        expScoreText.text = (playerValues.xp - expPerLevel.expPerLevelTable[playerValues.level - 1].totalExp).ToString() + "/" + expPerLevel.expPerLevelTable[playerValues.level].exp;

        if (!xpBar.Any())
        {
            InitiateXpBar();
            return;
        }

        float xpPerBar = (float)expPerLevel.expPerLevelTable[playerValues.level].exp / 26;

        for (int i = 0; i < 26; i++)
        {
            if ((playerValues.xp - expPerLevel.expPerLevelTable[playerValues.level - 1].totalExp) > xpPerBar * i)
            {
                if (xpBar[i] != filledBarPrefab)
                {
                    Destroy(xpBar[i]);
                    xpBar.RemoveAt(i);
                    xpBar.Insert(i, Instantiate(filledBarPrefab, xpBarHolder.transform));
                }
            }
            else
            {
                if (xpBar[i] != emptyBarPrefab)
                {
                    Destroy(xpBar[i]);
                    xpBar.RemoveAt(i);
                    xpBar.Insert(i, Instantiate(emptyBarPrefab, xpBarHolder.transform));
                }
            }
        }
    }

    public void InitiateXpBar()
    {
        float xpPerBar = (float)expPerLevel.expPerLevelTable[playerValues.level].exp / 26;

        for (int i = 0; i < 26; i++)
        {
            if ((playerValues.xp - expPerLevel.expPerLevelTable[playerValues.level - 1].totalExp) > xpPerBar * i)
            {
                xpBar.Add(Instantiate(filledBarPrefab, xpBarHolder.transform));
            }
            else
            {
                xpBar.Add(Instantiate(emptyBarPrefab, xpBarHolder.transform));
            }
        }
    }
}
