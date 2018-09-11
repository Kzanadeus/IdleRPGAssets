using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Message
{
    public string text;
    public Color color;
    public float duration;

    public Message(string text, Color color, float duration)
    {
        this.text = text;
        this.color = color;
        this.duration = duration;
    }
}

public class CharacterControllerScript : MonoBehaviour {

    GUIScript gui;
    ButtonsScript btn;

    public bool canMove = true;
    bool isCollided = false;

    float maxSpeed = 10f;
    float currentSpeed = 0.5f;
    float currentMovingSpeed = 0.1f;
    float currentDistance = 0f;

    float maxAttackSpeed = 0.1f;
    float currentAttackSpeed = 1f;

    float maxCriticalRate = 80;
    float currentCriticalRate = 1;
    float currentCriticalEffect = 1.1f;

    int monsterKillCount = 0;
    int bossKillCount = 0;
    int monsterToKillToGetAP = 10;
    int monsterLefToKillToGetAP = 10;

    int currentLevel = 1;
    int currentXP = 0;
    int nextLevelXP = 10;

    int currentAP = 0;
    int spentAP = 0;
    int APGainPerLevel = 1;

    int currentStrength = 10;
    int StrengthAPNeeded = 1;
    int currentAgility = 1;
    int AgilityAPNeeded = 1;
    int currentDexterity = 1;
    int DexterityAPNeeded = 1;
    int currentIntelligence = 1;
    int IntelligenceAPNeeded = 1;
    int currentVitality = 1;
    int VitalityAPNeeded = 1;
    int currentLuck = 1;
    int LuckAPNeeded = 1;

    int currentPhysicalDamages = 0;

    bool attributesChanged = true;

    List<Message> messages = new List<Message>();
    bool messagePoping = false;

    Animator anim;
    GameObject collider;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        gui = GameObject.Find("Main Camera").GetComponent<GUIScript>();
        btn = GameObject.Find("Main Camera").GetComponent<ButtonsScript>();

