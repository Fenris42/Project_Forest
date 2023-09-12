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

    private void Sanitize()
    {//keep values in range

        if (health < 0)
        {
            health = 0;
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void Damage(int damage)
    {//apply damage to player

        health -= damage;
        Sanitize();

        //update health bar
        healthBar.Remove(damage);

        //player has died
        if (health <= 0)
        {
            Die();
        }
    }

    public void Heal(int heal)
    {//apply healing to player

        health += heal;
        Sanitize();

        //update health bar
        healthBar.Add(health);
    }

    private void Die()
    {

    }
}
