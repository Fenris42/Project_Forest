using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer closedChest;
    [SerializeField] private SpriteRenderer openChest;
    private bool menuOpen;


    void Start()
    {// Start is called before the first frame update

    }
        
    void Update()
    {// Update is called once per frame

    }

    public void ToggleChest()
    {//toggle HUD chest icon open/closed

        if (menuOpen == false)
        {//open chest
            menuOpen = true;
            closedChest.enabled = false;
            openChest.enabled = true;
        }
        else if (menuOpen == true)
        {//close chest
            menuOpen = false;
            closedChest.enabled = true;
            openChest.enabled = false;
        }
    }
}
