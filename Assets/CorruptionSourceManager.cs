using System.Collections;
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
                if (colliding.GetComponent<CorruptionSource>().GetNumberOfCheckpoints() == 3)
                {
                    checkpoints = colliding.GetComponent<CorruptionSource>().GetCheckpoints();

                    threeCheckpointBox = Instantiate(threeCheckpointBoxPrefab, overWorldCanvas.transform);

                    for(int i = 0; i < checkpoints.Count; i++)
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

                else if (colliding.GetComponent<CorruptionSource>().GetNumberOfCheckpoints() == 5)
                {

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
        threeCheckpointBox.GetComponent<CorruptionBar>().TriggerCheckpointAnim(cp, GetOptionString(checkpoints[cp].option));
    }

    public void CleansingCompleted()
    {

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
            Destroy(threeCheckpointBox.gameObject);
        }
        if (fiveCheckpointBox != null)
        {
            Destroy(fiveCheckpointBox.gameObject);
        }

        checkpoints = null;
    }

    public bool CanCleanse()
    {
        if (inFrontOfPlayerTrigger.GetCollidingTileableStatus())
        {
            return true;
        }

        return false;
    }
}
