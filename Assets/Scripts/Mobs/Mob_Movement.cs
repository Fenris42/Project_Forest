using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mob_Movement : MonoBehaviour
{
    //public variables

    //private variables
    private Animator animator;
    private GameObject player;
    private Direction direction;
    private bool isAggro;
    private Mob_Health health;
    private bool AttackRange;

    //stats
    [SerializeField] private int moveSpeed;
    [SerializeField] private int aggroRange;


    // Start is called before the first frame update
    void Start()
    {
        //initialize components
        player = GameObject.Find("Player");
        animator = GetComponentInChildren<Animator>();
        direction = new Direction();
        health = GetComponent<Mob_Health>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Aggro();

        if (isAggro == true && health.Alive() == true)
        {
            Movement();
        }
        
    }

    private void Aggro()
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
        Vector2 upCoord = new Vector2(px, py + 1);
        Vector2 downCoord = new Vector2(px, py - 1);
        Vector2 leftCoord = new Vector2(px - 1, py);
        Vector2 rightCoord = new Vector2(px + 1, py);

        //distances between mob and the players up/down/left/right tile
        float distUp = Vector2.Distance(transform.position, upCoord);
        float distDown = Vector2.Distance(transform.position, downCoord);
        float distLeft = Vector2.Distance(transform.position, leftCoord);
        float distRight = Vector2.Distance(transform.position, rightCoord);

        //determine what tile up/down/left/right of player is closest
        if (distUp < distDown && distUp < distLeft && distUp < distRight)
        {//top tile
            px = px;
            py = py + 1;
        }
        else if (distDown < distUp && distDown < distLeft && distDown < distRight)
        {//down tile
            px = px;
            py = py - 1;
        }
        else if (distLeft < distUp && distLeft < distDown && distLeft < distRight)
        {//left tile
            px = px - 1;
            py = py;
        }
        else if (distRight < distUp && distRight < distDown && distRight < distLeft)
        {//down tile
            px = px + 1;
            py = py;
        }

        //distances between objects
        float distx = Mathf.Abs(mx - px);
        float disty = Mathf.Abs(my - py);

        if (distx > 0.1)
        {//move left/right to match player

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
        {// stop walking animation

            ResetAnimator();
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

    public Direction GetDirection()
    {//return object for which direction player is facing

        return direction;
    }

    public bool inAttackRange()
    {//lets other scripts know if player in attack range

        return AttackRange;
    }

    private void OnDrawGizmosSelected()
    {//draw aggro range in editor

        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
