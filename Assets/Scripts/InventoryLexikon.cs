using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryLexikon : MonoBehaviour
{
    [SerializeField] Image[] enemyIcons;

    [SerializeField] TextMeshProUGUI[] enemyNames;

    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI infoSouls;

    [SerializeField] EnemyLexikon enemyLexikon;
    private List<bool> seenLexikon = new List<bool>();
    private List<int> soulsLexikon = new List<int>();
    InventoryUI inventoryUI;

    private int currentlySelectedPos = 0;
    PlayerControls controls;
    private Vector2 movement;
    private float registeredMovement = 0f;
    private bool movementOffCooldown = true;
    [SerializeField] Animator soulAnimator;
    [SerializeField] Animator idleAnimator;
    [SerializeField] Sprite questionMarkSprite;
    void Awake()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
        controls = FindObjectOfType<PlayerControlsManager>().GetControls();
        controls.InventoryUI.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.InventoryUI.Move.canceled += ctx => movement = Vector2.zero;
    }

    void PopulateLexikon()
    {
        for (int i = 0; i < enemyLexikon.entries.Count; i++)
        {
            seenLexikon.Add(false);
            soulsLexikon.Add(enemyLexikon.entries[i].soulCount);
        }
        soulAnimator.SetFloat("SoulNumber", currentlySelectedPos);
        idleAnimator.SetFloat("IdleNumber", currentlySelectedPos);
        UpdateLexikonPos();

    }

    void Update()
    {
        DetectMove();
    }

    void DetectMove()
    {
        if (Mathf.Abs(movement.y) == 1f && movementOffCooldown == true)
        {
            if(movement.y > 0)
            {

                currentlySelectedPos--;
                soulAnimator.SetFloat("SoulNumber", currentlySelectedPos);
                idleAnimator.SetFloat("IdleNumber", currentlySelectedPos);
                if (currentlySelectedPos < 0)
                {
                    currentlySelectedPos = enemyLexikon.entries.Count - 1;
                    soulAnimator.SetFloat("SoulNumber", enemyLexikon.entries.Count - 1);
                    idleAnimator.SetFloat("IdleNumber", enemyLexikon.entries.Count - 1);
                }
            }
            else
            {
                
                currentlySelectedPos++;
                soulAnimator.SetFloat("SoulNumber", currentlySelectedPos);
                idleAnimator.SetFloat("IdleNumber", currentlySelectedPos);
                if (currentlySelectedPos > enemyLexikon.entries.Count - 1)
                {
                    currentlySelectedPos = 0;
                    soulAnimator.SetFloat("SoulNumber", 0);
                    idleAnimator.SetFloat("IdleNumber", 0);
                }
            }
            UpdateLexikonPos();
            movementOffCooldown = false;
            StartCoroutine(TriggerCooldown());
        }
    }

    IEnumerator TriggerCooldown()
    {
        yield return new WaitForSeconds(0.2f);

        movementOffCooldown = true;
    }

    void UpdateLexikonPos()
    {
        if (enemyLexikon.entries[currentlySelectedPos].hasEncountered)
        {
            infoName.text = enemyLexikon.entries[currentlySelectedPos].enemyName;
            infoText.text = enemyLexikon.entries[currentlySelectedPos].info;
            infoSouls.text = enemyLexikon.entries[currentlySelectedPos].soulCount.ToString();
        }
        else
        {
            infoName.text = "";
            infoText.text = "";
            infoSouls.text = "";
            soulAnimator.SetFloat("SoulNumber", -1);
            idleAnimator.SetFloat("IdleNumber", -1);
        }

        int relativeCounter = -2;
        for(int i = 0; i < 5; i++)
        {
            if(relativeCounter + currentlySelectedPos == -1)
            {
                if(enemyLexikon.entries[enemyLexikon.entries.Count - 1].hasEncountered)
                {
                    enemyIcons[i].sprite = enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyIcon;
                    enemyNames[i].text = enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyNumber + ". " +
                        enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyName;
                }
                else
                {
                    enemyIcons[i].sprite = questionMarkSprite;
                    enemyNames[i].text = enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyNumber + ". ";
                }

            }
            else if (relativeCounter + currentlySelectedPos < -1)
            {
                if (enemyLexikon.entries[enemyLexikon.entries.Count - 2].hasEncountered)
                {
                    enemyIcons[i].sprite = enemyLexikon.entries[enemyLexikon.entries.Count - 2].enemyIcon;
                    enemyNames[i].text = enemyLexikon.entries[enemyLexikon.entries.Count - 2].enemyNumber - 1 + ". " +
                        enemyLexikon.entries[enemyLexikon.entries.Count - 2].enemyName;
                }
                else
                {
                    enemyIcons[i].sprite = questionMarkSprite;
                    enemyNames[i].text = enemyLexikon.entries[enemyLexikon.entries.Count - 2].enemyNumber + ". ";
                }

            }
            else if(relativeCounter + currentlySelectedPos == enemyLexikon.entries.Count)
            {
                if (enemyLexikon.entries[0].hasEncountered)
                {
                    enemyIcons[i].sprite = enemyLexikon.entries[0].enemyIcon;
                    enemyNames[i].text = enemyLexikon.entries[0].enemyNumber + ". " +
                        enemyLexikon.entries[0].enemyName;
                }
                else
                {
                    enemyIcons[i].sprite = questionMarkSprite;
                    enemyNames[i].text = enemyLexikon.entries[0].enemyNumber + ". ";
                }

            }
            else if (relativeCounter + currentlySelectedPos > enemyLexikon.entries.Count)
            {
                if (enemyLexikon.entries[1].hasEncountered)
                {
                    enemyIcons[i].sprite = enemyLexikon.entries[1].enemyIcon;
                    enemyNames[i].text = enemyLexikon.entries[1].enemyNumber - 1 + ". " +
                        enemyLexikon.entries[1].enemyName;
                }
                else
                {
                    enemyIcons[i].sprite = questionMarkSprite;
                    enemyNames[i].text = enemyLexikon.entries[1].enemyNumber + ". ";
                }

            }
            else
            {
                if (enemyLexikon.entries[currentlySelectedPos + relativeCounter].hasEncountered)
                {
                    enemyIcons[i].sprite = enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyIcon;
                    enemyNames[i].text = enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyNumber + ". " +
                        enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyName;
                }
                else
                {
                    enemyIcons[i].sprite = questionMarkSprite;
                    enemyNames[i].text = enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyNumber + ". ";
                }

            }

            relativeCounter++;
        }
    }

    void OnEnable()
    {
        controls = FindObjectOfType<PlayerControlsManager>().GetControls();
        PopulateLexikon();
    }
}
