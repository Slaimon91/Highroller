using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchRewards : MonoBehaviour
{
    [SerializeField] PlayerValues playerValues;
    [SerializeField] GameObject rewardbox;
    //[SerializeField] GameObject battleRewardbox;
    [SerializeField] GameObject rewardboxHolder;
    [SerializeField] Sprite HPSprite;
    [SerializeField] Sprite gaiaSprite;
    [SerializeField] Sprite GASprite;
    //[SerializeField] Sprite XPSprite;

    private GameObject overWorldCanvas;
    int pendingGaiaReward = 0;
    int pendingHPReward = 0;
    int pendingXPReward = 0;

    void Awake()
    {
        overWorldCanvas = FindObjectOfType<OverworldCanvas>().gameObject;
    }
    public void LanuchGaiaRewardbox(int amount)
    {
        string gaiaIntro = "";
        string gaiaText = "";

        if (amount < 0)
        {
            gaiaIntro = "You released";
        }
        else
        {
            gaiaIntro = "You absorbed";
        }

        if (amount == 666)
        {
            gaiaText = "full Gaia";
        }
        else
        {
            gaiaText = Mathf.Abs(amount) + " Gaia";
        }

        GameObject popup = Instantiate(rewardbox, rewardboxHolder.transform);
        popup.GetComponent<Rewardbox>().AssignInfo(gaiaIntro, gaiaText, gaiaSprite);
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(132f / 255f, 183f / 255f, 36f / 255f));
        pendingGaiaReward = amount;
        AcceptReward();
    }

    public void LanuchHPRewardbox(int amount)
    {
        string HPIntro = "";
        string HPText = "";

        if (amount < 0)
        {
            HPIntro = "You took";
        }
        else
        {
            HPIntro = "You recovered";
        }

        if (amount == 666)
        {
            HPText = "full HP";
        }
        else
        {
            HPText = Mathf.Abs(amount) + " HP";
        }

        GameObject popup = Instantiate(rewardbox, rewardboxHolder.transform);
        popup.GetComponent<Rewardbox>().AssignInfo(HPIntro, HPText, HPSprite);
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(231f / 255f, 75f / 255f, 8f / 255f));
        pendingHPReward = amount;
        AcceptReward();
    }

    public void LanuchGARewardbox(int amount)
    {
        string GAIntro = "";
        string GAText = "";

        if (amount < 0)
        {
            GAIntro = "You paid";
        }
        else
        {
            GAIntro = "You got";
        }

        GAText = Mathf.Abs(amount) + " Golden Acorns";


        GameObject popup = Instantiate(rewardbox, rewardboxHolder.transform);
        popup.GetComponent<Rewardbox>().AssignInfo(GAIntro, GAText, GASprite);
        popup.GetComponent<Rewardbox>().SetRewardTextColor(new Color(255f / 255f, 168f / 255f, 31f / 255f));
        playerValues.currency += amount;
    }

    /*public void LanuchBattleRewardbox(int amount, int multiplier)
    {
        string XPText;
        if (multiplier > 1)
        {
            XPText = amount + "XP";
        }
        else
        {
            XPText = amount + "XP";
        }

        GameObject popup = Instantiate(battleRewardbox, overWorldCanvas.transform);
        popup.GetComponent<TileflipRewardbox>().AssignInfo(XPText, XPSprite);
        popup.GetComponent<TileflipRewardbox>().onAcceptRewardCallback += AcceptReward;
        StartCoroutine(ShowXP(popup, amount, multiplier));
        pendingHPReward = amount * multiplier;
    }*/

    public IEnumerator IncrementGaia()
    {
        float rewardScrollSpeed = 15f;
        float targetScore = playerValues.gaia + pendingGaiaReward;
        float tempScore = (int)playerValues.gaia;
        if (targetScore > playerValues.maxGaia)
        {
            targetScore = playerValues.maxGaia;
        }

        if (targetScore > playerValues.gaia)
        {
            while (tempScore < targetScore)
            {
                float numToInc = (rewardScrollSpeed * Time.deltaTime);
                tempScore += numToInc; // or whatever to get the speed you like

                if (tempScore > targetScore)
                {
                    tempScore = targetScore;
                }

                playerValues.gaia = (int)tempScore;
                yield return null;
            }
        }

        else
        {
            while (tempScore > targetScore)
            {
                float numToInc = (rewardScrollSpeed * Time.deltaTime);
                tempScore -= numToInc; // or whatever to get the speed you like

                if (tempScore < targetScore)
                {
                    tempScore = targetScore;
                }

                playerValues.gaia = (int)tempScore;
                yield return null;
            }
        }


        pendingGaiaReward = 0;
        yield return null;
    }

    public IEnumerator IncrementHP()
    {
        float rewardScrollSpeed = 15f;
        float targetScore = playerValues.healthPoints + pendingHPReward;
        float tempScore = (int)playerValues.healthPoints;
        if (targetScore > playerValues.maxHealthPoints)
        {
            targetScore = playerValues.maxHealthPoints;
        }

        if (targetScore > playerValues.healthPoints)
        {
            while (tempScore < targetScore)
            {
                float numToInc = (rewardScrollSpeed * Time.deltaTime);
                tempScore += numToInc; // or whatever to get the speed you like

                if (tempScore > targetScore)
                {
                    tempScore = targetScore;
                }

                playerValues.healthPoints = (int)tempScore;
                yield return null;
            }
        }

        else
        {
            while (tempScore > targetScore)
            {
                float numToInc = (rewardScrollSpeed * Time.deltaTime);
                tempScore -= numToInc; // or whatever to get the speed you like

                if (tempScore < targetScore)
                {
                    tempScore = targetScore;
                }

                playerValues.healthPoints = (int)tempScore;
                yield return null;
            }
        }


        pendingHPReward = 0;
        yield return null;
    }

    /*public IEnumerator ShowXP(GameObject popup, int amount, int multiplier)
    {
        string XPText;
        yield return new WaitForSeconds(2);
        XPText = amount + "XP x" + multiplier;
        popup.GetComponent<TileflipRewardbox>().AssignInfo(XPText, XPSprite);
        yield return new WaitForSeconds(2);
        XPText = amount * multiplier + "XP";
        popup.GetComponent<TileflipRewardbox>().AssignInfo(XPText, XPSprite);
    }*/

    public void AcceptReward()
    {
        if (pendingGaiaReward != 0)
        {
            StartCoroutine(GetComponent<LaunchRewards>().IncrementGaia());
        }

        if (pendingHPReward != 0)
        {
            StartCoroutine(GetComponent<LaunchRewards>().IncrementHP());
        }
    }
}
