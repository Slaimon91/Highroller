using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TileflipInfoboxMonster : MonoBehaviour
{
    [SerializeField] GameObject monsterbox1;
    [SerializeField] GameObject monsterbox2;
    [SerializeField] GameObject monsterbox3;
    [SerializeField] GameObject monsterbox4;

    private void Awake()
    {
        if (FindObjectOfType<AreaTitlebox>() != null)
        {
            FindObjectOfType<AreaTitlebox>().Remove();
        }
    }
    public void AssignInfo(List<Sprite> monsterIcons)
    {
        Image[] images = null;
        
        switch (monsterIcons.Count)
        {
            case 1:
                monsterbox1.SetActive(true);
                monsterbox2.SetActive(false);
                monsterbox3.SetActive(false);
                monsterbox4.SetActive(false);
                images = monsterbox1.GetComponentsInChildren<Image>();
                break;
            case 2:
                monsterbox1.SetActive(false);
                monsterbox2.SetActive(true);
                monsterbox3.SetActive(false);
                monsterbox4.SetActive(false);
                images = monsterbox2.GetComponentsInChildren<Image>();
                break;
            case 3:
                monsterbox1.SetActive(false);
                monsterbox2.SetActive(false);
                monsterbox3.SetActive(true);
                monsterbox4.SetActive(false);
                images = monsterbox3.GetComponentsInChildren<Image>();
                break;
            case 4:
                monsterbox1.SetActive(false);
                monsterbox2.SetActive(false);
                monsterbox3.SetActive(false);
                monsterbox4.SetActive(true);
                images = monsterbox4.GetComponentsInChildren<Image>();
                break;
        }

        for (int i = 0; i < monsterIcons.Count; i++)
        {
            images[i+1].sprite = monsterIcons[i];
        }
    }
}
