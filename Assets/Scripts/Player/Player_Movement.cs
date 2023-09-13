using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //public variables
    

    //private variables
    private Animator animator;
    private Direction direction = new Direction();
    private bool hold;
    private bool stunned;

    //stats
    [SerializeField] private int moveSpeed;



    
    void Start()
    {// Start is called before the first frame update

        //initialize components/variables
        animator = GetComponentInChildren<Animator>();
        direction.Down();

    }

    
    void Update()
    {// Update is called once per frame

        if (GetHolds() == false)
        {
            Movement();
        }
        else
        {
            ResetAnimator();
        }
        
    }

    private bool GetHolds()
    {
        if (stunned == true)
        {
            hold = true;
        }
        else
        {
            hold = false;
        }

        return hold;
    }
    private void Movement()
    {//move character per players input

        //separated to 2 if statements to prevent diagonal movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        { 
            if (Input.GetKey(KeyCode.W))
            {//move up
                MoveUp();
            }
            if (Input.GetKey(KeyCode.S))
            {//move down
                MoveDown();
            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {//move left
                MoveLeft();
            }
            if (Input.GetKey(KeyCode.D))
            {//move right
                MoveRight();
            }
        }
        else
        {//reset animators
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

        //set direction facing
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

        //set direction facing
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

        //set direction facing
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

        //set direction facing
        direction.Right();

        //move
        transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;

    }

    private void ResetAnimator()
    {//reset animation to idle

        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);
    }

    public void Knockback(Direction knockbackDirection)
    {//push the player in a direction

        //modifier for knockback amount
        float knockback = 1.5f;

        if (knockbackDirection.up == true)
        {//up
            transform.position += Vector3.up * knockback;
        }
        else if (knockbackDirection.down == true)
        {//down
            transform.position += Vector3.down * knockback;
        }
        else if (knockbackDirection.left == true)
        {//left
            transform.position += Vector3.left * knockback;
        }
        else if (knockbackDirection.right == true)
        {//right
            transform.position += Vector3.right * knockback;
        }

        //stun player for 1sec
        Stun();
    }

    private void Stun()
    {//stun player for x duration of time

        stunned = true;
        Invoke("ResetStun", 1f);
    }

    private void ResetStun()
    {
        stunned = false;
    }

    public Direction GetDirection()
    {//return object for which direction player is facing

        return direction;
    }
}
