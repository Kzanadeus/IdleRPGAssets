using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsScript : MonoBehaviour {

    CharacterControllerScript ccs;

    public GameObject StrengthButton;
    public GameObject AgilityButton;
    public GameObject DexterityButton;
    public GameObject IntelligenceButton;
    public GameObject VitalityButton;
    public GameObject LuckButton;
    public GameObject SPButton;
    public GameObject MPButton;

    // Use this for initialization
    void Start () {
        StrengthButton.SetActive(false);
        StrengthButton.GetComponent<Button>().onClick.AddListener(AddStrength);
        AgilityButton.SetActive(false);
        AgilityButton.GetComponent<Button>().onClick.AddListener(AddAgility);
        DexterityButton.SetActive(false);
        DexterityButton.GetComponent<Button>().onClick.AddListener(AddDexterity);
        IntelligenceButton.SetActive(false);
        IntelligenceButton.GetComponent<Button>().onClick.AddListener(AddIntelligence);
        VitalityButton.SetActive(false);
        VitalityButton.GetComponent<Button>().onClick.AddListener(AddVitality);
        LuckButton.SetActive(false);
        LuckButton.GetComponent<Button>().onClick.AddListener(AddLuck);

        ccs = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterControllerScript>();

    }

    public void ActivateStrength(bool activate)
    {
        StrengthButton.SetActive(activate);
    }

    public void AddStrength()
    {
        ccs.BuyStrengthWithAP();
    }

    public void ActivateAgility(bool activate)
    {
        AgilityButton.SetActive(activate);
    }

    public void AddAgility()
    {
        Debug.Log("AddAgility");
        ccs.BuyAgilityWithAP();
    }

    public void ActivateDexterity(bool activate)
    {
        DexterityButton.SetActive(activate);
    }

    public void AddDexterity()
    {
        ccs.BuyDexterityWithAP();
    }

    public void ActivateIntelligence(bool activate)
    {
        IntelligenceButton.SetActive(activate);
    }

    public void AddIntelligence()
    {
        ccs.BuyIntelligenceWithAP();
    }

    public void ActivateVitality(bool activate)
    {
        VitalityButton.SetActive(activate);
    }

    public void AddVitality()
    {
        ccs.BuyVitalityWithAP();
    }

    public void ActivateLuck(bool activate)
    {
        LuckButton.SetActive(activate);
    }

    public void AddLuck()
    {
        ccs.BuyLuckhWithAP();
    }
}
