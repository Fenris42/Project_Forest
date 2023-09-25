using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer spriteOpenTop;
    [SerializeField] private SpriteRenderer spriteOpenBottom;
    [SerializeField] private SpriteRenderer spriteClosed;
    [SerializeField] private Range_Check trigger;

    //stats



    void Start()
    {// Start is called before the first frame update
        
    }

    void Update()
    {// Update is called once per frame

        if (Input.GetKeyDown(KeyCode.E) && trigger.IsPlayerInRange() == true)
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        //toggle sprites
        spriteClosed.enabled = false;
        spriteOpenBottom.enabled = true;
        spriteOpenTop.enabled = true;

        //loot

    }
}
