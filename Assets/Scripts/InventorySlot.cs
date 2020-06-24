using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;

    //public Sprite dot;
    //public Sprite holder;
    //public Image slotImage;
    //public Image icon;
    //public Button removeButton;
    [SerializeField] GameObject childHolder;



    public void AddItem(Item newItem)
    {
        item = newItem;

        Instantiate(newItem.prefab, childHolder.transform);

        //slotImage.sprite = holder;
        //icon.sprite = item.icon;
        //icon.enabled = true;
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        //icon.sprite = null;
        //icon.enabled = false;
        //removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public GameObject GetChildHolder()
    {
        return childHolder;
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }

}
