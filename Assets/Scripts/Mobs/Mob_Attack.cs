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
    private enum attackTypes { Melee, Archer };
    

    //stats
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float attackRange;
    [SerializeField] private float projectileSpeed;
    [SerializeField] attackTypes attackType;



    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        mob_movement = GetComponent<Mob_Movement>();
       
    }

    
    void Update()
    {// Update is called once per frame

        timer += Time.deltaTime;

        if (mob_movement.InAttackRange() == true && timer >= attackCoolDown)
        {
            if (attackType == attackTypes.Melee)
            {
                SwordAttack();
            }
            else if (attackType == attackTypes.Archer)
            {
                ArrowAttack();
            }

            timer = 0;

        }
    }
    public float GetAttackRange()
    {
        return attackRange;
    }

    private void AttackDirection()
    {

    }
    private void SwordAttack()
    {
        //get mobs direction
        Direction direction = GetComponent<Mob_Movement>().GetDirection();

        //get mobs coordinates
        float x = transform.position.x;
        float y = transform.position.y;

        //array to store all attackable objects in range
        Collider2D[] targets = { };

        //used to find cubes center point in box draw
        float offset;

        if (attackRange <= 1)
        {//prevents range from being pushed into the mob
            offset = 1;
        }
        else
        {//correctly offsets the center point of the cube based on attack range
            offset = (attackRange / 2) + 0.5f;
        }
        

        //set animation and coordinate offset
        if (direction.up == true)
        {//Up

            y += attackRange;
            animator.SetTrigger("attack_up");
            targets = Physics2D.OverlapBoxAll(new Vector2(x, y + offset), new Vector2(1, attackRange), 0);

        }
        else if (direction.down == true)
        {//Down

            y -= attackRange;
            animator.SetTrigger("attack_down");
            targets = Physics2D.OverlapBoxAll(new Vector2(x, y - offset), new Vector2(1, attackRange), 0);

        }
        else if (direction.left == true)
        {//Left

            x -= attackRange;
            animator.SetTrigger("attack_left");
            targets = Physics2D.OverlapBoxAll(new Vector2(x - offset, y), new Vector2(attackRange, 1), 0);

        }
        else if (direction.right == true)
        {//Right

            x += attackRange;
            animator.SetTrigger("attack_right");
            targets = Physics2D.OverlapBoxAll(new Vector2(x + offset, y), new Vector2(attackRange, 1), 0);
        }

        //get all enemies in attack range
        //Collider2D[] targets = Physics2D.OverlapBoxAll(new Vector2(x, y), new Vector2(attackRange, attackRange), 0);

        //loop through each enemy and damage
        foreach (Collider2D target in targets)
        {
            if (target.tag == "Player")
            {//damage player

                target.GetComponent<Player_Health>().Damage(attackDamage);
            }

        }
    }

    private void ArrowAttack()
    {

        //play animation
        animator.SetTrigger("Attack");

        //spawn arrow in sync with animation
        Invoke("SpawnArrow", 0.5f);
    }

    private void SpawnProjectile()
    {
        /*
        AttackDirection()

        float x = transform.position.x; 
        float y = transform.position.y; 

        var arrow = arrowPrefab.GetComponent<Arrow>();
        arrow.ArrowSpeed = arrowSpeed;
        arrow.ArrowDamage = arrowDamage * (drawStrength / 100); //reduce max damage based on % of charge bar filled
        arrow.ArrowDirection = 1;

        //spawn arrow
        Instantiate(arrowPrefab, new Vector3(xCoord, yCoord), transform.rotation);
        */
    }

    private void OnDrawGizmosSelected()
    {//For Debug - Draw sword attack range in editor

        //get players coordinates
        float x = transform.position.x;
        float y = transform.position.y;
        float offset;

        //used to find cubes center point in gizmo draw
        if (attackRange <= 1)
        {//prevents range from being pushed into the mob
            offset = 1;
        }
        else
        {//correctly offsets the center point of the cube based on attack range
            offset = (attackRange / 2) + 0.5f;
        }

        //left
        Gizmos.DrawWireCube(new Vector2(x - offset, y), new Vector2(attackRange, 1));

        //right
        Gizmos.DrawWireCube(new Vector2(x + offset, y), new Vector2(attackRange, 1));

        //up
        Gizmos.DrawWireCube(new Vector2(x, y + offset), new Vector2(1, attackRange));

        //down
        Gizmos.DrawWireCube(new Vector2(x, y - offset), new Vector2(1, attackRange));




    }
}
