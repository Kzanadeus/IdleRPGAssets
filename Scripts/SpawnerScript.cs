using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

    CharacterControllerScript ccs;
    bool isMonster = false;

    public GameObject[] objs;
    public float spawnMin = 1f;
    public float spawnMax = 2f;

	// Use this for initialization
	void Start ()
    {
        if(this.gameObject.tag == "MonsterSpawner")
        {
            isMonster = true;
        }
        ccs = GameObject.Find("Character").GetComponent<CharacterControllerScript>();
        Spawn();
	}

    void Spawn()
    {
        if(objs.Length == 0)
        {
            return;
        }
        bool instantiate = false;
        if (ccs.canMove)
        {
            if (!isMonster)
            {
                instantiate = true;
            }
            else
            {
                GameObject[] objs = GameObject.FindGameObjectsWithTag("Monster");
                if (objs.GetLength(0) > 0)
                {
                    float currentPosX = transform.position.x;
                    float closestMonsterPosX = objs[objs.GetLength(0) - 1].transform.position.x;
                    if (currentPosX - closestMonsterPosX >= 1)
                    {
                        instantiate = true;
                    }
                }
                else {
                    instantiate = true;
                }
            }
        }
        if (instantiate)
        {
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, -1f);
            Instantiate(objs[Random.Range(0, objs.GetLength(0))], newPosition, Quaternion.identity);
        }
        Invoke("Spawn", Random.Range(spawnMin, spawnMax));
    }
}
