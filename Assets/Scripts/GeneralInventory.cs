using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GeneralInventory : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthValueText;
    [SerializeField] TextMeshProUGUI gaiaValueText;
    [SerializeField] TextMeshProUGUI currencyValueText;
    [SerializeField] PlayerValues playerValues;

    private void OnEnable()
    {
        healthValueText.text = playerValues.healthPoints + " / " + playerValues.maxHealthPoints;
        gaiaValueText.text = playerValues.gaia + " / " + playerValues.maxGaia;
        currencyValueText.text = playerValues.currency + "";
    }
}
