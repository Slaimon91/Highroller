using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class BattleBounty : MonoBehaviour
{
    private int xpAmount = 0;
    private int xpMultiplier = 1;
    private int targetScore;
    private int levelUp = 0;
    private float xpScrollSpeed = 15;
    private float displayXPWaitTime = 0.75f;
    private bool finishedDisplayingXP = false;
    private List<int> gotSouls = new List<int>();
    private List<GameObject> xpBar = new List<GameObject>();
    [SerializeField] GameObject xpHolderGO;
    [SerializeField] GameObject chooseHolderGO;
    [SerializeField] GameObject soulsRewardGO;
    [SerializeField] GameObject emptyBarPrefab;
    [SerializeField] GameObject filledBarPrefab;
    [SerializeField] GameObject xpBarHolder;
    [SerializeField] GameObject firstXPSelection;
    [SerializeField] GameObject firstChooseSelection;
    [SerializeField] GameObject firstSoulSelection;

    [SerializeField] TextMeshProUGUI expText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI totalText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expScoreText;
    [SerializeField] TextMeshProUGUI soulNameText;

    [SerializeField] PlayerValues playerValues;
    [SerializeField] ExpPerLevel expPerLevel;
    [SerializeField] GameObject rewardOverlay;
    [SerializeField] EnemyLexikon enemyLexikon;

    [SerializeField] Animator healthAnimator;
    [SerializeField] Animator gaiaAnimator;
    [SerializeField] Animator soulAnimator;

    EventSystem eventSystem;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    public void AddXP(int xp)
    {
        xpAmount += xp;
    }

    public void SetXPMultiplier(int multi)
    {
        if(multi > xpMultiplier)
        {
            xpMultiplier = multi;
        }
    }

    public void GiveBounty()
    {
        rewardOverlay.SetActive(true);
        xpHolderGO.SetActive(true);
        StartCoroutine(DisplayValues());        
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

    public void UpdateXpBar()
    {
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

    IEnumerator DisplayValues()
    {
        eventSystem.SetSelectedGameObject(firstXPSelection);
        levelText.text = "Lvl " + playerValues.level.ToString();
        expScoreText.text = (playerValues.xp - expPerLevel.expPerLevelTable[playerValues.level-1].totalExp).ToString() + "/" + expPerLevel.expPerLevelTable[playerValues.level].exp;
        InitiateXpBar();

        yield return new WaitForSeconds(displayXPWaitTime);

        expText.text = xpAmount.ToString();

        yield return new WaitForSeconds(displayXPWaitTime);

        comboText.text = xpMultiplier.ToString() + "x";

        yield return new WaitForSeconds(displayXPWaitTime);

        totalText.text = (xpAmount*xpMultiplier).ToString();

        StartCoroutine(IncrementXP());

        yield return null;
    }

    IEnumerator IncrementXP()
    {
        targetScore = playerValues.xp + (xpAmount * xpMultiplier);
        float tempXP = (int)playerValues.xp;
        while (tempXP < targetScore)
        {
            float numToInc = (xpScrollSpeed * Time.deltaTime);
            tempXP += numToInc; // or whatever to get the speed you like

            if (tempXP > targetScore)
            {
                tempXP = targetScore;
            }

            playerValues.xp = (int)tempXP;

            if ((playerValues.xp - expPerLevel.expPerLevelTable[playerValues.level - 1].totalExp) >= expPerLevel.expPerLevelTable[playerValues.level].exp) //Level up
            {
                levelUp++;
                playerValues.level++;
                levelText.text = "Lvl " + playerValues.level.ToString();
            }

            expScoreText.text = (playerValues.xp - expPerLevel.expPerLevelTable[playerValues.level - 1].totalExp).ToString() + "/" + expPerLevel.expPerLevelTable[playerValues.level].exp;
            UpdateXpBar();
            yield return null;
        }

        finishedDisplayingXP = true;
        yield return null;
    }

    public void XPPressContinue()
    {
        if(!finishedDisplayingXP)
        {
            displayXPWaitTime = 0f;
            xpScrollSpeed = 100f;
            return;
        }

        if(levelUp > 0)
        {
            xpHolderGO.SetActive(false);
            chooseHolderGO.SetActive(true);
            eventSystem.SetSelectedGameObject(firstChooseSelection);
        }
        else if(gotSouls.Any())
        {
            xpHolderGO.SetActive(false);
            soulsRewardGO.SetActive(true);
            soulAnimator.SetFloat("IntroNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
            soulAnimator.SetFloat("IdleNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
            eventSystem.SetSelectedGameObject(firstSoulSelection);
            StartCoroutine(DisplaySoulText());
        }
        else
        {
            FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
            FindObjectOfType<LevelLoader>().LoadOverworldScene();
        }
    }

    public void HealthPressContinue()
    {
        levelUp--;
        playerValues.maxHealthPoints += 5;
        
        if (levelUp == 0)
        {
            if (gotSouls.Any())
            {
                chooseHolderGO.SetActive(false);
                soulsRewardGO.SetActive(true);
                soulAnimator.SetFloat("IntroNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
                soulAnimator.SetFloat("IdleNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
                eventSystem.SetSelectedGameObject(firstSoulSelection);
                StartCoroutine(DisplaySoulText());
            }
            else
            {
                FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
                FindObjectOfType<LevelLoader>().LoadOverworldScene();
            }
        }
        else
        {
            eventSystem.SetSelectedGameObject(firstChooseSelection);
        }
    }

    public void GaiaPressContinue()
    {
        levelUp--;
        playerValues.maxGaia += 25;
        
        if (levelUp == 0)
        {
            if (gotSouls.Any())
            {
                chooseHolderGO.SetActive(false);
                soulsRewardGO.SetActive(true);
                soulAnimator.SetFloat("IntroNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
                soulAnimator.SetFloat("IdleNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
                eventSystem.SetSelectedGameObject(firstSoulSelection);
                StartCoroutine(DisplaySoulText());
            }
            else
            {
                FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
                FindObjectOfType<LevelLoader>().LoadOverworldScene();
            }
        }
        else
        {
            eventSystem.SetSelectedGameObject(firstChooseSelection);
        }
    }

    public void SoulsPressContinue()
    {
        gotSouls.RemoveAt(0);

        if (!gotSouls.Any())
        {
            FindObjectOfType<PlayerControlsManager>().ToggleOffGenericUI();
            FindObjectOfType<LevelLoader>().LoadOverworldScene();
        }
        else
        {
            soulNameText.gameObject.SetActive(false);
            soulAnimator.SetFloat("IntroNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
            soulAnimator.SetFloat("IdleNumber", enemyLexikon.entries[gotSouls[0]].enemyNumber - 1);
            soulAnimator.Play("IntroTree", -1, 0f);
            eventSystem.SetSelectedGameObject(firstSoulSelection);
            StartCoroutine(DisplaySoulText());
        }
    }

    IEnumerator DisplaySoulText()
    {
        yield return new WaitForSeconds(0.75f);

        soulNameText.gameObject.SetActive(true);
        soulNameText.text = enemyLexikon.entries[gotSouls[0]].enemyName + " soul";
    }

    public void AddSoul(string monsterName)
    {
        for(int i = 0; i < enemyLexikon.entries.Count; i++)
        {
            if(enemyLexikon.entries[i].enemyName == monsterName)
            {
                enemyLexikon.entries[i].soulCount++;
                gotSouls.Add(i);
                break;
            }
        }
    }

    public void StartHealthAnim()
    {
        healthAnimator.SetTrigger("HealthAnim");
        eventSystem.SetSelectedGameObject(firstXPSelection);
        StartCoroutine(StartHealthAnimWait());
    }

    public IEnumerator StartHealthAnimWait()
    {
        yield return new WaitForSeconds(1.75f);
        HealthPressContinue();
    }

    public void StartGaiaAnim()
    {
        gaiaAnimator.SetTrigger("GaiaAnim");
        eventSystem.SetSelectedGameObject(firstXPSelection);
        StartCoroutine(StartGaiaAnimWait());
    }

    public IEnumerator StartGaiaAnimWait()
    {
        yield return new WaitForSeconds(1.75f);
        GaiaPressContinue();
    }
}
