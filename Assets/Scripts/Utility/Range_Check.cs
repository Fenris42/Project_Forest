using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range_Check : MonoBehaviour
{
    //private variables
    private bool playerInRange = false;

    public bool IsPlayerInRange()
    {
        return playerInRange;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//player has entered the BoxCollider2D trigger
        if (collision.gameObject.name == "Player")
        {
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {//player has exitted the BoxCollider2D trigger
        if (collision.gameObject.name == "Player")
        {
            playerInRange = false;

        }
    }
}
