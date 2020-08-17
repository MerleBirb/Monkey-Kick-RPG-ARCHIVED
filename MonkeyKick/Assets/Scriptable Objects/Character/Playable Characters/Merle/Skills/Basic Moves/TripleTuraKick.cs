using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Triple Tura-Kick", menuName = "Player Attacks/Merle/Triple Tura-Kick")]
public class TripleTuraKick : Skill
{
    ////////// TRIPLE TURA-KICK //////////
    // the script for merle's first basic attack. the name is a pun of the turaco bird

    private string skillType = "Kinetic";
    public int baseDamage = 2;
    private float targetSpace = 1f;
    public GameObject[] hitEffects;
    private bool hasKicked = false;
    private float moveTimer = 0.0f;
    private float currentMoveTimer = 0.0f;

    public enum KickStates
    { 
        SKILL_START,
        MOVE_TO_ENEMY,
        KICK_1,
        SKILL_END
    }

    public KickStates state = KickStates.SKILL_START;

    // the function to do the attack
    public override void SkillAction(PlayerBattleScript user)
    {
        player = user;

        switch (state)
        {
            case KickStates.SKILL_START:
                {
                    hasKicked = false;

                    if (player.anim.GetBool("TripleKick") == false)
                    {
                        state = KickStates.MOVE_TO_ENEMY;
                    }

                    break;
                }
            case KickStates.MOVE_TO_ENEMY:
                {
                    Vector3 targetPos = new Vector3(player.target.transform.position.x - targetSpace,
                        player.transform.position.y, player.target.transform.position.z);

                    if (player.transform.position != targetPos)
                    {
                        float lerpSpeed = 5.0f;
                        moveTimer = Mathf.Abs(player.transform.position.x - player.target.transform.position.x) * lerpSpeed;

                        currentMoveTimer += Time.deltaTime;

                        if (currentMoveTimer > moveTimer)
                        {
                            currentMoveTimer = moveTimer;
                        }

                        float totalDistance = Vector3.Distance(player.battlePos, targetPos);
                        float percentage = currentMoveTimer / totalDistance;

                        player.transform.position = Vector3.Lerp(player.transform.position, targetPos, percentage);
                    }
                    else
                    {
                        state = KickStates.KICK_1;
                    }

                    break;
                }
            case KickStates.KICK_1:
                {                   
                    player.attackTimer -= Time.deltaTime;
                    currentMoveTimer = 0.0f;

                    if (player.attackTimer < 0.0f)
                    {
                        player.attackTimer = 0.0f;
                    }

                    if (!player.isCreated)
                    {
                        player.timedButton = Instantiate(player.button, player.battleMenu.transform);
                        player.isCreated = true;
                    }

                    TriggerIcon(player.gameObject, player.timedButton, 0, player.attackTimer, player.attackResetTimer, 2.0f, 0f, -100f);

                    if (player.anim.GetBool("TripleKick") == false)
                    {
                        player.anim.SetBool("TripleKick", true);
                    }

                    if (Input.GetButtonDown("X_Button") && !hasKicked)
                    {
                        hasKicked = true;

                        player.timedText = Instantiate(player.text, player.battleMenu.transform);

                        GameObject hitEffect = Instantiate(hitEffects[0], player.target.transform);

                        Destroy(player.timedText, 1.0f);
                        Destroy(player.timedButton);
                        Destroy(hitEffect, 1.0f);

                        if ((player.attackTimer > (player.attackResetTimer / 2.0f)) && (player.attackTimer <= player.attackResetTimer))
                        {
                            TriggerDamage(player.gameObject, 0.4f, 0);
                            TriggerText(player.gameObject, player.timedText, 1, 0f, -200f);
                            currentMoveTimer = 0.0f;
                        }
                        else if ((player.attackTimer > (player.attackResetTimer / 4.0f)) && (player.attackTimer <= (player.attackResetTimer / 2.0f)))
                        {
                            TriggerDamage(player.gameObject, 0.8f, 1);
                            TriggerText(player.gameObject, player.timedText, 2, 0f, -200f);
                            currentMoveTimer = 0.0f;
                        }
                        else if ((player.attackTimer > 0.0f) && (player.attackTimer <= (player.attackResetTimer / 4.0f)))
                        {
                            TriggerDamage(player.gameObject, 1.2f, 2);
                            TriggerText(player.gameObject, player.timedText, 3, 0f, -200f);
                            currentMoveTimer = 0.0f;
                        }
                        else
                        {
                            TriggerDamage(player.gameObject, 0.1f, 1);
                            TriggerText(player.gameObject, player.timedText, 0, 0f, -200f);
                            currentMoveTimer = 0.0f;
                        }
                    }

                    if (player.attackTimer <= 0.0f)
                    {
                        

                        if (!hasKicked)
                        {
                            player.timedText = Instantiate(player.text, player.battleMenu.transform);
                            TriggerText(player.gameObject, player.timedText, 0, 0f, -200f);

                            Destroy(player.timedText, 1.0f);
                            Destroy(player.timedButton);
                        }

                        state = KickStates.SKILL_END;
                    }

                    break;
                }
            case KickStates.SKILL_END:
                {
                    if (!GameManager.inBattle)
                    {
                        state = KickStates.SKILL_START;
                    }

                    if (player.transform.position != player.battlePos)
                    {
                        float lerpSpeed = 5.0f;
                        moveTimer = Mathf.Abs(player.battlePos.x - player.transform.position.x) * lerpSpeed;

                        currentMoveTimer += Time.deltaTime;

                        if (currentMoveTimer > moveTimer)
                        {
                            currentMoveTimer = moveTimer;
                        }

                        float totalDistance = Vector3.Distance(player.battlePos, player.transform.position);
                        float percentage = currentMoveTimer / totalDistance;

                        player.transform.position = Vector3.Lerp(player.battlePos, player.transform.position, percentage);
                    }
                    else
                    {                    
                        state = KickStates.SKILL_START;                      
                    }                  

                    break;
                }
            default:
                {
                    if (!GameManager.inBattle)
                    {
                        state = KickStates.SKILL_START;
                    }

                    if (player != TurnSystemScript.selectedCharacter)
                    {
                        state = KickStates.SKILL_START;
                    }

                    break;
                }
        }     
    }

    // deals damage
    public void TriggerDamage(GameObject user, float damageMult, int soundChoice)
    {
        var playerTarget = player.target.GetComponent<EnemyBattleScript>();

        audioSource = player.audioSource;

        int clampValue = playerTarget.charStats.maxHP;
        float damageScale = (float)player.charStats.strength * damageMult;
        int damage = baseDamage + Mathf.Clamp(Mathf.RoundToInt(damageScale), 1, clampValue);

        audioSource.clip = hitSound[soundChoice];
        audioSource.Play();

        Debug.Log("Damage: " + damage);
        playerTarget.currentHP -= damage;
        playerTarget.anim.Play("Hurt");
        playerTarget.damaged = true;
    }

    // helps reset values
    public override void OnBeforeSerialize() { }
    public override void OnAfterDeserialize()
    {
        Reset();
    }

    private void Reset()
    {
        state = KickStates.SKILL_START;
        baseDamage = 2;
        moveTimer = 0.0f;
        currentMoveTimer = 0.0f;
        hasKicked = false;
    }

    public override string ToString()
    {
        return $@"Runtime: {state} {baseDamage} {moveTimer} {currentMoveTimer} {hasKicked}
        Initial: {KickStates.SKILL_START} {2} {0.0f} {0.0f} {false}";
    }
}
