using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    ////////// MENU MANAGER //////////
    // store the UI
    public GameObject menu;
    public GameObject statusMenu;
    public GameObject itemMenu;
    public GameObject saveMenu;
    public GameObject[] menuOptions;
    public Text[] statTexts;
    public GameObject[] saveOptions;
    public Text saveText;
    
    // store important objects
    public PlayerBattleScript playerStats;
    public SaveAndLoad saveSystem;

    // inputs
    private float moveX;
    private float moveZ;
    private bool stickPressed;
    public int buttonSelect;
    private int lastButtonSelect = 0;
    private int lastItemSelect = 0;

    // sounds for the menu
    private AudioSource audioSource;
    public AudioClip[] sounds;

    // menu navigation enums
    public enum Menus
    {
        MENU_UNOPENED,
        GENERAL,
        STATUS,
        EQUIPMENT,
        ITEMS,
        MAP,
        SETTINGS,
        SAVE
    }

    // sound enum
    public enum Sounds
    {
        OPEN,
        CLOSE,
        SELECT,
        BACK,
        SAVE
    }

    public static Menus state;

    // first frame
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        menu.SetActive(false);
        statusMenu.SetActive(false);
        saveMenu.SetActive(false);

        stickPressed = false;
    }

    // Update is called once per frame
    private void Update()
    {        
        CheckInput();       

        switch(state)
        {
            case (Menus.MENU_UNOPENED):
                {
                    OpenMenu();

                    break;
                }
            case (Menus.GENERAL):
                {
                    SelectOption(0.6f, menuOptions);
                    TextColorMenu(menuOptions);

                    if (Input.GetButtonDown("A_Button"))
                    {
                        switch (buttonSelect)
                        {
                            case 0:
                                {
                                    PlaySound(sounds, Sounds.OPEN);
                                    state = Menus.STATUS;
                                    lastButtonSelect = buttonSelect;
                                    buttonSelect = 0;

                                    break;
                                }
                            case 1:
                                {
                                    PlaySound(sounds, Sounds.OPEN);
                                    state = Menus.EQUIPMENT;
                                    lastButtonSelect = buttonSelect;
                                    buttonSelect = 0;

                                    break;
                                }
                            case 2:
                                {
                                    PlaySound(sounds, Sounds.OPEN);
                                    state = Menus.ITEMS;
                                    lastButtonSelect = buttonSelect;
                                    buttonSelect = lastItemSelect;

                                    break;
                                }
                            case 3:
                                {
                                    PlaySound(sounds, Sounds.OPEN);
                                    state = Menus.MAP;
                                    lastButtonSelect = buttonSelect;
                                    buttonSelect = 0;

                                    break;
                                }
                            case 4:
                                {
                                    PlaySound(sounds, Sounds.OPEN);
                                    state = Menus.SETTINGS;
                                    lastButtonSelect = buttonSelect;
                                    buttonSelect = 0;

                                    break;
                                }
                            case 5:
                                {
                                    PlaySound(sounds, Sounds.OPEN);
                                    saveOptions[0].SetActive(true);
                                    saveOptions[1].SetActive(true);
                                    saveText.text = "SAVE GAME?";

                                    state = Menus.SAVE;
                                    lastButtonSelect = buttonSelect;
                                    buttonSelect = 0;

                                    break;
                                }
                        }
                    }

                    CloseMenu();

                    break;
                }
            case Menus.STATUS:
                {
                    StatusMenu();
                    CloseMenu();

                    break;
                }
            case Menus.ITEMS:
                {
                    ItemMenu();
                    CloseMenu();
                    break;
                }
            case Menus.SAVE:
                {
                    SaveMenu();
                    CloseMenu();

                    break;
                }
        }
    }

    // selecting your option
    public void SelectOption(float dz, GameObject[] options)
    {
        if (moveZ < -dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect++;
                stickPressed = true;
            }
        }
        else if (moveZ > dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect--;
                stickPressed = true;
            }
        }
        else if (moveX > dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect++;
                stickPressed = true;
            }
        }
        else if (moveX < -dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect--;
                stickPressed = true;
            }
        }
        else
        {
            stickPressed = false;
        }

        if (buttonSelect > options.Length - 1)
        {
            buttonSelect = 0;
        }
        else if (buttonSelect < 0)
        {
            buttonSelect = options.Length - 1;
        }
    }

    // selecting your item
    public void SelectItem(float dz, ItemSlot[] inventory)
    {
        if (moveZ < -dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect++;
                stickPressed = true;
            }
        }
        else if (moveZ > dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect--;
                stickPressed = true;
            }
        }
        else if (moveX > dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect++;
                stickPressed = true;
            }
        }
        else if (moveX < -dz)
        {
            if (!stickPressed)
            {
                PlaySound(sounds, Sounds.SELECT);
                buttonSelect--;
                stickPressed = true;
            }
        }
        else
        {
            stickPressed = false;
        }

        if (buttonSelect > inventory.Length - 1)
        {
            buttonSelect = 0;
        }
        else if (buttonSelect < 0)
        {
            buttonSelect = inventory.Length - 1;
        }
    }

    // changes color of selected options
    private void TextColorMenu(GameObject[] options)
    {
        options[buttonSelect].GetComponent<Text>().color = Color.white;
        List<GameObject> otherOptions = options.Where((t) => t != options[buttonSelect]).ToList();

        if (otherOptions != null)
        {
            for (int i = 0; i < otherOptions.Count; i++)
            {
                otherOptions[i].GetComponent<Text>().color = Color.black;
            }
        }
    }

    // changes color of selected options
    public void ItemSlotColorMenu(ItemSlot[] inventory)
    {
        inventory[buttonSelect].GetComponent<Image>().color = Color.white;
        List<ItemSlot> otherInventory = inventory.Where((t) => t != inventory[buttonSelect]).ToList();

        if (otherInventory != null)
        {
            for (int i = 0; i < otherInventory.Count; i++)
            {
                otherInventory[i].GetComponent<Image>().color = Color.black;
            }
        }
    }

    // open the menu
    public void OpenMenu()
    {
        if (Input.GetButtonDown("START_Button"))
        {
            if (!menu.activeSelf)
            {
                PlaySound(sounds, Sounds.OPEN);
                menu.SetActive(true);
                state = Menus.GENERAL;
            }
        }
    }

    // close the menu
    public void CloseMenu()
    {
        if (Input.GetButtonDown("START_Button"))
        {
            buttonSelect = lastButtonSelect;

            if (menu.activeSelf)
            {
                PlaySound(sounds, Sounds.CLOSE);
                menu.SetActive(false);
                state = Menus.MENU_UNOPENED;
            }

            if (statusMenu.activeSelf)
            {
                PlaySound(sounds, Sounds.CLOSE);
                statusMenu.SetActive(false);
                state = Menus.MENU_UNOPENED;
            }

            if (itemMenu.activeSelf)
            {
                PlaySound(sounds, Sounds.CLOSE);
                itemMenu.SetActive(false);
                lastItemSelect = buttonSelect;
                state = Menus.MENU_UNOPENED;
            }

            if (saveMenu.activeSelf)
            {
                PlaySound(sounds, Sounds.CLOSE);
                saveMenu.SetActive(false);
                state = Menus.MENU_UNOPENED;
            }
        }

        if (Input.GetButtonDown("B_Button"))
        {
            buttonSelect = lastButtonSelect;

            if (menu.activeSelf)
            {
                PlaySound(sounds, Sounds.CLOSE);
                menu.SetActive(false);
                state = Menus.MENU_UNOPENED;
            }

            if (statusMenu.activeSelf)
            {
                PlaySound(sounds, Sounds.BACK);
                statusMenu.SetActive(false);
                menu.SetActive(true);
                state = Menus.GENERAL;
            }

            if (itemMenu.activeSelf)
            {
                PlaySound(sounds, Sounds.BACK);
                itemMenu.SetActive(false);
                menu.SetActive(true);
                state = Menus.GENERAL;
            }

            if (saveMenu.activeSelf)
            {
                if (saveText.text != "SAVING GAME...")
                {
                    PlaySound(sounds, Sounds.BACK);
                    saveMenu.SetActive(false);
                    menu.SetActive(true);
                    state = Menus.GENERAL;
                }
            }
        }
    }

    // display the text
    private void StatusMenu()
    {
        menu.SetActive(false);
        statusMenu.SetActive(true);

        statTexts[0].text = playerStats.charStats.fullName;
        statTexts[1].text = "Lv. " + playerStats.charStats.level;
        statTexts[2].text = "Exp: " + playerStats.currentXP + " / " + playerStats.charStats.maxXP;
        statTexts[3].text = "HP: " + playerStats.currentHP + " / " + playerStats.charStats.maxHP;
        statTexts[4].text = "EP: " + playerStats.currentEnergy + " / " + playerStats.charStats.maxEnergy;
        statTexts[5].text = "Strength: " + playerStats.charStats.strength;
        statTexts[6].text = "Intelligence: " + playerStats.charStats.intelligence;
        statTexts[7].text = "Defense: " + playerStats.charStats.defense;
        statTexts[8].text = "Speed: " + playerStats.charStats.speed;
        statTexts[9].text = "Luck: " + playerStats.charStats.luck;
    }

    // the item menu function, controls how the inventory menu works
    private void ItemMenu()
    {
        menu.SetActive(false);
        itemMenu.SetActive(true);
    }

    // open the save menu
    private void SaveMenu()
    {
        menu.SetActive(false);
        saveMenu.SetActive(true);

        SelectOption(0.6f, saveOptions);
        TextColorMenu(saveOptions);

        if (Input.GetButtonDown("A_Button"))
        {
            if (buttonSelect == 0)
            {
                PlaySound(sounds, Sounds.SAVE);
                saveOptions[0].SetActive(false);
                saveOptions[1].SetActive(false);

                saveText.text = "SAVING GAME...";
                saveSystem.Save();
                saveText.text = "GAME SAVED!";
            }
            else
            {
                PlaySound(sounds, Sounds.BACK);
                saveMenu.SetActive(false);
                menu.SetActive(true);
                state = Menus.GENERAL;
            }
        }        
    }

    // loads the game file
    private void LoadGame()
    {
        Debug.Log("Loading game...");
        saveSystem.Load();
        saveMenu.SetActive(false);
    }

    // checks to see if the player pressed any buttons
    public void CheckInput()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
    }

    // quick shortcut script to play specific sound
    private void PlaySound(AudioClip[] clips, Sounds sound)
    {
        audioSource.clip = clips[(int)sound];
        audioSource.Play();
    }
}
