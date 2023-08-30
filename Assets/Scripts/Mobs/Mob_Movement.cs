using System.Collections;
using System.Collections.Generic;
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

        //players coords
        float px = player.transform.position.x;
        float py = player.transform.position.y;

        //mobs coords
        float mx = transform.position.x;
        float my = transform.position.y;

        //distances between objects
        float distx = Mathf.Abs(mx - px);
        float disty = Mathf.Abs(my - py);

        if (distx > 1)
        {//move to players x coord 

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
        {// move to players y coord

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

    private void OnDrawGizmosSelected()
    {//draw aggro range in editor

        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }
}