        currentPhysicalDamages = currentStrength;
        ChangeAP(5);
    }

    public void ChangeXP(int amount)
    {
        currentXP += amount;
        if(currentXP >= nextLevelXP)
        {
            currentXP -= nextLevelXP;
            currentLevel++;
            ChangeAP(APGainPerLevel);
            messages.Add(new Message("Level UP", Color.white, 1.5f));
            messages.Add(new Message("AP UP", Color.green, 1.5f));
            nextLevelXP = (int)Mathf.Round(nextLevelXP * 1.2f);
        }
    }

    public void CalculateNewDamages()
    {
        currentPhysicalDamages = currentStrength;
    }

    public void ChangeAP(int amount)
    {
        currentAP += amount;
        ManageAttributesButtons();
        gui.SetCharacterAPAmount(currentAP);
    }

    public void BuyStrengthWithAP()
    {
        if (currentAP >= StrengthAPNeeded)
        {
            currentStrength++;
            CalculateNewDamages();
            spentAP += StrengthAPNeeded;
            StrengthAPNeeded++;
            ChangeAP((StrengthAPNeeded - 1) * -1);
        }
    }

    public void BuyAgilityWithAP()
    {
        Debug.Log("BuyAgilityByAP");
        if (currentAP >= AgilityAPNeeded)
        {
            currentAgility++;
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += 0.01f;
            }
            spentAP += AgilityAPNeeded;
            AgilityAPNeeded++;
            ChangeAP((AgilityAPNeeded - 1) * -1);
        }
    }

    public void BuyDexterityWithAP()
    {
        if (currentAP >= DexterityAPNeeded)
        {
            currentDexterity++;
            spentAP += DexterityAPNeeded;
            currentAttackSpeed -= 0.001f;
            DexterityAPNeeded++;
            ChangeAP(DexterityAPNeeded * -1);
        }
    }

    public void BuyIntelligenceWithAP()
    {
        if (currentAP >= IntelligenceAPNeeded)
        {
            currentIntelligence++;
            spentAP += IntelligenceAPNeeded;
            IntelligenceAPNeeded++;
            ChangeAP((IntelligenceAPNeeded - 1) * -1);
        }
    }

    public void BuyVitalityWithAP()
    {
        if (currentAP >= VitalityAPNeeded)
        {
            currentVitality++;
            spentAP += VitalityAPNeeded;
            VitalityAPNeeded++;
            ChangeAP((VitalityAPNeeded - 1) * -1);
        }
    }

    public void BuyLuckhWithAP()
    {
        if (currentAP >= LuckAPNeeded)
        {
            currentLuck++;
            spentAP += LuckAPNeeded;
            currentCriticalRate += 0.1f;
            currentCriticalEffect += 0.01f;
            LuckAPNeeded++;
            ChangeAP((LuckAPNeeded - 1) * -1);
        }
    }

    public void ManageAttributesButtons()
    {
        btn.ActivateStrength(currentAP >= StrengthAPNeeded);
        btn.ActivateAgility(currentAP >= AgilityAPNeeded);
        btn.ActivateDexterity(currentAP >= DexterityAPNeeded);
        btn.ActivateIntelligence(currentAP >= IntelligenceAPNeeded);
        btn.ActivateVitality(currentAP >= VitalityAPNeeded);
        btn.ActivateLuck(currentAP >= LuckAPNeeded);
    }

    public void destroyedMonster(GameObject deadMonster)
    {
        ChangeXP(deadMonster.gameObject.GetComponent<SlimeControllerScript>().XPGain);
        monsterKillCount++;
        monsterLefToKillToGetAP -= 1;
        if(monsterLefToKillToGetAP == 0)
        {
            monsterLefToKillToGetAP = monsterToKillToGetAP;
            ChangeAP(1);
            messages.Add(new Message("AP UP", Color.green, 1.5f));
        }
        if (deadMonster == collider)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            isCollided = false;
            anim.SetBool("Collision", false);
            canMove = true;
            CancelInvoke();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            collider = collision.gameObject;
            isCollided = true;
            anim.SetBool("Collision", true);
            InvokeRepeating("LaunchAttack", currentAttackSpeed, currentAttackSpeed);
            Stop();
        }
    }

    void LaunchAttack()
    {
        int damages = currentPhysicalDamages;
        bool isCritical = false;
        int rnd = (int)Random.Range(1, 100);
        if(rnd < currentCriticalRate)
        {
            damages = (int)Mathf.Ceil((float)damages * currentCriticalEffect);
            isCritical = true;
        }
        collider.GetComponent<SlimeControllerScript>().TakeDamages(damages, isCritical);
    }

    void CreateText(Message message)
    {
        CombatTextManagerScript.Instance.CreateText(gameObject.transform.position, message.text, message.color, message.duration);
    }

    IEnumerator EndMessagePoping()
    {
        yield return new WaitForSeconds(((Message)messages[0]).duration);
        messagePoping = false;
        messages.RemoveAt(0);
    }

    void ManageMessages()
    {
        if (messagePoping)
        {
            return;
        }

        if(messages.Count > 0)
        {
            Message message = (Message)messages[0];
            CreateText(message);
            messagePoping = true;
            StartCoroutine(EndMessagePoping());
        }
    }

    void ManageCharacterData()
    {
        currentDistance = (Vector3.Distance(new Vector3(-10, 3, 0), transform.position));
        gui.SetCharacterVariables(currentXP, nextLevelXP, currentLevel, currentDistance);

        if (attributesChanged)
        {
            gui.SetCharacterAttributes(currentStrength, currentAgility, currentDexterity, currentIntelligence, currentVitality, currentLuck);
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        ManageMessages();
        ManageCharacterData();

        if (Input.GetKeyDown("space"))
        {
            canMove = !canMove;
            if (!canMove || isCollided)
            {
                Stop();
            }
        }
        if (!canMove)
        {
            return;
        }
        currentMovingSpeed = currentSpeed;
        //float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(currentMovingSpeed));

        GetComponent<Rigidbody2D>().velocity = new Vector2(currentMovingSpeed * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

        if(currentSpeed > maxSpeed)
        {
            currentSpeed = maxSpeed;
        }
	}

    void Stop()
    {
        canMove = false;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        anim.SetFloat("Speed", 0);
        currentMovingSpeed = 0;
    }
}
