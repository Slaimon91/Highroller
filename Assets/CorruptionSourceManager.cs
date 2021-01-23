﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorruptionSourceManager : MonoBehaviour
{
    [SerializeField] GameObject threeCheckpointBoxPrefab;
    [SerializeField] GameObject fiveCheckpointBoxPrefab;
    private GameObject threeCheckpointBox;
    private GameObject fiveCheckpointBox;
    private InFrontOfPlayerTrigger inFrontOfPlayerTrigger;
    private PlayerController playerController;
    private GameObject overWorldCanvas;
    private List<CorruptionCheckpoint> checkpoints = new List<CorruptionCheckpoint>();
    private int recentlyClearedNR = 0;
    private Sprite recentBackground;
    private CorruptionSource currentSource;
    private bool cleansingComplete = false;

    private void Awake()
    {
        inFrontOfPlayerTrigger = FindObjectOfType<InFrontOfPlayerTrigger>();
        overWorldCanvas = FindObjectOfType<OverworldCanvas>().gameObject;
    }

    public void StartCleansing()
    {
        if(inFrontOfPlayerTrigger.GetCollidingTileableStatus())
        {
            GameObject colliding = inFrontOfPlayerTrigger.GetCollidingGameObject();

            if (colliding.GetComponent<CorruptionSource>() != null)
            {
                currentSource = colliding.GetComponent<CorruptionSource>();

                if (currentSource.GetNumberOfCheckpoints() == 3)
                {
                    checkpoints = currentSource.GetCheckpoints();

                    threeCheckpointBox = Instantiate(threeCheckpointBoxPrefab, overWorldCanvas.transform);

                    threeCheckpointBox.GetComponent<CorruptionBar>().onWaitForCheckpointAnimeCallback += CheckpointEvent;
                    recentBackground = currentSource.GetBattleBackground();
                    threeCheckpointBox.GetComponent<CorruptionBar>().SetNrOfCheckpoints(checkpoints.Count);

                    for (int i = 0; i < checkpoints.Count; i++)
                    {
                        if(checkpoints[i].cleansed)
                        {
                            bool isLast = false;

                            if (i == checkpoints.Count - 1)
                                isLast = true;
                            threeCheckpointBox.GetComponent<CorruptionBar>().SetCheckpoints(i, GetOptionString(checkpoints[i].option), isLast);
                        }
                    }
                }

                else if (currentSource.GetNumberOfCheckpoints() == 5)
                {
                    checkpoints = currentSource.GetCheckpoints();

                    fiveCheckpointBox = Instantiate(fiveCheckpointBoxPrefab, overWorldCanvas.transform);

                    fiveCheckpointBox.GetComponent<CorruptionBar>().onWaitForCheckpointAnimeCallback += CheckpointEvent;
                    recentBackground = currentSource.GetBattleBackground();
                    fiveCheckpointBox.GetComponent<CorruptionBar>().SetNrOfCheckpoints(checkpoints.Count);

                    for (int i = 0; i < checkpoints.Count; i++)
                    {
                        if (checkpoints[i].cleansed)
                        {
                            bool isLast = false;

                            if (i == checkpoints.Count - 1)
                                isLast = true;
                            fiveCheckpointBox.GetComponent<CorruptionBar>().SetCheckpoints(i, GetOptionString(checkpoints[i].option), isLast);
                        }
                    }
                }
            }
        }
    }

    private string GetOptionString(CheckpointOptions option)
    {
        string optionString = "";

        switch (option)
        {
            case CheckpointOptions.Health:
                optionString = "isHP";
                break;
            case CheckpointOptions.Gaia:
                optionString = "isGaia";
                break;
            case CheckpointOptions.Monster:
                optionString = "isMonster";
                break;
        }

        return optionString;
    }

    public void TriggerCheckpoint(int cp)
    {
        checkpoints[cp].cleansed = true;
        recentlyClearedNR = cp;
        
        if (threeCheckpointBox != null)
        {
            threeCheckpointBox.GetComponent<CorruptionBar>().TriggerCheckpointAnim(cp, GetOptionString(checkpoints[cp].option));
        }
        else if (fiveCheckpointBox != null)
        {
            fiveCheckpointBox.GetComponent<CorruptionBar>().TriggerCheckpointAnim(cp, GetOptionString(checkpoints[cp].option));
        }
    }

    public void CleansingCompleted()
    {
        cleansingComplete = true;
    }

    public void StartHolding()
    {
        if (threeCheckpointBox != null)
        {
            threeCheckpointBox.GetComponent<CorruptionBar>().ButtonStarted();
        }
        else if (fiveCheckpointBox != null)
        {
            fiveCheckpointBox.GetComponent<CorruptionBar>().ButtonStarted();
        }
    }

    public void StopHolding()
    {
        if(threeCheckpointBox != null)
        {
            threeCheckpointBox.GetComponent<CorruptionBar>().ButtonCanceled();
        }
        else if (fiveCheckpointBox != null)
        {
            fiveCheckpointBox.GetComponent<CorruptionBar>().ButtonCanceled();
        }
    }

    public void StopCleansing()
    {
        if (threeCheckpointBox != null)
        {
            threeCheckpointBox.GetComponent<CorruptionBar>().onWaitForCheckpointAnimeCallback -= CheckpointEvent;
            Destroy(threeCheckpointBox.gameObject);
        }
        if (fiveCheckpointBox != null)
        {
            fiveCheckpointBox.GetComponent<CorruptionBar>().onWaitForCheckpointAnimeCallback -= CheckpointEvent;
            Destroy(fiveCheckpointBox.gameObject);
        }

        checkpoints = null;
        currentSource = null;
        recentlyClearedNR = 0;
    }

    public bool CanCleanse()
    {
        if (inFrontOfPlayerTrigger.GetCollidingTileableStatus() && !cleansingComplete)
        {
            return true;
        }

        return false;
    }

    public void CheckpointEvent()
    {
        if(checkpoints[recentlyClearedNR].option == CheckpointOptions.Health)
        {
            FindObjectOfType<LaunchRewards>().LanuchHPRewardbox(checkpoints[recentlyClearedNR].rewardAmount);
            if (cleansingComplete)
                ActivateDestroyAnim();
        }
        else if (checkpoints[recentlyClearedNR].option == CheckpointOptions.Gaia)
        {
            FindObjectOfType<LaunchRewards>().LanuchGaiaRewardbox(checkpoints[recentlyClearedNR].rewardAmount);
            if (cleansingComplete)
                ActivateDestroyAnim();
        }
        else if (checkpoints[recentlyClearedNR].option == CheckpointOptions.Monster)
        {
            if(threeCheckpointBox != null)
            {
                StartCoroutine(FindObjectOfType<EncounterManager>().LaunchCustomBattle(null, checkpoints[recentlyClearedNR].encounter.list, recentBackground));
            }
            else if (fiveCheckpointBox != null)
            {
                StartCoroutine(FindObjectOfType<EncounterManager>().LaunchCustomBattle(null, checkpoints[recentlyClearedNR].encounter.list, recentBackground));
            }
        }
    }

    public void ActivateDestroyAnim()
    {
        currentSource.GetComponent<Animator>().SetBool("isDestroyed", true);
        StopCleansing();
    }
}
