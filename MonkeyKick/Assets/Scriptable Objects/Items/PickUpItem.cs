using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    // store item that is gonna be used
    public Item item;
    [SerializeField]
    private bool inRange;

    // the player stored
    [SerializeField]
    private GameObject player;

    // when entering its range
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
            player = other.gameObject;
        }
    }

    // when exiting its range
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            player = null;
        }
    }

    // steps every frame
    private void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Y_Button"))
            {
                Debug.Log("Picked up " + item.itemName + "!");
                AudioSource.PlayClipAtPoint(item.itemSound[1], transform.position);
                Inventory.instance.Add(item);
                Destroy(gameObject);
            }
        }
    }
}
