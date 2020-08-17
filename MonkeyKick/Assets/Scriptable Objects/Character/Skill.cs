using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : ScriptableObject, ISerializationCallbackReceiver
{
    ////////// SKILL CLASS //////////
    /// this scriptable object class is the basis for all battle skills in the game, problem with dat buddy?? :>>>

    ////////// DESCRIPTION //////////
    // describes the skill
    public string skillName = "New Skill";
    [TextArea(10, 15)]
    public string skillDescription = "This skill does something.";

    ////////// BUTTONS AND RANK TEXT //////////
    // the button presses that come up and the text of how good your button press was
    public Sprite[] buttonIcon;
    public string[] rankTextList = { "MISS...", "ALRIGHT!", "AWESOME!!", "AMAZING!!!" };

    ////////// ATTACK PRESENTATION //////////
    // the audio, the animation, which character is doing the move, etc
    public PlayerBattleScript player;
    public AudioSource audioSource;
    public AudioClip[] hitSound;

    // changes the icon and its size 
    public void TriggerIcon(GameObject user, Image image, int iconChoice, float currentSize, float limitSize, float divSize,
        float xOffset, float yOffset)
    {
        if (image.sprite != buttonIcon[iconChoice])
        {
            image.sprite = buttonIcon[iconChoice];
        }

        image.rectTransform.sizeDelta = new Vector2((image.sprite.rect.width * (currentSize / limitSize)) / divSize,
            (image.sprite.rect.height * (currentSize / limitSize)) / divSize);

        if (image != null)
        {
            image.transform.position = new Vector3(Camera.main.WorldToScreenPoint(user.transform.position).x + xOffset,
                Camera.main.WorldToScreenPoint(user.transform.position).y + yOffset,
                Camera.main.WorldToScreenPoint(user.transform.position).z);
        }
    }

    // shows the text on screen
    public void TriggerText(GameObject user, Text rankText, int textChoice, float xOffset, float yOffset)
    {
        if (rankText != null)
        {
            rankText.transform.position = new Vector3(Camera.main.WorldToScreenPoint(user.transform.position).x + xOffset,
                Camera.main.WorldToScreenPoint(user.transform.position).y + yOffset,
                Camera.main.WorldToScreenPoint(user.transform.position).z);
        }

        rankText.text = rankTextList[textChoice];
    }

    // do the ability
    public abstract void SkillAction(PlayerBattleScript user);

    // helps reset SO
    public abstract void OnBeforeSerialize();
    public abstract void OnAfterDeserialize();
}
