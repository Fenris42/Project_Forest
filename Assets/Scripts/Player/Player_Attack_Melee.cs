using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Melee : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask mobLayer;
    private Player_Movement player_movement;
    private Direction direction;

    //stats
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;



    void Start()
    {// Start is called before the first frame update

        //get components
        player_movement = GetComponent<Player_Movement>();

        
    }
    
    void Update()
    {// Update is called once per frame

        PlayerInput();
        GetDirection();
        
    }
    
    private void PlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {//Left mouse button clicked

            SwordAttack();
        }
    }
    private void GetDirection()
    {//get players currently facing direction from player movement script

        direction = player_movement.GetDirection();
    }
    
    private void SwordAttack()
    {
        /*
        //change animation to sword attack
        animator.SetTrigger("Sword_Attack");

        //detect enemies in sword range
        Collider2D[] enemies = Physics2D.OverlapCircleAll(swordAttackRange.position, swordRange, mobLayer);
        Collider2D[] enemies = Physics2D.OverlapBoxAll(transform.position, attackRange, mobLayer);

        //loop through each enemy in range
        foreach (Collider2D enemy in enemies)
        {
            //damage enemy
            //enemy.GetComponent<Mob_Slime>().TakeDamage(swordAttackPower);
        }
        */
        //get players coordinates
        float x = transform.position.x;
        float y = transform.position.y;

        //array to store all in range enemies
        

        if(direction.up == true)
        {//Up

            animator.SetTrigger("attack_up");

            //offset
            y += attackRange;

        }
        else if(direction.down == true)
        {//Down

            animator.SetTrigger("attack_down");
            //offset
            y -= attackRange;

        }
        else if (direction.left == true)
        {//Left

            animator.SetTrigger("attack_left");

            //offset
            x -= attackRange;

        }
        else if (direction.right == true)
        {//Right

            animator.SetTrigger("attack_right");

            //offset
            x += attackRange;
        }

        //get all enemies in attack range
        Collider2D[] enemies = Physics2D.OverlapBoxAll(new Vector2(x, y), new Vector2(attackRange, attackRange), mobLayer);

        //loop through each enemy and damage
        foreach (Collider2D enemy in enemies)
        {
            //damage enemy
            //enemy.GetComponent<Mob_Slime>().TakeDamage(swordAttackPower);
        }

    }


    private void OnDrawGizmosSelected()
    {//For Debug - Draw sword attack range in editor

        //get players coordinates
        float x = transform.position.x;
        float y = transform.position.y;

        //Note: including attack range as a coordinate offset to have all 4 boxes corners touching each other
        //left
        Gizmos.DrawWireCube(new Vector2(x - attackRange, y), new Vector2(attackRange, attackRange));

        //right
        Gizmos.DrawWireCube(new Vector2(x + attackRange, y), new Vector2(attackRange, attackRange));

        //up
        Gizmos.DrawWireCube(new Vector2(x, y + attackRange), new Vector2(attackRange, attackRange));

        //down
        Gizmos.DrawWireCube(new Vector2(x, y - attackRange), new Vector2(attackRange, attackRange));

    }
    
}
