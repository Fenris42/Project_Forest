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
    public Direction(Direction direction)
    {
        this.up = direction.up;
        this.down = direction.down;
        this.left = direction.left;
        this.right = direction.right;
    }

    //Set direction methods
    public void Up()
    {
        Reset();
        up = true;
    }

    public void Down()
    {
        Reset();
        down = true;
    }

    public void Left()
    {
        Reset();
        left = true;
    }

    public void Right()
    {
        Reset();
        right = true;
    }

    public void Reset()
    {
        up = false;
        down = false;
        left = false;
        right = false;
    }
}
