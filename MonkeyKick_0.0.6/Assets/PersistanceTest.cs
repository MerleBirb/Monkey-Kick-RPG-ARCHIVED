using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersistanceTest : MonoBehaviour
{
    bool pressButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponentInChildren<Text>().text = "HP: " + GameManager.Instance.PlayerParty[0].currentHP.ToString() + "/" +
            GameManager.Instance.PlayerParty[0].maxHP.Value.ToString();
    }
}
