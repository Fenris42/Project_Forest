using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Menu : MonoBehaviour
{
    //serialized fields
    [SerializeField] private GameObject ItemSlotPrefab;
    [SerializeField] private GameObject contentPanel;

    //public variables

    //private variables
    private List<Inventory_Item> items = new List<Inventory_Item>();

    void Start()
    {// Start is called before the first frame update
        AddItemSlot();
    }

    
    void Update()
    {// Update is called once per frame

    }

    public void AddItemSlot()
    {
        GameObject slot = Instantiate(ItemSlotPrefab, Vector3.zero, Quaternion.identity);
        slot.transform.SetParent(contentPanel.transform);
        slot.transform.localScale = Vector3.one;
    }
}
