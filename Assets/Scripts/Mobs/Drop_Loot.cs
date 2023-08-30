using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop_Loot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropLoot(string lootTable)
    {
        lootTable = lootTable.ToLower();

        if (lootTable == "trash")
        {

        }
    }
}
