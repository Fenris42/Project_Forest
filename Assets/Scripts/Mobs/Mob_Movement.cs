using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mob_Movement : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private GameObject target;
    private Animator animator;
    private GameObject player;
    private Direction direction;
    private Mob_Health health;
    private Mob_Attack mob_attack;
    private float attackRange;
    private bool isAggro;
    private bool hold;
    private bool stunned;
    private GameObject upTarget;
    private GameObject downTarget;
    private GameObject leftTarget;
    private GameObject rightTarget;

    //stats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float aggroRange;



    void Start()
    {// Start is called before the first frame update

        //initialize components/variables
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        direction = new Direction();
        health = GetComponent<Mob_Health>();
        mob_attack = GetComponent<Mob_Attack>();
        attackRange = mob_attack.GetAttackRange();
        

        //initialize targets around player
        SetTargets();
    }


    void Update()
    {// Update is called once per frame

        if (IsAggro() == false)
        {//Do nothing

        }
        if (IsAggro() == true && GetHolds() == false)
        {//move towards player
            Movement();
        }
        else if (IsAggro() == true && GetHolds() == true)
        {//stop moving and face player
            FacePlayer();
        }
    }

    // Movement ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void Movement()
    {//move to attack range of player

        GameObject target = SelectTarget();

        //mobs coords
        float mx = transform.position.x;
        float my = transform.position.y;

        //target coords
        float tx = target.transform.position.x;
        float ty = target.transform.position.y;

        //distances between objects
        float distx = Mathf.Abs(mx - tx);
        float disty = Mathf.Abs(my - ty);

        if (distx > 0.1)
        {//move left/right to match player

            if (mx > tx)
            {//move left
                MoveLeft();
            }
            else
            {//move right
                MoveRight();
            }
        }
        else if (disty > 0.1)
        {// move up/down to match player

            if (my < ty)
            {//move up
                MoveUp();
            }
            else
            {//move down
                MoveDown();
            }
        }
        else
        {
            FacePlayer();
        }
    }

    private void FacePlayer()
    {// turn to face player

        ResetAnimator();

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
            {//left of player
                direction.Right();
                animator.SetTrigger("idle_right");
            }
            else if (mx > px)
            {//right of player
                direction.Left();
                animator.SetTrigger("idle_left");
            }
        }
        else if (disty > distx)
        {//y axis
            if (my > py)
            {//up of player
                direction.Down();
                animator.SetTrigger("idle_down");
            }
            else if (my < py)
            {//down of player
                direction.Up();
                animator.SetTrigger("idle_up");
            }
        }
    }

    private void MoveUp()
    {
        //animators
        animator.SetBool("walk_up", true);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);

        //set direction
        direction.Up();

        //move
        transform.position += (Vector3.up * moveSpeed) * Time.deltaTime;
    }

    private void MoveDown()
    {
        //animators
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", true);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);

        //set direction
        direction.Down();

        //move
        transform.position += (Vector3.down * moveSpeed) * Time.deltaTime;
    }

    private void MoveLeft()
    {
        //animators
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", true);
        animator.SetBool("walk_right", false);

        //set direction
        direction.Left();

        //move
        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        //animators
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", true);

        //set direction
        direction.Right();

        //move
        transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;
    }



    // Targeting ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void SetTargets()
    {
        //target direction
        Direction targetDirection = new Direction();

        //player coords
        float px = player.transform.position.x;
        float py = player.transform.position.y;

        //tile coords up/down/left/right of player
        Vector3 upCoord = new Vector3(px, py + attackRange, 0);
        Vector3 downCoord = new Vector3(px, py - attackRange, 0);
        Vector3 leftCoord = new Vector3(px - attackRange, py, 0);
        Vector3 rightCoord = new Vector3(px + attackRange, py, 0);

        //create target objects on player
        //up
        upTarget = Instantiate(target, upCoord, transform.rotation, player.transform);
        upTarget.GetComponent<Target>().SetTargetsMob(gameObject);
        targetDirection.Up();
        upTarget.GetComponent<Target>().SetTargetTile(targetDirection);

        //down
        downTarget = Instantiate(target, downCoord, transform.rotation, player.transform);
        downTarget.GetComponent<Target>().SetTargetsMob(gameObject);
        targetDirection.Down();
        downTarget.GetComponent<Target>().SetTargetTile(targetDirection);

        //left
        leftTarget = Instantiate(target, leftCoord, transform.rotation, player.transform);
        leftTarget.GetComponent<Target>().SetTargetsMob(gameObject);
        targetDirection.Left();
        leftTarget.GetComponent<Target>().SetTargetTile(targetDirection);

        //right
        rightTarget = Instantiate(target, rightCoord, transform.rotation, player.transform);
        rightTarget.GetComponent<Target>().SetTargetsMob(gameObject);
        targetDirection.Right();
        rightTarget.GetComponent<Target>().SetTargetTile(targetDirection);
    }

    public void RemoveTargets()
    {//remove all targets from player
        Destroy(upTarget);
        Destroy(downTarget);
        Destroy(leftTarget);
        Destroy(rightTarget);
    }

    private GameObject SelectTarget()
    {//select mobs ideal attack position

        //TO DO
        //check all targets.outofbounds() for all false values and then select closets ACTIVE target

        //distances between mob and the players up/down/left/right tile
        float distUp = Vector2.Distance(transform.position, upTarget.transform.position);
        float distDown = Vector2.Distance(transform.position, downTarget.transform.position);
        float distLeft = Vector2.Distance(transform.position, leftTarget.transform.position);
        float distRight = Vector2.Distance(transform.position, rightTarget.transform.position);
        
        //determine what tile up/down/left/right of player is closest
        if (distUp < distDown && distUp < distLeft && distUp < distRight)
        {//top tile
            return upTarget;
        }
        else if (distDown < distUp && distDown < distLeft && distDown < distRight)
        {//down tile
            return downTarget;
        }
        else if (distLeft < distUp && distLeft < distDown && distLeft < distRight)
        {//left tile
            return leftTarget;
        }
        else if (distRight < distUp && distRight < distDown && distRight < distLeft)
        {//right tile
            return rightTarget;
        }
        else
        {
            return null;
        }
    }



    // Utility ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private bool GetHolds()
    {//get movement holds

        if (IsAggro() == false)
        {//is mob aggroed
            hold = true;
        }
        else if (health.Alive() == false)
        {//is mob alive
            hold = true;
        }
        else if (stunned == true)
        {
            hold = true;
        }
        else if (mob_attack.InAttackRange() == true)
        {
            hold = true;
        }
        else
        {//no holds, mob free to move
            hold = false;
        }

        return hold;
    }

    private bool IsAggro()
    {//check if player has entered aggro range

        //get difference in coordinates between mob and player (line and diagonal)
        float dist = Vector2.Distance(transform.position, player.transform.position);

        if (dist < aggroRange)
        {//player has entered aggro range
            isAggro = true;
        }

        return isAggro;
    }

    private void ResetAnimator()
    {//return to idle animation

        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);
    }

    public Direction GetDirection()
    {//return object for which direction mob is facing
        return direction;
    }

    public void Stun(float time)
    {//stun player for x duration of time

        stunned = true;
        Invoke("ResetStun", time);
    }

    private void ResetStun()
    {
        stunned = false;
    }

    private void OnDrawGizmosSelected()
    {//draw aggro range in editor

        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
