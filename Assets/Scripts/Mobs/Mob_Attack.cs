using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Attack : MonoBehaviour
{
    //public variables

    //private variables
    private GameObject player;
    private Animator animator;
    private float timer;
    private Mob_Movement mob_movement;
    private float attackRange;

    //stats
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCoolDown;


    
    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        mob_movement = GetComponent<Mob_Movement>();
        attackRange = mob_movement.GetAttackRange();
    }

    
    void Update()
    {// Update is called once per frame

        timer += Time.deltaTime;

        if (mob_movement.InAttackRange() == true && timer >= attackCoolDown)
        {
            SwordAttack();
            timer = 0;
        }
    }

    private void SwordAttack()
    {
        //get mobs direction
        Direction direction = GetComponent<Mob_Movement>().GetDirection();

        //get mobs coordinates
        float x = transform.position.x;
        float y = transform.position.y;

        //set animation and coordinate offset
        if (direction.up == true)
        {//Up

            y += attackRange;
            animator.SetTrigger("attack_up");

        }
        else if (direction.down == true)
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
            if (target.tag == "Player")
            {//damage player

                target.GetComponent<Player_Health>().Damage(attackDamage);
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
