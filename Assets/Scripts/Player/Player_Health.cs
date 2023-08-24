using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private GameObject HealthBarObject;
    private Stat_Bar healthBar;

    //stats
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables
        healthBar = HealthBarObject.GetComponent<Stat_Bar>();
        healthBar.SetCurrent(health);
        healthBar.SetMax(maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
