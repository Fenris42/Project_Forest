using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //public variables

    //serialized variables
    [SerializeField] private SpriteRenderer closedChestSprite;
    [SerializeField] private SpriteRenderer openChestSprite;
    [SerializeField] private Image pauseIcon;
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private GameObject menuMain;
    [SerializeField] private GameObject menuInventory;
    [SerializeField] private TMP_Text header;

    //private variables
    private bool menuOpen;
    private enum menus { none, main, inventory };
    private menus menu;

    

    void Start()
    {// Start is called before the first frame update
        
    }
        
    void Update()
    {// Update is called once per frame
        PlayerKeyPress();
    }

    private void PlayerKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {//toggle main menu
            if (menu != menus.main)
            {
                DisplayMainMenu();
            }
            else
            {
                CloseMenu();
            }
        }
        else if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (menu != menus.inventory)
            {
                DisplayInventoryMenu();
            }
            else
            {
                CloseMenu();
            }
        }
    }

    public void ChestClick()
    {
        if (menuOpen == false)
        {
            DisplayInventoryMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    // Menus //////////////////////////////////////////////////////////////////////////////////////////////
    private void OpenMenu()
    {
        menuOpen = true;
        menuWindow.SetActive(true);
        ResetMenu();
        OpenChest();
        Pause();
    }

    public void CloseMenu()
    {
        menuOpen = false;
        menuWindow.SetActive(false);
        ResetMenu();
        CloseChest();
        UnPause();
    }

    // Main Menu ///////////////////////////////////////////////////////////////////////////////////////////
    private void DisplayMainMenu()
    {
        OpenMenu();
        menuMain.SetActive(true);
        header.text = "Main Menu";
        menu = menus.main;
    }

    public void Quit()
    {//close game
        Application.Quit(); 
    }

    // Inventory ///////////////////////////////////////////////////////////////////////////////////////////
    private void DisplayInventoryMenu()
    {
        OpenMenu();
        menuInventory.SetActive(true);
        header.text = "Inventory";
        menu = menus.inventory;
    }



    // Utility //////////////////////////////////////////////////////////////////////////////////////////////
    private void ResetMenu()
    {//close all sub menus
        header.text = "";
        menu = menus.none;

        menuMain.SetActive(false);
        menuInventory.SetActive(false);
    }

    private void OpenChest()
    {
        closedChestSprite.enabled = false;
        openChestSprite.enabled = true;
    }
    private void CloseChest()
    {
        closedChestSprite.enabled = true;
        openChestSprite.enabled = false;
    }

    private void Pause()
    {
        pauseIcon.enabled = true;
        Time.timeScale = 0;
    }

    private void UnPause()
    {
        pauseIcon.enabled = false;
        Time.timeScale = 1;
    }

}
