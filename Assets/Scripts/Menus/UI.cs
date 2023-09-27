using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private SpriteRenderer closedChestSprite;
    [SerializeField] private SpriteRenderer openChestSprite;
    [SerializeField] private Image pauseIcon;
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private GameObject menuMain;
    [SerializeField] private GameObject menuInventory;
    [SerializeField] private TMP_Text header;

    private bool menuOpen;
    private enum menus { none, main, inventory };
    private menus menu;

    

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
            DisplayMainMenu();
        }
        else if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.Tab))
        {
            DisplayInventoryMenu();
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


    private void DisplayMainMenu()
    {
        if (menu != menus.main)
        {
            OpenMenu();
            menuMain.SetActive(true);
            header.text = "Main Menu";
            menu = menus.main;
        }
        else
        {
            CloseMenu();
        }

    }
    
    public void DisplayInventoryMenu()
    {
        if (menu != menus.inventory)
        {
            OpenMenu();
            menuInventory.SetActive(true);
            header.text = "Inventory";
            menu = menus.inventory;
        }
        else
        {
            CloseMenu();
        }

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
