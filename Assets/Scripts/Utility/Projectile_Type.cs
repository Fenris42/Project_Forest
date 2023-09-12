using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Type
{
    public bool arrow { get; set; }
    public bool fireball { get; set; }
    public bool iceball { get; set; }

    //Constructor
    public Projectile_Type()
    {
        arrow = false;
        fireball = false;
        iceball = false;
    }

    //Methods
    public void Arrow()
    {
        Reset();
        arrow = true;
    }

    public void Fireball()
    {
        Reset();
        fireball = true;
    }

    public void Iceball()
    {
        Reset();
        iceball = true;
    }

    public void Reset()
    {//reset all variables to false

        arrow = false;
        fireball = false;
        iceball = false;
    }
}
