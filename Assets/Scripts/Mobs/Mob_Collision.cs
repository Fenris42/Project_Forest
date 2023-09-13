using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Collision : MonoBehaviour
{
    //public variables

    //private variables
    private GameObject player;
    private int attackDamage;
    private bool hold;

    
    void Start()
    {// Start is called before the first frame update

        //initialize components/variables
        player = GameObject.Find("Player");
        attackDamage = GetComponent<Mob_Attack>().GetAttackDamage();

    }
        
    void Update()
    {// Update is called once per frame

    }

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
        gameObject.GetComponent<Mob_Movement>().Stun();
    }

    private void ResetHold()
    {
        hold = false;
    }

    public bool OnHold()
    {
        return hold;
    }
}
