using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Attack : MonoBehaviour
{
    //public variables

    //private variables
    private GameObject player;

    //stats
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;


    
    void Start()
    {// Start is called before the first frame update

        //get components
        player = GameObject.Find("Player");
    }

    
    void Update()
    {// Update is called once per frame

    }

    public float GetAttackRange()
    {
        return attackRange;
    }

    public bool InAttackRange()
    {//lets other scripts know if player in attack range

        //mobs coords
        float mx = transform.position.x;
        float my = transform.position.y;

        //player coords
        float px = player.transform.position.x;
        float py = player.transform.position.y;

        //distances between objects
        float distx = Mathf.Abs(mx - px);
        float disty = Mathf.Abs(my - py);

        if (distx < attackRange && disty < attackRange)
        {
            return true;
        }
        else
        {
            return false;
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
