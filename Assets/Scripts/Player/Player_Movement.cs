using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //public variables
    

    //private variables
    private Animator animator;
    private Direction direction = new Direction();

    //stats
    [SerializeField] private int moveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        //import components
        animator = GetComponentInChildren<Animator>();

        //set default facing direction
        direction.Down();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
    }

    private void PlayerInput()
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
        animator.SetBool("walk_up", true);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);

        direction.Up();

        transform.position += (Vector3.up * moveSpeed) * Time.deltaTime;
    }

    private void MoveDown()
    {
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", true);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);

        direction.Down();

        transform.position += (Vector3.down * moveSpeed) * Time.deltaTime;
    }

    private void MoveLeft()
    {
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", true);
        animator.SetBool("walk_right", false);

        direction.Left();

        transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;
    }

    private void MoveRight()
    {
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", true);

        direction.Right();

        transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;

    }

    private void ResetAnimator()
    {
        animator.SetBool("walk_up", false);
        animator.SetBool("walk_down", false);
        animator.SetBool("walk_left", false);
        animator.SetBool("walk_right", false);
    }

    public Direction GetDirection()
    {//return object for which direction player is facing
        return direction;
    }
}
