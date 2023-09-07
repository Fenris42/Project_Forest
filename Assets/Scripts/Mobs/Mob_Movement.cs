using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mob_Movement : MonoBehaviour
{
    //public variables

    //private variables
    private Animator animator;
    private GameObject player;
    private Direction direction;
    private Mob_Health health;
    private Mob_Attack mob_attack;
    private bool inAttackRange;
    private float attackRange;
    private bool isAggro;

    //stats
    [SerializeField] private float moveSpeed;
    [SerializeField] private float aggroRange;



    void Start()
    {// Start is called before the first frame update

        //initialize components
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        direction = new Direction();
        health = GetComponent<Mob_Health>();
        mob_attack = GetComponent<Mob_Attack>();
        attackRange = mob_attack.GetAttackRange();
    }

    
    void Update()
    {// Update is called once per frame

        IsAggro();

        if (isAggro == true && health.Alive() == true)
        {
            Movement();
        }
    }

    public Direction GetDirection()
    {//return object for which direction player is facing

        return direction;
    }

    public bool InAttackRange()
    {
        return inAttackRange;
    }

    

    private void IsAggro()
    {//check if player has entered aggro range

        //get difference in coordinates between mob and player (line and diagonal)
        float dist = Vector2.Distance(transform.position, player.transform.position);
        
        if (dist < aggroRange)
        {//player has entered aggro range

            isAggro = true;
        }
    }

    private void Movement()
    {//move to attack range of player

        //mobs coords
        float mx = transform.position.x;
        float my = transform.position.y;

        //player coords
        float px = player.transform.position.x;
        float py = player.transform.position.y;
        
        //tile coords up/down/left/right of player
        Vector2 upCoord = new Vector2(px, py + attackRange);
        Vector2 downCoord = new Vector2(px, py - attackRange);
        Vector2 leftCoord = new Vector2(px - attackRange, py);
        Vector2 rightCoord = new Vector2(px + attackRange, py);

        //distances between mob and the players up/down/left/right tile
        float distUp = Vector2.Distance(transform.position, upCoord);
        float distDown = Vector2.Distance(transform.position, downCoord);
        float distLeft = Vector2.Distance(transform.position, leftCoord);
        float distRight = Vector2.Distance(transform.position, rightCoord);

        //determine what tile up/down/left/right of player is closest
        if (distUp < distDown && distUp < distLeft && distUp < distRight)
        {//top tile
            px = px;
            py = py + attackRange;
        }
        else if (distDown < distUp && distDown < distLeft && distDown < distRight)
        {//down tile
            px = px;
            py = py - attackRange;
        }
        else if (distLeft < distUp && distLeft < distDown && distLeft < distRight)
        {//left tile
            px = px - attackRange;
            py = py;
        }
        else if (distRight < distUp && distRight < distDown && distRight < distLeft)
        {//right tile
            px = px + attackRange;
            py = py;
        }

        //distances between objects
        float distx = Mathf.Abs(mx - px);
        float disty = Mathf.Abs(my - py);

        if (distx > 0.1)
        {//move left/right to match player

            inAttackRange = false;

            if (mx > px)
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

            inAttackRange = false;

            if (my < py)
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
            ResetAnimator();
            FacePlayer();
            inAttackRange = true;
        }
    }

    private void FacePlayer()
    {// turn to face player

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

    private void ResetAnimator()
    {
        //return to idle animation
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);
    }

    private void OnDrawGizmosSelected()
    {//draw aggro range in editor

        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
