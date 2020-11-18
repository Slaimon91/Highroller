using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaiablockLog : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject popUpBox;
    [SerializeField] GameObject overworldCanvas;
    [SerializeField] int gaiaAmount = 0;
    [SerializeField] PlayerValues playerValues;
    private GameObject popup;

    public void Interact()
    {
        string questionText = "Release <color=#84B724>" + gaiaAmount + "</color>";
        popup = Instantiate(popUpBox, overworldCanvas.transform);
        popup.GetComponent<PopupQuestion>().onYesAnswerCallback += YesGaiaBlock;
        popup.GetComponent<PopupQuestion>().onNoAnswerCallback += NoGaiaBlock;
        popup.GetComponent<PopupQuestion>().SetQuestionText(questionText);
        if(playerValues.gaia < gaiaAmount)
        {
            popup.GetComponent<PopupQuestion>().DisableYesButton();
        }
    }
    public void YesGaiaBlock()
    {
        Debug.Log("You released the gaia");
        popup.GetComponent<PopupQuestion>().onYesAnswerCallback -= YesGaiaBlock;
        popup.GetComponent<PopupQuestion>().onNoAnswerCallback -= NoGaiaBlock;
        FindObjectOfType<LaunchRewards>().LanuchGaiaRewardbox(-gaiaAmount);
        Destroy(gameObject);
    }

    public void NoGaiaBlock()
    {
        Debug.Log("You said no to the gaia");
        popup.GetComponent<PopupQuestion>().onYesAnswerCallback -= YesGaiaBlock;
        popup.GetComponent<PopupQuestion>().onNoAnswerCallback -= NoGaiaBlock;
    }
}
