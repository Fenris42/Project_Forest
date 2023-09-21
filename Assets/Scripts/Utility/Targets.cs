using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targets
{
    //public variables
    public Direction direction { get; set; }
    public float distance { get; set; }
    public bool outOfBounds { get; set; }

    
    //Constructors
    public Targets()
    {
        direction = new Direction();
        distance = 0;
        outOfBounds = false;
    }
    
}
