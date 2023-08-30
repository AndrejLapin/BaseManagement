using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] int resourceSlots = 50;
    int avalibleSlots = 0;
    Dictionary<ResourceType, int> resourceInventory = new Dictionary<ResourceType, int>();
    // Start is called before the first frame update
    void Start()
    {
        avalibleSlots = resourceSlots;

        for(int i = 0; i < (int)ResourceType.TotalTypes; i++)
        {
            resourceInventory.Add((ResourceType)i, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetAvalibleSlots()
    {
        return avalibleSlots;
    }

    public int AddToBackpack(ResourceType type, int amount)
    {
        int amountToAdd = amount > avalibleSlots ? avalibleSlots : amount;

        avalibleSlots -= amountToAdd;
        if(!resourceInventory.ContainsKey(type))
        {
            resourceInventory.Add(type, 0);
        }
        resourceInventory[type] += amountToAdd;
        return amountToAdd;
    }
}
