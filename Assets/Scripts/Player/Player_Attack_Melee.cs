using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack_Melee : MonoBehaviour
{
    //public variables

    //private variables
    private Animator animator;
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
        animator = GetComponentInChildren<Animator>();


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
        //get players coordinates
        float x = transform.position.x;
        float y = transform.position.y;
        
        //set animation and coordinate offset
        if(direction.up == true)
        {//Up

            y += attackRange;
            animator.SetTrigger("attack_up");

        }
        else if(direction.down == true)
        {//Down

            y -= attackRange;
            animator.SetTrigger("attack_down");

        }
        else if (direction.left == true)
        {//Left

            x -= attackRange;
            animator.SetTrigger("attack_left");

        }
        else if (direction.right == true)
        {//Right

            x += attackRange;
            animator.SetTrigger("attack_right");
        }

        //get all enemies in attack range
        Collider2D[] targets = Physics2D.OverlapBoxAll(new Vector2(x, y), new Vector2(attackRange, attackRange), 0);

        //loop through each enemy and damage
        foreach (Collider2D target in targets)
        {
            if (target.tag == "Mob")
            {//damage enemy

                target.GetComponent<Mob_Health>().Damage(attackDamage);
            }
            
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
