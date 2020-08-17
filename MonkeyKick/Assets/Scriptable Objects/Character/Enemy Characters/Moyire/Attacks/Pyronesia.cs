using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pyronesia", menuName = "Enemy Attacks/Moyire/Pyronesia")]
public class Pyronesia : EnemySkill
{
    ////////// PYRONESIA //////////
    // the script for the Moyire's fire attack

    private string skillType = "Fire";
    private int baseDamage = 2;
    public GameObject fireballPrefab;
    private GameObject fireball;
    public GameObject[] hitEffects;
    private bool beenCreated = false;

    private enum FireballStates
    {
        PREPARING,
        FIRING,
        FIRED
    }

    private FireballStates state = FireballStates.PREPARING;

    // deals damage
    public void TriggerDamage(GameObject user, float damageMult, int soundChoice)
    {
        var enemyTarget = enemy.target.GetComponent<PlayerBattleScript>();

        audioSource = enemy.audioSource;

        int clampValue = enemyTarget.charStats.maxHP;
        float damageScale = (float)enemy.charStats.strength * damageMult;
        int damage = baseDamage + Mathf.Clamp(Mathf.RoundToInt(damageScale), 1, clampValue);

        audioSource.clip = hitSound[soundChoice];
        audioSource.Play();

        Debug.Log("Damage: " + damage);
        enemyTarget.currentHP -= damage;
        enemyTarget.anim.Play("Hurt");
        enemyTarget.damaged = true;
    }

    // the function to do the attack
    public override void SkillAction(EnemyBattleScript user)
    {
        enemy = user;

        switch(state)
        {
            case FireballStates.PREPARING:
                {
                    if (enemy.state == EnemyBattleScript.BattleStates.ACTION)
                    {
                        beenCreated = false;
                        enemy.target.GetComponent<PlayerBattleScript>().state = PlayerBattleScript.BattleStates.COUNTER;
                        state = FireballStates.FIRING;
                    }

                    break;
                }
            case FireballStates.FIRING:
                {
                    if (!beenCreated)
                    {
                        fireball = Instantiate(fireballPrefab, enemy.transform);
                        beenCreated = true;
                    }
                    
                    state = FireballStates.FIRED;                    

                    break;
                }
            case FireballStates.FIRED:
                {
                    if (fireball == null)
                    {
                        enemy.target.GetComponent<PlayerBattleScript>().state = PlayerBattleScript.BattleStates.WAIT;
                        enemy.state = EnemyBattleScript.BattleStates.RESET;

                        state = FireballStates.PREPARING;
                    }

                    break;
                }
        }
    }
}
