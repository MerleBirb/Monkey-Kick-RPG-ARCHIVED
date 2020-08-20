using System;
using UnityEditor;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    ////////// INVENTORY UI //////////
    /// the inventory is more complex than the average menu, so it needs its own whole extra script!

    // stores the inventory
    private Inventory inventory;
    public GameObject itemMenu;

    // get the menu manager
    private MenuManager menuManager;

    // stores the inventory slots
    private ItemSlot[] slots;

    // states of the menu
    public enum ItemState
    {
        CLOSED,
        OPENED,
        CHOOSING_ITEM,
        ITEM_SELECTED
    }

    public ItemState state = ItemState.CLOSED;

    // Start is called before the first frame update
    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallBack += UpdateUI;

        menuManager = GetComponent<MenuManager>();

        slots = itemMenu.GetComponentsInChildren<ItemSlot>();
    }

    // updates every frame
    private void Update()
    {
        menuManager.CheckInput();

        switch (state)
        {
            case ItemState.CLOSED:
                {
                    if (itemMenu.activeSelf)
                    {
                        state = ItemState.OPENED;
                    }

                    break;
                }
            case ItemState.OPENED:
                {
                    if (!itemMenu.activeSelf)
                    {
                        state = ItemState.CLOSED;
                    }

                    CycleInventory();

                    break;
                }
            case ItemState.CHOOSING_ITEM:
                {
                    if (!itemMenu.activeSelf)
                    {
                        state = ItemState.CLOSED;
                    }

                    break;
                }
            case ItemState.ITEM_SELECTED:
                {
                    if (!itemMenu.activeSelf)
                    {
                        state = ItemState.CLOSED;
                    }

                    break;
                }
        }
    }

    // cycling through the inventory
    private void CycleInventory()
    {
        menuManager.SelectItem(0.6f, slots);
        menuManager.ItemSlotColorMenu(slots);

        if (Input.GetButtonDown("Y_Button"))
        {
            slots[menuManager.buttonSelect].UseItem();
            slots[menuManager.buttonSelect].ClearSlot();
        }
    }

    // updates what shows up in the inventory
    private void UpdateUI()
    {
        Debug.Log("UPDATING MENU");

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
