using UnityEngine;

public abstract class Item : ScriptableObject
{
    ////////// ITEM CLASS //////////
    /// this scriptable object is pretty self explanatory if you ask me. just an item. doesnt take a genius to figure that out

    ////////// DESCRIPTION //////////
    // describes the item
    public string itemName = "New Item";
    [TextArea(10, 15)]
    public string itemDescription = "This item does something.";
    public string itemType;
    public int cost = 2;
    public Sprite image;
    public AudioSource audioSource;
    public AudioClip[] itemSound;
    public PlayerBattleScript player;

    // item effect
    public abstract void UseItem(PlayerBattleScript user);

}
