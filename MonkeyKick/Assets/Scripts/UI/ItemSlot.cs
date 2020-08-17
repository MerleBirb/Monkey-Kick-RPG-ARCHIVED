using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    ////////// ITEM SLOT //////////
    /// this script handles and stores information about items in their respective inventory slots
    
    // store the item and it's image
    private Item item;
    public Image icon;

    // adds the item to the current inventory slot assigned
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.image;
        icon.enabled = true;
    }

    // clears the slot for now
    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    // uses the item in the slot
    public void UseItem()
    {
        if (item != null)
        {
            item.UseItem(GameManager.mainParty[0]);
        }
    }
}
