using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Type
{
    public bool arrow { get; set; }

    //Constructor
    public Projectile_Type()
    {

    }

    //Methods
    public void Arrow()
    {
        Reset();
        arrow = true;
    }

    private void Reset()
    {//reset all variables to false

        arrow = false;
    }
}
