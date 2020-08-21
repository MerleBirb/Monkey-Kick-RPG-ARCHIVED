using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerleAnimations : MonoBehaviour
{
    //////////// ANIMATION EVENTS FOR MERLE ////////////
    private Animator anim;
    public PlayerBattleScript playerBattle;

    // start is called on the first frame
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // toggle Merle's kick attack off
    public void MerleTripleKickFinish()
    {
        anim.SetBool("TripleKick", false);
    }

    //// hurt animation
    //public void MerleHurtFinish()
    //{
    //    anim.SetBool("GotHurt", false);
    //}
}
