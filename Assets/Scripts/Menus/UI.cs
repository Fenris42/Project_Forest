using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer closedChest;
    [SerializeField] private SpriteRenderer openChest;
    [SerializeField] private Image pause;
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menu_main;
    [SerializeField] private GameObject menu_inventory;
    [SerializeField] private TMP_Text header;

    private bool menuOpen;
    private bool mainMenuOpen;
    private bool inventoryOpen;



    void Start()
    {// Start is called before the first frame update

    }
        
    void Update()
    {// Update is called once per frame
        PlayerInput();
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {//toggle main menu
            ToggleMenu();
            DisplayMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleMenu();
            DisplayInventory();
        }
    }

    public void ToggleMenu()
    {//toggle HUD chest icon open/closed

        if (menuOpen == false)
        {//open menu
            menuOpen = true;
            menu.SetActive(true);
        }
        else if (menuOpen == true)
        {//close menu
            menuOpen = false;
            menu.SetActive(false);
        }

        TogglePause();
        ToggleChest();
        ResetMenu();
    }

    private void ToggleChest()
    {
        if (menuOpen == true)
        {//toggle chest sprite
            closedChest.enabled = false;
            openChest.enabled = true;
        }
        else if (menuOpen == false)
        {//toggle chest sprite
            closedChest.enabled = true;
            openChest.enabled = false;
        }
    }

    private void TogglePause()
    {
        if (menuOpen == true)
        {//pause game
            pause.enabled = true;
            Time.timeScale = 0;

        }
        else if (menuOpen == false)
        {//resume game
            pause.enabled = false;
            Time.timeScale = 1;
        }
    }

    public void DisplayMainMenu()
    {
        ResetMenu();
        header.text = "Main Menu";
        menu_main.SetActive(true);

    }

    public void DisplayInventory()
    {
        ResetMenu();
        header.text = "Inventory";
        menu_inventory.SetActive(true);
    }

    private void ResetMenu()
    {//close all sub menus
        header.text = "";
        menu_main.SetActive(false);
        menu_inventory.SetActive(false);
    }
}
