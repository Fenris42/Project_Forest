using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Health : MonoBehaviour
{
    //public variables
    
    //private variables
    [SerializeField] private GameObject healthBarObject;
    private Animator animator;
    private Stat_Bar healthBar;
    private bool alive;
    private Drop_Loot loot;

    //stats
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    


        
    void Start()
    {// Start is called before the first frame update

        //import components
        healthBar = healthBarObject.GetComponent<Stat_Bar>();
        loot = new Drop_Loot();
        animator = GetComponentInChildren<Animator>();

        //Initialize variables
        healthBar.SetCurrent(health);
        healthBar.SetMax(maxHealth);

        alive = true;
    }

    
    void Update()
    {// Update is called once per frame


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
    {//apply damage to mob

        health -= damage;
        Sanitize();

        //update health bar
        healthBar.Remove(damage);

        //mob has died
        if (health <= 0)
        {
            Die();
        }
    }
    
    public void Heal(int heal)
    {//heal mob

        health += heal;
        Sanitize();

        //update health bar
        healthBar.Add(heal);
    }

    private void Die()
    {//kill mob

        alive = false;
        animator.SetTrigger("die");

        //disable components
        healthBarObject.SetActive(false);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<Mob_Movement>().RemoveTargets();

        //loot
        loot.DropLoot("trash");

        //delete mob after 1 sec
        Invoke("DeleteMob", 1);
    }

    private void DeleteMob()
    {
        Destroy(gameObject);
    }

    public bool Alive()
    {
        return alive;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}
