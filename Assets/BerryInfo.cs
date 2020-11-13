using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BerryInfo : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] Image seedImage;
    [SerializeField] TextMeshProUGUI seedNameText;
    [SerializeField] GameObject selected;
    private BerryChoice berryChoice;

    public void OnSelect(BaseEventData eventData)
    {
        selected.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        selected.SetActive(false);
    }

    public void BerryInfoSetup(Sprite seedSprite, string seedName, BerryChoice bc)
    {
        seedImage.sprite = seedSprite;
        seedNameText.text = seedName;
        berryChoice = bc;
    }

    public void BerryClicked()
    {
        berryChoice.ChooseBerry(gameObject.GetComponent<BerryInfo>());
        Debug.Log("jejk");
    }
}
