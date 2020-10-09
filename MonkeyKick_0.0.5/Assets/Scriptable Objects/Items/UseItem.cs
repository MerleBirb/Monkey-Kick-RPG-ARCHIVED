using UnityEngine;

public class UseItem : MonoBehaviour
{
    // store item that is gonna be used
    public Item item;
    [SerializeField]
    private bool inRange;

    // the player stored
    [SerializeField]
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            item.audioSource = GetComponent<AudioSource>();
            item.audioSource.clip = item.itemSound[1];
            inRange = true;
            player = other.gameObject;
        }    
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            player = null;
        }
    }

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetButtonDown("Y_Button"))
            {
                Debug.Log("PLAYER HEALED!");
                item.audioSource.Play();
                item.UseItem(player.GetComponent<PlayerBattleScript>());
            }
        }
    }
}
