using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home_Door : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer doorSprite;
    [SerializeField] private BoxCollider2D doorHitbox;
    [SerializeField] private GameObject openTrigger;
    [SerializeField] private GameObject enterTrigger;
    private bool playerInOpenRange = false;
    private bool playerInEnterRange = false;


    
    void Start()
    {// Start is called before the first frame update

    }

    
    void Update()
    {// Update is called once per frame

        CheckIfPlayerInRange();
        PlayerInput();
        
        if (playerInEnterRange == true)
        {
            EnterDungeon();
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInOpenRange == true)
        {
            OpenDoor();
        }
    }

    private void CheckIfPlayerInRange()
    {
        playerInOpenRange = openTrigger.GetComponent<Range_Check>().IsPlayerInRange();
        playerInEnterRange = enterTrigger.GetComponent<Range_Check>().IsPlayerInRange();
    }

    private void OpenDoor()
    {
        doorSprite.enabled = false;
        doorHitbox.enabled = false;

    }

    private void EnterDungeon()
    {
        SceneManager.LoadScene("Debug_Dungeon");
    }

    
}
