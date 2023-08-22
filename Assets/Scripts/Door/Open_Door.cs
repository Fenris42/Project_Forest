using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open_Door : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer doorSprite;
    [SerializeField] private BoxCollider2D doorHitbox;
    private bool playerInRange = false;
    

    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange == true)
        {
            ToggleDoor();
        }
    }

    private void ToggleDoor()
    {
        if (doorSprite.enabled == true)
        {
            doorSprite.enabled = false;
            doorHitbox.enabled = false;
        }
        else
        {
            doorSprite.enabled = true;
            doorHitbox.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerInRange = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerInRange = false;

        }
    }
}
