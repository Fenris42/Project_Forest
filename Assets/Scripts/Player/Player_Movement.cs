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

        if (Input.GetKey(KeyCode.W))
        {//move up
            animator.SetTrigger("idle_back");
            transform.position += (Vector3.up * moveSpeed) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.A))
        {//move left
            animator.SetTrigger("idle_left");
            transform.position += (Vector3.left * moveSpeed) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {//move right
            animator.SetTrigger("idle_right");
            transform.position += (Vector3.right * moveSpeed) * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {//move down
            animator.SetTrigger("idle_front");
            transform.position += (Vector3.down * moveSpeed) * Time.deltaTime;

        }

    }
}
