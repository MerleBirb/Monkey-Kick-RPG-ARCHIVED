using UnityEngine;

[CreateAssetMenu(fileName = "New HP Heal Item", menuName = "Items/HP Heal Item")]
public class HPRestoreItem : Item
{
    [SerializeField]
    private float HPRestorePercentage = 33;

    // item effect
    public override void UseItem(PlayerBattleScript user)
    {
        player = user;
        player.currentHP += (int)(player.charStats.maxHP * (HPRestorePercentage / 100.0f));
        AudioSource.PlayClipAtPoint(itemSound[0], GameManager.mainParty[0].transform.position);
    }
}
