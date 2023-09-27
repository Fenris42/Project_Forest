using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    private Projectile_Type projectile_type = new Projectile_Type();

    //stats
    private enum classTypes { Fighter, Archer, Wizard };
    [SerializeField] private classTypes classType;
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

        if (InAttackRange() == true && mob_health.Alive() == true)
        {
            if (timer >= attackCoolDown)
            {
                Attack();
                timer = 0;
            }
        }
    }

    private void Attack()
    {//attack based on which attack type is selected

        //freeze mobs movement while attack in progress
        mob_movement.Stun(0.5f);

        //get which direction to attack in and play animation
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
        player.GetComponent<Player_Health>().Damage(attackDamage);

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



    // Knockback ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {//player has touched the mob
            KnockBack();
        }
    }

    private Direction GetKnockbackDirection()
    {
        Direction direction = new Direction();

        //mobs coordinates
        float mx = transform.position.x;
        float my = transform.position.y;

        //players coordinates
        float px = player.transform.position.x;
        float py = player.transform.position.y;

        //distances between objects
        float distx = Mathf.Abs(mx - px);
        float disty = Mathf.Abs(my - py);

        if (distx > disty)
        {//x axis
            if (mx < px)
            {//player on the right
                direction.Right();
            }
            else if (mx > px)
            {//player on the left
                direction.Left();
            }
        }
        else if (disty > distx)
        {//y axis
            if (my > py)
            {//player is below
                direction.Down();
            }
            else if (my < py)
            {//player is above
                direction.Up();
            }
        }

        return direction;
    }

    private void KnockBack()
    {//damage player and knockback
        player.GetComponent<Player_Health>().Damage(attackDamage / 2);
        player.GetComponent<Player_Movement>().Knockback(GetKnockbackDirection());

        //hold position briefly after knockback
        gameObject.GetComponent<Mob_Movement>().Stun(1f);
    }



    //  Utility functions //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool InAttackRange()
    {//check if player is somewhere inside mobs attack area

        bool inAttackRange = false;

        //get mobs coordinates
        float x = transform.position.x;
        float y = transform.position.y;

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

        //get all targets in attack range
        Collider2D[] targets = { };
        //up
        targets = targets.Union(Physics2D.OverlapBoxAll(new Vector2(x, y + offset), new Vector2(0.1f, attackRange), 0)).ToArray();
        //down
        targets = targets.Union(Physics2D.OverlapBoxAll(new Vector2(x, y - offset), new Vector2(0.1f, attackRange), 0)).ToArray();
        //left
        targets = targets.Union(Physics2D.OverlapBoxAll(new Vector2(x - offset, y), new Vector2(attackRange, 0.1f), 0)).ToArray();
        //right
        targets = targets.Union(Physics2D.OverlapBoxAll(new Vector2(x + offset, y), new Vector2(attackRange, 0.1f), 0)).ToArray();

        //loop through each enemy and damage
        foreach (Collider2D target in targets)
        {
            if (target.tag == "Player")
            {
                inAttackRange = true;
            }
        }

        return inAttackRange;
    }
    
    private Direction GetDirection()
    {
        Direction direction = GetComponent<Mob_Movement>().GetDirection();
        return direction;
    }

    private void AttackDirection()
    {
        //set animation
        if (GetDirection().up == true)
        {//Up
            animator.SetTrigger("attack_up");
        }
        else if (GetDirection().down == true)
        {//Down
            animator.SetTrigger("attack_down");
        }
        else if (GetDirection().left == true)
        {//Left
            animator.SetTrigger("attack_left");
        }
        else if (GetDirection().right == true)
        {//Right
            animator.SetTrigger("attack_right");
        }
    }

    private void SpawnProjectile()
    {
        //get mobs coords
        float x = transform.position.x;
        float y = transform.position.y;

        //spawn clone game object
        GameObject projectile = Instantiate(projectilePrefab, new Vector3(x, y), transform.rotation);

        //configure instantiated object
        projectile.GetComponent<Projectile>().Initialize(attackDamage, projectileSpeed, GetDirection(), projectile_type);

    }

    public float GetAttackRange()
    {
        return attackRange;
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

        //up
        Gizmos.DrawWireCube(new Vector2(x, y + offset), new Vector2(0.1f, attackRange));

        //down
        Gizmos.DrawWireCube(new Vector2(x, y - offset), new Vector2(0.1f, attackRange));

        //left
        Gizmos.DrawWireCube(new Vector2(x - offset, y), new Vector2(attackRange, 0.1f));

        //right
        Gizmos.DrawWireCube(new Vector2(x + offset, y), new Vector2(attackRange, 0.1f));

        

    }
}
