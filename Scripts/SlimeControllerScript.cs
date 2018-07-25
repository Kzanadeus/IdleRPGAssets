using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeControllerScript : MonoBehaviour {

    GUIScript gui;

    public bool showPopup = false;

    public int level = 1;
    public float scale = 1;
    public int statHP = 2;
    public int statMinHP = 1;
    public int statMaxHP = 3;
    public int statDefense = 0;
    public int XPGain = 0;

    void Start()
    {
        gui = GameObject.Find("Main Camera").GetComponent<GUIScript>();

        level = (int)Mathf.Floor(gui.characterDistance / 100) + 1;
        statHP = Random.Range(statMinHP*level, statMaxHP*level);
        CalculateScale();
        XPGain = (int)Mathf.Round(statHP / 100);
        if(XPGain < 1)
        {
            XPGain = 1;
        }
        if (scale > 1)
        {
            transform.localScale = new Vector3(scale, scale, 0);
            transform.position = new Vector3(transform.position.x, transform.position.y + ((scale - 1f) * 0.1f), 0);
        }
    }

    void CalculateScale()
    {
        if(statHP >= 1000000)
        {
            scale = 5 + Mathf.Floor(statHP / 1000000f) / 10f;
        }
        else if(statHP >= 100000)
        {
            scale = 4 + Mathf.Floor(statHP / 100000f) / 10f;
        }
        else if(statHP >= 10000)
        {
            scale = 3 + Mathf.Floor(statHP / 10000f) / 10f;
        }
        else if(statHP >= 1000)
        {
            scale = 2 + Mathf.Floor(statHP / 1000f) / 10f;
        }
        else if(statHP >= 100)
        {
            scale = 1 + Mathf.Floor(statHP / 100f) / 10f;
        }
    }

    public void TakeDamages(int damages, bool isCritical)
    {
        int damageTaken = damages - statDefense;
        statHP -= damageTaken;

        if (isCritical)
        {
            CombatTextManagerScript.Instance.CreateText(gameObject.transform.position, damageTaken.ToString()+" !", Color.red, 1.5f);
        }
        else
        {
            CombatTextManagerScript.Instance.CreateText(gameObject.transform.position, damageTaken.ToString(), Color.gray, 1.5f);
        }
        if (statHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameObject.FindObjectOfType<CharacterControllerScript>().destroyedMonster(this.gameObject);
        showPopup = false;
        gui.ChangeKillCount(1);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
        return;
	}

    void Stop()
    {
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Monster")
        {
            Destroy(collision.gameObject);
        }
    }

    void OnMouseDown()
    {
        showPopup = !showPopup;
    }

    private void OnGUI()
    {
        if (showPopup)
        {
            Vector3 screenPos = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(new Vector3(transform.position.x - 0.5f, transform.position.y + 0.3f, transform.position.z));
            GUI.Label(new Rect(screenPos.x, Screen.height - screenPos.y - 10, 100, 100), "Level : " + level);
        }
    }
}
