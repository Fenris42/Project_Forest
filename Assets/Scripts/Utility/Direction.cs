using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    //public variables
    public bool up { get; set; }
    public bool down { get; set; }
    public bool left { get; set; }
    public bool right { get; set; }

    //Constructors
    public Direction()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }

    //Set direction methods
    public void Up()
    {
        up = true;
        down = false;
        left = false;
        right = false;
    }

    public void Down()
    {
        up = false;
        down = true;
        left = false;
        right = false;
    }

    public void Left()
    {
        up = false;
        down = false;
        left = true;
        right = false;
    }

    public void Right()
    {
        up = false;
        down = false;
        left = false;
        right = true;
    }

}
