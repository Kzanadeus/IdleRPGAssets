using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMoverController : MonoBehaviour {

    public string SearchedTag;
    public int moveDistance;
    public bool isRand;
    public int randMin;
    public int randMax;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != SearchedTag)
        {
            return;
        }
        else
        {
            if (isRand)
            {
                collision.transform.position = new Vector3(collision.transform.position.x + (int)Random.Range(randMin, randMax), collision.transform.position.y, collision.transform.position.z);
            }
            else
            {
                collision.transform.position = new Vector3(collision.transform.position.x + moveDistance, collision.transform.position.y, collision.transform.position.z);

            }
        }
    }
}
