using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using TMPro;


public class EnemyInfo : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    private bool buttonSelected = false;

    //selectedTile.SetActive(false);
    [SerializeField] GameObject selected;
    private GameObject infoTextImage;
    private TextMeshProUGUI infoName;
    private TextMeshProUGUI infoText;
    private string unitName;
    private string unitText;

    void Start()
    {
        InfoPanel enemyPanel = GetComponentInParent<InfoPanel>();
        infoTextImage = enemyPanel.infoTextHolder;
        infoName = enemyPanel.infoName;
        infoText = enemyPanel.infoText;

    }
    // Update is called once per frame
    void Update()
    {

    }

    public void OnSelect(BaseEventData eventData)
    {
        buttonSelected = true;
        selected.SetActive(true);
        FindObjectOfType<AudioManager>().Play("MoveBattleCursor");
        infoName.text = unitName;
        infoText.text = unitText;
        infoTextImage.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        buttonSelected = false;
        //marked.SetActive(false);
        selected.SetActive(false);
        infoTextImage.SetActive(false);
    }

    public void AbilityClicked()
    {
        
    }

    public void SetButtonNavigation(Button otherButton, string direction)
    {
        Navigation nav = GetComponent<Button>().navigation;
        switch (direction)
        {
            case "up":
                nav.selectOnUp = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
            case "down":
                nav.selectOnDown = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
            case "left":
                nav.selectOnLeft = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
            case "right":
                nav.selectOnRight = otherButton;
                GetComponent<Button>().navigation = nav;
                break;
        }
    }

    public void SetUnitName(string newName)
    {
        unitName = newName;
    }

    public void SetUnitText(string newText)
    {
        unitText = newText;
    }
}
