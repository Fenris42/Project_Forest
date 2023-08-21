using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_Follow_Player : MonoBehaviour
{
    //public variables

    //private variables
    private GameObject player;
    private Camera camera;


    
    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        player = GameObject.Find("Player");
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    private void FollowPlayer()
    {
        //get coordinates
        float x = player.transform.position.x;
        float y = player.transform.position.y;
        float z = camera.transform.position.z;

        //set camera position
        camera.transform.position = new Vector3(x, y, z);
    }
}
