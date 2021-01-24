using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CorruptionBar : MonoBehaviour
{
    [SerializeField] List<GameObject> checkpoints = new List<GameObject>();
    [SerializeField] Sprite hpFreeze;
    [SerializeField] Sprite gaiaFreeze;
    [SerializeField] Sprite monsterFreeze;
    [SerializeField] Sprite hpBigFreeze;
    [SerializeField] Sprite gaiaBigFreeze;
    [SerializeField] Sprite monsterBigFreeze;

    private int clearedCheckpoints = 0;
    private float checkpointModifier;

    private bool buttonDown;
    private float buttonDownTimer;
    private bool holding = false;
    private bool passCompleted = false;

    private float requiredHoldTime;

    public UnityEvent onLongClick;

    [SerializeField] private Image fillImage;

    private GameObject savedSelectedGameObject;
    private EventSystem eventSystem;
    private AudioManager audioManager;
    private ButtonPanel buttonPanel;
    private CorruptionSourceManager corruptionSourceManager;
    private bool pausedForAnim = false;
    public delegate void WaitForCheckpointAnim();
    public WaitForCheckpointAnim onWaitForCheckpointAnimCallback;

    void Awake()
    {
        eventSystem = FindObjectOfType<EventSystem>();
        audioManager = FindObjectOfType<AudioManager>();
        corruptionSourceManager = FindObjectOfType<CorruptionSourceManager>();
    }

    void Start()
    {
        buttonPanel = FindObjectOfType<ButtonPanel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (buttonDown && !pausedForAnim)
        {
            if (eventSystem.currentSelectedGameObject != null)
            {
                savedSelectedGameObject = eventSystem.currentSelectedGameObject;
                audioManager.Play("Passing");
            }

            eventSystem.SetSelectedGameObject(null);

            buttonDownTimer += Time.deltaTime;
            fillImage.fillAmount = buttonDownTimer / requiredHoldTime;

            if (fillImage.fillAmount >= (clearedCheckpoints * checkpointModifier + checkpointModifier) || fillImage.fillAmount == 1)
            {
                corruptionSourceManager.TriggerCheckpoint(clearedCheckpoints);
                clearedCheckpoints++;
            }
        }
        else
        {
            if(fillImage.fillAmount > (clearedCheckpoints * checkpointModifier))
            {
                buttonDownTimer -= Time.deltaTime;
                if (buttonDownTimer < 0)
                    buttonDownTimer = 0;
                fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
            }
        }
    }

    private void PassCompleted()
    {
        corruptionSourceManager.CleansingCompleted();
        audioManager.Stop("Passing");
        audioManager.Play("PassCompleted");
        savedSelectedGameObject = eventSystem.firstSelectedGameObject;
    }

    public void Reset()
    {
        audioManager.Stop("Passing");
        buttonDown = false;
        passCompleted = false;
        eventSystem.SetSelectedGameObject(savedSelectedGameObject);
    }

    public void ButtonStarted()
    {
        holding = true;
        buttonDown = true;
    }

    public void ButtonCanceled()
    {
        if (holding && !passCompleted)
        {
            holding = false;
            Reset();
        }
    }

    public void SetCheckpoints(int nr, string checkpointInfo, bool isLast)
    {
        clearedCheckpoints++;
        buttonDownTimer = (clearedCheckpoints);
        fillImage.fillAmount = buttonDownTimer / requiredHoldTime;
        checkpoints[nr].GetComponent<Animator>().enabled = false;

        switch (checkpointInfo)
        {
            case "isHP":
                if (!isLast)
                    checkpoints[nr].GetComponent<Image>().sprite = hpFreeze;
                else
                    checkpoints[nr].GetComponent<Image>().sprite = hpBigFreeze;
                break;
            case "isGaia":
                if (!isLast)
                    checkpoints[nr].GetComponent<Image>().sprite = gaiaFreeze;
                else
                    checkpoints[nr].GetComponent<Image>().sprite = gaiaBigFreeze;
                break;
            case "isMonster":
                if (!isLast)
                    checkpoints[nr].GetComponent<Image>().sprite = monsterFreeze;
                else
                    checkpoints[nr].GetComponent<Image>().sprite = monsterBigFreeze;
                break;
        }
    }

    public void SetNrOfCheckpoints(int nr)
    {
        checkpointModifier = (float)(1f / nr);
        requiredHoldTime = nr;
    }

    public void TriggerCheckpointAnim(int checkpoint, string optionString)
    {
        pausedForAnim = true;
        if(clearedCheckpoints + 1 == requiredHoldTime)
        {
            checkpoints[checkpoint].GetComponent<Animator>().SetBool("isBig", true);
            passCompleted = true;
            PassCompleted();
        }
        checkpoints[checkpoint].GetComponent<Animator>().SetBool(optionString, true);
    }

    public void UnpauseBox()
    {
        pausedForAnim = false;
        onWaitForCheckpointAnimCallback?.Invoke();
    }

    public void UnpauseBoxFinal()
    {
        onWaitForCheckpointAnimCallback?.Invoke();
    }
}
