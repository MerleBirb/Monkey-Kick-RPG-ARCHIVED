using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBattleScript : MonoBehaviour
{
    ////////// THE TURN BASED SYSTEM //////////
    // store the turn based system and the game manager
    public GameObject battleSystem;
    public TurnSystemScript turnSystem;
    public TurnClass turnClass;
    public List<Skill> basicSkills = new List<Skill>();

    // store the booleans and other variables from the turn based system
    public bool isTurn = false;
    public int actionSelect = 0;
    public int enemyChosen = 0;
    public Vector3 battlePos;
    private bool stickPressed = false;
    public GameObject target;
    private Rigidbody rb;

    // making some fighting states
    public enum BattleStates
    {
        ENTER_BATTLE,
        WAIT,
        SELECT_ACTION,
        CHOOSE_TARGET,
        ACTION,
        RETURN,
        COUNTER,
        RESET
    }

    public BattleStates state;

    ////////// CHARACTER ATTRIBUTES //////////
    // store the stats and player info
    public Character charStats;

    // store the UI of the character
    public Text nameUI;
    public Text HPText;
    public Text energyText;
    public Image HPBarBG;
    public Image HPBar;
    public Image energyBarBG;
    public Image energyBar;

    // timed button stuff
    [SerializeField]
    private bool isGrounded = false;
    public float jumpHeight = 5f;
    public AudioSource[] audioSources;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float footRadius = 0.25f;
    public Image button;
    public Text text;
    public Image timedButton;
    public Text timedText;
    public bool isCreated = false;
    public AudioSource audioSource;
    public bool damaged = false;

    // the current health, energy, and amount of xp of the player
    public int currentHP, currentEnergy, currentXP = 0;

    ////////// MOVEMENTS AND ANIMATIONS //////////
    // store the battle system
    public BattleUIScript battleMenu;

    // store the animations
    public Animator anim;
    [SerializeField]
    private Material matWhite, matDefault;
    SpriteRenderer sr;

    // store a attack timer
    public float attackTimer;
    public float attackResetTimer;

    ////////// CONTROLS //////////
    // store the joysticks used
    private float moveX, moveZ;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        attackResetTimer = attackTimer;
        turnSystem = battleSystem.GetComponent<TurnSystemScript>();
        anim = GetComponentInChildren<Animator>();

        sr = GetComponentInChildren<SpriteRenderer>();
        matDefault = sr.material;

        foreach (TurnClass tc in turnSystem.charList)
        {
            if(tc.character.name == gameObject.name)
            {
                turnClass = tc;
            }
        }

        state = BattleStates.ENTER_BATTLE;
    }

    // Update is called once per frame
    private void Update()
    {
        CheckGround();

        if (GameManager.inBattle)
        {           

            isTurn = turnClass.isTurn;

            switch (state)
            {
                case (BattleStates.ENTER_BATTLE):
                    {
                        attackTimer = attackResetTimer;
                        actionSelect = 0;
                        anim.speed = 1;

                        battlePos = new Vector3((float)System.Math.Round(battlePos.x, 1), (float)System.Math.Round(battlePos.y, 1),
                            (float)System.Math.Round(battlePos.z, 1));
                        
                        transform.position = battlePos;

                        rb.velocity = Vector3.zero;

                        if (rb.velocity == Vector3.zero)
                        {
                            state = BattleStates.WAIT;
                        }

                        break;
                    }
                case (BattleStates.WAIT):
                    {
                        if (isTurn)
                        {
                            state = BattleStates.SELECT_ACTION;
                        }

                        break;
                    }
                case (BattleStates.SELECT_ACTION):
                    {
                        if (moveZ < -0.6)
                        {
                            if (!stickPressed)
                            {
                                actionSelect++;
                                stickPressed = true;
                            }
                        }
                        else if (moveZ > 0.6)
                        {
                            if (!stickPressed)
                            {
                                actionSelect--;
                                stickPressed = true;
                            }
                        }
                        else if (moveX > 0.6)
                        {
                            if (!stickPressed)
                            {
                                actionSelect += 2;
                                stickPressed = true;
                            }
                        }
                        else if (moveX < -0.6)
                        {
                            if (!stickPressed)
                            {
                                actionSelect -= 2;
                                stickPressed = true;
                            }
                        }
                        else
                        {
                            stickPressed = false;
                        }

                        if (actionSelect > battleMenu.battleMenuUI.Count - 1)
                        {
                            actionSelect = 0;
                        }
                        else if (actionSelect < 0)
                        {
                            actionSelect = battleMenu.battleMenuUI.Count - 1;
                        }

                        if (Input.GetButtonDown("A_Button"))
                        {
                            switch (actionSelect)
                            {
                                case 0:
                                    {
                                        state = BattleStates.CHOOSE_TARGET;

                                        break;
                                    }
                            }
                        }

                        break;
                    }
                case (BattleStates.CHOOSE_TARGET):
                    {
                        if (moveZ < -0.6 || moveX > 0.6)
                        {
                            if (!stickPressed)
                            {
                                enemyChosen++;
                                stickPressed = true;
                            }
                        }
                        else if (moveZ > 0.6 || moveX < -0.6)
                        {
                            if (!stickPressed)
                            {
                                enemyChosen--;
                                stickPressed = true;
                            }
                        }
                        else
                        {
                            stickPressed = false;
                        }

                        if (enemyChosen > turnSystem.enemyGroup.Count - 1)
                        {
                            enemyChosen = 0;
                        }
                        else if (enemyChosen < 0)
                        {
                            enemyChosen = turnSystem.enemyGroup.Count - 1;
                        }

                        if (Input.GetButtonDown("A_Button"))
                        {
                            target = turnSystem.enemyGroup[enemyChosen];
                            StoreAttack();

                            state = BattleStates.ACTION;
                        }

                        if (Input.GetButtonDown("B_Button"))
                        {
                            state = BattleStates.SELECT_ACTION;
                            isCreated = false;
                        }

                        break;
                    }
                case (BattleStates.ACTION):
                    {
                        basicSkills[0].SkillAction(this);

                        break;
                    }
                case (BattleStates.RETURN):
                    {
                        isCreated = false;
                        
                        if ((float)System.Math.Round(transform.position.x, 1) == (float)System.Math.Round(battlePos.x, 1))
                        {
                            attackTimer = attackResetTimer;
                            state = BattleStates.RESET;
                        }

                        break;
                    }
                case (BattleStates.RESET):
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
                case (BattleStates.COUNTER):
                    {
                        if (isGrounded)
                        {
                            if (Input.GetButtonDown("A_Button"))
                            {
                                rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
                                audioSources[0].Play();
                                isGrounded = false;
                            }
                        }

                        break;
                    }
            }

            ManageUI();
            CheckInput();
        }

        if (damaged)
        {
            sr.material = matWhite;
            Invoke("ResetMaterial", 0.1f);
            damaged = false;
        }
    }

    // store the attack currently done
    private void StoreAttack()
    {
        StoreActions action = new StoreActions();
        action.attackerName = turnClass.charName;
        action.attacker = gameObject;
        action.attackerTarget = turnSystem.enemyGroup[enemyChosen];
        turnSystem.CollectAction(action);
    }

    // update the UI with the player info
    private void ManageUI()
    {
        nameUI.text = charStats.name + "  Lv. " + charStats.level;
        HPText.text = "HP: " + currentHP + " / " + charStats.maxHP;
        energyText.text = "Energy: " + currentEnergy + " / " + charStats.maxEnergy;

        HPBar.fillAmount = (float)currentHP / charStats.maxHP;
        energyBar.fillAmount = (float)currentEnergy / charStats.maxEnergy;
    }

    // checks to see if the player pressed any buttons
    private void CheckInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
    }

    // ignore normal collision with enemies
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }

    // damage flash
    private void ResetMaterial()
    {
        sr.material = matDefault;
    }

    // checks to see if the feet touch ground :D foot check foot check hahah
    private void CheckGround()
    {
        Collider[] surfaces = Physics.OverlapSphere(groundCheck.position, footRadius, groundLayer);

        if (surfaces.Length > 0)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}

// Store the move list in this class
[System.Serializable]
public class SkillList
{
    // store weapon skills in here
    
}