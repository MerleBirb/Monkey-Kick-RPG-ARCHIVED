using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    ////////// INVENTORY MANAGER //////////
    /// this script handles and holds the player's inventory

    // store the list of items
    public List<Item> items = new List<Item>();

    #region Singleton
    // singleton for the inventory
    public static Inventory instance;

    // singleton pattern
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of the Inventory has been found!");
            return;
        }

        instance = this;
    }
    #endregion

    // delegate setup for inventory
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;

    // adds the item to the inventory
    public void Add (Item item)
    {
        items.Add(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }

    // removes the item from the inventory
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallBack != null)
        {
            onItemChangedCallBack.Invoke();
        }
    }
}
