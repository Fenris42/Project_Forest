using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Attack_Melee : MonoBehaviour
{
    //public variables

    //private variables

    //stats
    [SerializeField] private float attackRange;
    [SerializeField] private int attackDamage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
