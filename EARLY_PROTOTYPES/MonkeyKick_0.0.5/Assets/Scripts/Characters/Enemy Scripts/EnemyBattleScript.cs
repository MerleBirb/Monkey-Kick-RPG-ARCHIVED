using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBattleScript : MonoBehaviour
{
    ////////// THE TURN BASED SYSTEM //////////
    // store the turn based system and the game manager
    public GameObject battleSystem;
    public TurnSystemScript turnSystem;
    public TurnClass turnClass;
    public EnemySkillList moveList;

    // store the booleans and other variables from the turn based system
    public bool isTurn = false;
    public int playerChosen = 0;
    public GameObject target;
    public Vector3 storeTargetPos;
    public Vector3 battlePos;
    private Rigidbody rb;
    public bool damaged;

    // making some fighting states
    public enum BattleStates
    {
        ENTER_BATTLE,
        WAIT,
        SELECT_ACTION,
        CHOOSE_TARGET,
        MOVE_TO_TARGET,
        ACTION,
        RETURN,
        RESET
    }

    public BattleStates state;

    ////////// CHARACTER ATTRIBUTES //////////
    // store the stats and enemy info
    public Character charStats;

    // store the UI of the character
    public Text nameUI;
    public Text HPText;
    public Image HPBarBG;
    public Image HPBar;

    // the current health of the enemy
    public int currentHP = 0;

    ////////// ANIMATIONS //////////
    // store the animations
    public Animator anim;
    [SerializeField]
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;

    // sounds
    public AudioSource audioSource;

    // store a timer
    float lerpTimer = 0.0f;
    float currentLerpTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        turnSystem = battleSystem.GetComponent<TurnSystemScript>();
        anim = GetComponentInChildren<Animator>();

        sr = GetComponentInChildren<SpriteRenderer>();
        matDefault = sr.material;

        foreach (TurnClass tc in turnSystem.charList)
        {
            if (tc.character.name == gameObject.name)
            {
                turnClass = tc;
            }
        }

        state = BattleStates.ENTER_BATTLE;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.inBattle)
        {
            isTurn = turnClass.isTurn;
            
            if (currentHP < 0)
            {
                currentHP = 0;
            }

            if (currentHP > charStats.maxHP)
            {
                currentHP = charStats.maxHP;
            }

            if (currentHP <= 0)
            {
                foreach (GameObject player in turnSystem.playerGroup)
                {
                    player.GetComponent<PlayerBattleScript>().currentXP += charStats.XPValue;
                }

                turnSystem.allCharacterGroup.Remove(gameObject);
                turnSystem.enemyGroup.Remove(gameObject);
                Destroy(gameObject);
            }

            switch (state)
            {
                case BattleStates.ENTER_BATTLE:
                    {
                        battlePos = new Vector3((float)System.Math.Round(battlePos.x, 1), (float)System.Math.Round(battlePos.y, 1),
                            (float)System.Math.Round(battlePos.z, 1));

                        //transform.position = battlePos;                       

                        rb.velocity = Vector3.zero;

                        if (rb.velocity == Vector3.zero)
                        {
                            state = BattleStates.WAIT;
                        }

                        break;
                    }
                case BattleStates.WAIT:
                    {
                        if (isTurn)
                        {
                            state = BattleStates.SELECT_ACTION;
                        }

                        break;
                    }
                case BattleStates.SELECT_ACTION:
                    {
                        state = BattleStates.CHOOSE_TARGET;

                        break;
                    }
                case BattleStates.CHOOSE_TARGET:
                    {
                        playerChosen = Random.Range(0, turnSystem.playerGroup.Count);
                        currentLerpTimer = 0.0f;
                        target = turnSystem.playerGroup[playerChosen];
                        StoreAttack();

                        state = BattleStates.ACTION;

                        break;
                    }
                case BattleStates.ACTION:
                    {
                        moveList.skills[0].SkillAction(this);

                        break;
                    }
                case BattleStates.RETURN:
                    {
                        currentLerpTimer += Time.deltaTime;

                        if (currentLerpTimer > lerpTimer)
                        {
                            currentLerpTimer = lerpTimer;
                        }

                        float percentage = currentLerpTimer / lerpTimer;
                        transform.position = Vector3.Lerp(transform.position, new Vector3(battlePos.x, transform.position.y, battlePos.z),
                            percentage);

                        if ((float)System.Math.Round(transform.position.x, 1) == (float)System.Math.Round(battlePos.x, 1))
                        {
                            //transform.position = battlePos;
                            state = BattleStates.RESET;
                        }

                        break;
                    }
                case BattleStates.RESET:
                    {
                        isTurn = false;
                        turnClass.isTurn = isTurn;
                        turnClass.wasTurnPrev = true;

                        if (!isTurn)
                        {
                            state = BattleStates.WAIT;
                        }

                        break;
                    }
            }

            ManageUI();

            if (damaged)
            {
                sr.material = matWhite;
                Invoke("ResetMaterial", 0.1f);
                damaged = false;
            }
        }
    }

    // update the UI with the player info
    void ManageUI()
    {
        //int HPDivision = 50;
        //int energyDivision = 50;

        nameUI.text = charStats.name + "  Lv. " + charStats.level;
        HPText.text = "HP: " + currentHP + " / " + charStats.maxHP;

        //HPBarBG.transform.localScale = new Vector3(1 + (charStats.maxHP / HPDivision), 1, 1);
        //HPBar.transform.localScale = new Vector3(1 + (charStats.maxHP / HPDivision), 1, 1);
        HPBar.fillAmount = (float)currentHP / charStats.maxHP;
    }

    // store the attack currently done
    void StoreAttack()
    {
        StoreActions action = new StoreActions();
        action.attackerName = turnClass.charName;
        action.attacker = gameObject;
        action.attackerTarget = turnSystem.playerGroup[playerChosen];
        turnSystem.CollectAction(action);
    }

    // end the turn with this function
    void EndTurn()
    {
        isTurn = false;
        turnClass.isTurn = isTurn;
        turnClass.wasTurnPrev = true;
    }

    // damage calculator for enemies
    public void DamagePlayer()
    {
        int basePower = 1;
        target.GetComponent<PlayerBattleScript>().currentHP -= turnSystem.DealDamage(basePower, charStats.strength, charStats.level,
            target.GetComponent<PlayerBattleScript>().charStats.defense,
            target.GetComponent<PlayerBattleScript>().charStats.level, target.GetComponent<PlayerBattleScript>().currentHP);
    }

    // ignore normal collisions with players
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    // damage flash
    private void ResetMaterial()
    {
        sr.material = matDefault;
    }
}

// Store the move list in this class
[System.Serializable]
public class EnemySkillList
{
    // store weapon skills in here
    public List<EnemySkill> skills = new List<EnemySkill>();
}
