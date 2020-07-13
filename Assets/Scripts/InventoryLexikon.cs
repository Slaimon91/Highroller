using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryLexikon : MonoBehaviour
{
    [SerializeField] Sprite questionMarkSprite;
    private string questionMarkName = "?????";

    [SerializeField] Image[] enemyIcons;

    [SerializeField] TextMeshProUGUI[] enemyNames;

    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoText;

    [SerializeField] EnemyLexikon enemyLexikon;
    private List<bool> seenLexikon = new List<bool>();
    private List<int> soulsLexikon = new List<int>();
    InventoryUI inventoryUI;

    private int currentlySelectedPos = 0;
    PlayerControls controls;
    private Vector2 movement;
    private float registeredMovement = 0f;
    private bool movementOffCooldown = true;

    void Awake()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();

        controls = FindObjectOfType<PlayerControlsManager>().GetControls();
        controls.InventoryUI.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.InventoryUI.Move.canceled += ctx => movement = Vector2.zero;
    }

    void Start()
    {
        for (int i = 0; i < enemyLexikon.entries.Count; i++)
        {
            seenLexikon.Add(false);
            soulsLexikon.Add(0);
        }
        UpdateLexikonPos();
    }
    void Update()
    {
        DetectMove();
        Debug.Log(currentlySelectedPos);
    }

    void DetectMove()
    {
        if (Mathf.Abs(movement.y) == 1f && movementOffCooldown == true)
        {
            if(movement.y > 0)
            {
                currentlySelectedPos--;
                if(currentlySelectedPos < 0)
                {
                    currentlySelectedPos = enemyLexikon.entries.Count - 1;
                }
                UpdateLexikonPos();
            }
            else
            {
                currentlySelectedPos++;
                if (currentlySelectedPos > enemyLexikon.entries.Count)
                {
                    currentlySelectedPos = 0;
                }
                UpdateLexikonPos();
            }

            movementOffCooldown = false;
            StartCoroutine(TriggerCooldown());
        }
    }

    IEnumerator TriggerCooldown()
    {
        yield return new WaitForSeconds(0.15f);

        movementOffCooldown = true;
    }

    void UpdateLexikonPos()
    {
        infoName.text = enemyLexikon.entries[currentlySelectedPos].enemyName;
        infoText.text = enemyLexikon.entries[currentlySelectedPos].info;

        int relativeCounter = -2;
        for(int i = 0; i < 5; i++)
        {
            if(relativeCounter + currentlySelectedPos == -1)
            {
                enemyIcons[i].sprite = enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyIcon;
                enemyNames[i].text = enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyNumber + ". " + 
                    enemyLexikon.entries[enemyLexikon.entries.Count - 1].enemyName;
            }
            else if (relativeCounter + currentlySelectedPos < -1)
            {
                enemyIcons[i].sprite = enemyLexikon.entries[enemyLexikon.entries.Count -2].enemyIcon;
                enemyNames[i].text = enemyLexikon.entries[enemyLexikon.entries.Count -2].enemyNumber - 1 + ". " +
                    enemyLexikon.entries[enemyLexikon.entries.Count -2].enemyName;
            }
            else if(relativeCounter + currentlySelectedPos == enemyLexikon.entries.Count)
            {
                enemyIcons[i].sprite = enemyLexikon.entries[0].enemyIcon;
                enemyNames[i].text = enemyLexikon.entries[0].enemyNumber + ". " + 
                    enemyLexikon.entries[0].enemyName;
            }
            else if (relativeCounter + currentlySelectedPos > enemyLexikon.entries.Count)
            {
                enemyIcons[i].sprite = enemyLexikon.entries[1].enemyIcon;
                enemyNames[i].text = enemyLexikon.entries[1].enemyNumber - 1 + ". " +
                    enemyLexikon.entries[1].enemyName;
            }
            else
            {
                enemyIcons[i].sprite = enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyIcon;
                enemyNames[i].text = enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyNumber + ". " + 
                    enemyLexikon.entries[currentlySelectedPos + relativeCounter].enemyName;
            }

            relativeCounter++;
        }
    }

    void OnEnable()
    {
        controls = FindObjectOfType<PlayerControlsManager>().GetControls();
    }
}
