using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatTextManagerScript : MonoBehaviour {

    private static CombatTextManagerScript instance;

    public GameObject textPrefab;

    public RectTransform canvasTransform;

    public float speed;
    public Vector3 direction;

    public static CombatTextManagerScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<CombatTextManagerScript>();
            }

            return instance;
        }
    }

    public void CreateText(Vector3 position, string text, Color color, float fadeTime)
    {
        GameObject sct = (GameObject)Instantiate(textPrefab, position, Quaternion.identity);
        sct.transform.SetParent(canvasTransform);
        sct.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        sct.GetComponent<CombatTextScript>().Initialize(speed, direction, fadeTime);
        sct.GetComponent<Text>().text = text;
        sct.GetComponent<Text>().color = color;
    }
}
