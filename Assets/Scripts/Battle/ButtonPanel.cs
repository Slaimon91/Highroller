using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPanel : MonoBehaviour
{
    [SerializeField] Image orangeText;
    [SerializeField] Image redText;
    [SerializeField] Image greenText;
    [SerializeField] Image blueText;

    [SerializeField] Sprite emptyText;
    [SerializeField] Sprite orangenaText;
    [SerializeField] Sprite rednaText;
    [SerializeField] Sprite bluenaText;
    [SerializeField] Sprite greennaText;
    [SerializeField] Sprite assignText;
    [SerializeField] Sprite combineText;
    [SerializeField] Sprite deselectText;
    [SerializeField] Sprite deselectGreenText;
    [SerializeField] Sprite dodgeText;
    [SerializeField] Sprite guardText;
    [SerializeField] Sprite lockText;
    [SerializeField] Sprite passText;
    [SerializeField] Sprite selectText;
    [SerializeField] Sprite splitText;
    [SerializeField] Sprite toggleText;
    [SerializeField] Sprite unlockText;

    public void SetEmptyOrangeText()
    {
        orangeText.sprite = orangenaText;
    }
    public void SetEmptyRedText()
    {
        redText.sprite = rednaText;
    }
    public void SetEmptyGreenText()
    {
        greenText.sprite = greennaText;
    }
    public void SetEmptyBlueText()
    {
        blueText.sprite = bluenaText;
    }
    public void SetAssignText()
    {
        greenText.sprite = assignText;
    }

    public void SetCombineText()
    {
        greenText.sprite = combineText;
    }
    public void SetDeselectText()
    {
        redText.sprite = deselectText;
    }
    public void SetDeselectGreenText()
    {
        greenText.sprite = deselectGreenText;
    }
    public void SetDodgeText()
    {
        redText.sprite = dodgeText;
    }
    public void SetGuardText()
    {
        greenText.sprite = guardText;
    }
    public void SetLockText()
    {
        blueText.sprite = lockText;
    }
    public void SetPassText()
    {
        orangeText.sprite = passText;
    }
    public void SetSelectText()
    {
        greenText.sprite = selectText;
    }
    public void SetSplitText()
    {
        redText.sprite = splitText;
    }
    public void SetToggleText()
    {
        greenText.sprite = toggleText;
    }
    public void SetUnlockText()
    {
        blueText.sprite = unlockText;
    }
}
