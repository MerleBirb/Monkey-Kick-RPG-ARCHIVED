//===== PLAYABLE CONTROLLER =====//
/*
5/19/21
Description:
- What the PlayerController and the PartyMemberController scripts derive from.

*/

using UnityEngine;

public abstract class PlayableController : MonoBehaviour
{
    public PlayableInfo stats;

    public virtual void Start()
    {
        if (!stats.isAlive)
        {
            this.gameObject.SetActive(false);
        }
    }
}
