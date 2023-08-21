using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //public variables

    //private variables
    private Animator animator;

    //stats
    [SerializeField] private int moveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        //import components
        animator = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {//move character per players input

        //separated to 2 if statements to prevent diagonal movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        { 
            if (Input.GetKey(KeyCode.W))
            {//move up
                animator.SetBool("walk_back", true);
                animator.SetBool("walk_front", false);
                animator.SetBool("walk_left", false);
                animator.SetBool("walk_right", false);
                transform.position += (Vector3.up * moveSpeed) * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.S))
            {//move down
                animator.SetBool("walk_front", true);
                animator.SetBool("walk_back", false);
                animator.SetBool("walk_left", false);
                animator.SetBool("walk_right", false);
                transform.position += (Vector3.down * moveSpeed) * Time.deltaTime;

            }
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {//move left
                animator.SetBool("walk_left", true);
                animator.SetBool("walk_back", false);
                animator.SetBool("walk_front", false);
                animator.SetBool("walk_right", false);
                transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

            }
            if (Input.GetKey(KeyCode.D))
            {//move right
                animator.SetBool("walk_right", true);
                animator.SetBool("walk_back", false);
                animator.SetBool("walk_front", false);
                animator.SetBool("walk_left", false);
                transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;

            }
        }
        else
        {//reset animators
            animator.SetBool("walk_back", false);
            animator.SetBool("walk_front", false);
            animator.SetBool("walk_left", false);
            animator.SetBool("walk_right", false);
        }
    }
}
