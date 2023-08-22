using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tunnel: MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer topDoorSprite;
    [SerializeField] private SpriteRenderer bottomDoorSprite;
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
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        bottomDoorSprite.enabled = false;
        topDoorSprite.enabled = false;
        doorHitbox.enabled = false;

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
