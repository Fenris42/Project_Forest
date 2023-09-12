using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //public variables

    //private variables
    [SerializeField] private GameObject ArrowObject;
    [SerializeField] private GameObject FireballObject;
    [SerializeField] private GameObject IceballObject;

    //stats
    private int damage;
    private float speed;
    private Direction direction;
    private Projectile_Type type;


    void Start()
    {// Start is called before the first frame update

    }

    void Update()
    {// Update is called once per frame

        Move();
    }

    public void Initialize(int Damage, float Speed, Direction Direction, Projectile_Type Projectile_Type)
    {
        damage = Damage;
        speed = Speed;
        direction = Direction;
        type = Projectile_Type;

        //Set which sprite and hitbox to use
        SetType();

        //set sprites orientation
        SetDirection();
    }
    private void Move()
    {//arrows flight pattern
        
        if (direction.up == true) 
        {//up
            transform.position += (Vector3.up * speed) * Time.deltaTime;
        }
        else if (direction.down == true)
        {//down
            transform.position += (Vector3.down * speed) * Time.deltaTime;
        }
        else if (direction.left == true)
        {//left
            transform.position += (Vector3.left * speed) * Time.deltaTime;
        }
        else if (direction.right == true)
        {//right
            transform.position += (Vector3.right * speed) * Time.deltaTime;
        }
    }

    private void SetType()
    {//set active child sprite

        if (type.arrow == true)
        {
            ArrowObject.SetActive(true);
        }
        else if (type.fireball == true)
        {
            FireballObject.SetActive(true);
        }
        else if (type.iceball == true)
        {
            IceballObject.SetActive(true);
        }
    }

    private void SetDirection()
    {//set sprite direction of arrow
        
        //set arrow rotation coords
        float x = transform.rotation.x;
        float y = transform.rotation.y;
        float z = transform.rotation.z;

        if (direction.right == true)
        {//right (default)
            z = 0;
        }
        else if (direction.down == true)
        {//down
            z = -90;
        }
        else if (direction.left == true)
        {//left
            z = -180;
        }
        else if (direction.up == true)
        {//up
            z = -270;
        }

        //set  rotation
        transform.Rotate(x, y, z);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//Collision handling
        
        if (collision.tag == "Wall")
        {//projectile has gone out of bounds

            //delete projectile
            Destroy(gameObject);
        }

        //projectile hit player
        if (collision.tag == "Player")
        {
            Player_Health player_health = GameObject.Find("Player").GetComponent<Player_Health>();
            player_health.Damage(damage);

            //delete projectile
            Destroy(gameObject);
        }
    }
}
