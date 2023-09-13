using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Mob_Attack : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] GameObject projectilePrefab;

    private GameObject player;
    private Animator animator;
    private float timer;
    private Mob_Movement mob_movement;
    private Mob_Health mob_health;
    private Direction direction = new Direction();
    private Projectile_Type projectile_type = new Projectile_Type();

    //stats
    private enum classTypes { Fighter, Archer, Wizard };
    [SerializeField] classTypes classType;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float attackRange;
    [SerializeField] private float projectileSpeed;
    



    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        mob_movement = GetComponent<Mob_Movement>();
        mob_health = GetComponent<Mob_Health>();
       
    }

    
    void Update()
    {// Update is called once per frame

        timer += Time.deltaTime;

        if (mob_movement.InAttackRange() == true && mob_health.Alive() == true && timer >= attackCoolDown)
        {
            Attack();
            timer = 0;
        }
    }

    private void Attack()
    {//attack based on which attack type is selected

        //get which direction to attack in
        AttackDirection();

        //random number for weighted attacks
        //(because unity reasons, lower range is inclusive but max range is exclusive. max range needs to be 1 higher than it should)
        int x = Random.Range(1, 101);

        if (classType == classTypes.Fighter)
        {//Fighter profile

            if (x >= 1 && x <= 100)
            {//100%
                Sword_Basic();
            }
        }
        else if (classType == classTypes.Archer)
        {//Archer profile

            if (x >= 1 && x <= 100)
            {//100%
                Arrow_Basic();
            }  
        }
        else if (classType == classTypes.Wizard)
        {//Wizard profile

            if (x >= 1 && x < 45)
            {//45%
                Spell_Fireball();
            }
            else if (x >= 45 && x < 90)
            {//45%
                Spell_Iceball();
            }
            else if (x >= 90 && x <= 100)
            {//10%
                Spell_Heal();
            }
        }
    }

    



    // Fighter Attacks //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Sword_Basic()
    {
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
            targets = Physics2D.OverlapBoxAll(new Vector2(x, y + offset), new Vector2(1, attackRange), 0);

        }
        else if (direction.down == true)
        {//Down

            y -= attackRange;
            targets = Physics2D.OverlapBoxAll(new Vector2(x, y - offset), new Vector2(1, attackRange), 0);

        }
        else if (direction.left == true)
        {//Left

            x -= attackRange;
            targets = Physics2D.OverlapBoxAll(new Vector2(x - offset, y), new Vector2(attackRange, 1), 0);

        }
        else if (direction.right == true)
        {//Right

            x += attackRange;
            targets = Physics2D.OverlapBoxAll(new Vector2(x + offset, y), new Vector2(attackRange, 1), 0);
        }
        //loop through each enemy and damage
        foreach (Collider2D target in targets)
        {
            if (target.tag == "Player")
            {//damage player

                target.GetComponent<Player_Health>().Damage(attackDamage);
            }

        }
    }



    // Archer Attacks //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Arrow_Basic()
    {
        //set projectile type to arrow
        projectile_type.Arrow();
        
        //delay spawn to sync up with attack animation
        Invoke("SpawnProjectile", 0.5f);
    }




    // Spells //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Spell_Fireball()
    {
        //set projectile type to fireball
        projectile_type.Fireball();

        //delay spawn to sync up with attack animation
        Invoke("SpawnProjectile", 0.5f);
    }

    private void Spell_Iceball()
    {
        //set projectile type to fireball
        projectile_type.Iceball();

        //delay spawn to sync up with attack animation
        Invoke("SpawnProjectile", 0.5f);
    }

    private void Spell_Heal()
    {//heal for 1/4 health

        int heal = mob_health.GetMaxHealth();
        heal = heal / 4;

        mob_health.Heal(heal);
    }



    //  Utility functions //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private Direction AttackDirection()
    {
        direction = GetComponent<Mob_Movement>().GetDirection();

        //set animation
        if (direction.up == true)
        {//Up
            animator.SetTrigger("attack_up");
        }
        else if (direction.down == true)
        {//Down
            animator.SetTrigger("attack_down");
        }
        else if (direction.left == true)
        {//Left
            animator.SetTrigger("attack_left");
        }
        else if (direction.right == true)
        {//Right
            animator.SetTrigger("attack_right");
        }

        return direction;
    }

    private void SpawnProjectile()
    {
        //get mobs coords
        float x = transform.position.x;
        float y = transform.position.y;

        //spawn clone game object
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(x, y), transform.rotation);

        //configure instantiated object
        projectile.GetComponent<Projectile>().Initialize(attackDamage, projectileSpeed, direction, projectile_type);

    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public int GetAttackDamage()
    {
        return attackDamage;
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
