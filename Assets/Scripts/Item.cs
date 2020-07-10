using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Highroller/InventoryItem")]
public class Item : ScriptableObject
{
    public new string name = "New Item";
    //public Sprite icon = null;
    public GameObject prefab = null;

    public virtual void Use()
    {
        //Use item
        Debug.Log("Using" + name);
    }
}
