using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIScript : MonoBehaviour
{
    public RectTransform StrengthValue;
    public RectTransform AgilityValue;
    public RectTransform DexterityValue;
    public RectTransform IntelligenceValue;
    public RectTransform VitalityValue;
    public RectTransform LuckValue;

    public int characterKillCount = 0;
    public float characterDistance = 0f;
    public int characterCurrentXP = 0;
    public int characterNextLevelXP = 0;
    public int characterCurrentLevel = 0;
    public int characterAPAmount = 0;
    public int characterStrength = 0;
    public int characterDexterity = 0;
    public int characterAgility = 0;
    public int characterIntelligence = 0;
    public int characterVitality = 0;
    public int characterLuck = 0;

    public void SetCharacterAPAmount(int APAmount)
    {
        characterAPAmount = APAmount;
    }

    public void SetCharacterAttributes(int currentStrength, int currentAgility, int currentDexterity, int currentIntelligence, int currentVitality, int currentLuck)
    {
        characterStrength = currentStrength;
        characterAgility = currentAgility;
        characterDexterity = currentDexterity;
        characterIntelligence = currentIntelligence;
        characterVitality = currentVitality;
        characterLuck = currentLuck;
    }

    public void SetCharacterVariables(int currentXP, int nextLevelXP, int currentLevel, float distance)
    {
        characterCurrentXP = currentXP;
        characterNextLevelXP = nextLevelXP;
        characterCurrentLevel = currentLevel;
        characterDistance = distance;
    }

    public void ChangeKillCount(int amount)
    {
        characterKillCount += amount;
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 30), "Level : " + characterCurrentLevel);
        GUI.Label(new Rect(130, 10, 100, 30), "XP : " + characterCurrentXP);
        GUI.Label(new Rect(230, 10, 100, 30), "TNL : " + characterNextLevelXP);
        GUI.Label(new Rect(330, 10, 100, 30), "Kill : " + characterKillCount);
        GUI.Label(new Rect(430, 10, 100, 30), "Distance : " + (int)characterDistance);
        GUI.Label(new Rect(10, 160, 100, 30), "AP : " + characterAPAmount);
        GUI.Label(new Rect(10, 200, 100, 30), "Attributes");
        StrengthValue.GetComponent<Text>().text = characterStrength.ToString();
        AgilityValue.GetComponent<Text>().text = characterAgility.ToString();
        DexterityValue.GetComponent<Text>().text = characterDexterity.ToString();
        IntelligenceValue.GetComponent<Text>().text = characterIntelligence.ToString();
        VitalityValue.GetComponent<Text>().text = characterVitality.ToString();
        LuckValue.GetComponent<Text>().text = characterLuck.ToString();
    }
}
