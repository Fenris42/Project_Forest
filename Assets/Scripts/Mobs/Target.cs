using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //public varialbes

    //private variables
    private Direction targetTile;
    private GameObject mob;
    [SerializeField] private bool outOfBounds;

    public bool OutOfBounds()
    {
        return outOfBounds;
    }
    public void SetTargetsMob(GameObject ParentMob)
    {
        mob = ParentMob;
    }

    public void SetTargetTile(Direction targetTileDirection)
    {//set the current tile direction of the target
        targetTile = new Direction(targetTileDirection);
    }

    public Direction GetTargetTile()
    {//get the current tile direction of the target
        return targetTile;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {//target has gone outside the room
            outOfBounds = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {//target has reentered to the room
            outOfBounds = false;
        }
    }
}
