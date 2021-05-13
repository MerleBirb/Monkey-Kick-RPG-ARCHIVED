//===== DON'T DESTROY ON LOAD =====//
/*
5/12/21
Description: 
- Place on an object to add the 'DontDestroyOnLoad' function to it. That's it.

*/


using UnityEngine;

public class DDOL : MonoBehaviour
{
    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
