using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberInventory : MonoBehaviour
{
    public float maxWeight, currentWeight;
    public int maxItems;
    public int[] cuantity;
    public string[] name ;

    public void AddItem(Item item)
    {
        for(int i = 0; i < maxItems; i++)
        {
            if((name[i] == "" || name[i] == item.name) && (currentWeight + item.weight <= maxWeight))
            {
                cuantity[i] += 1;
                name[i] = item.name;
                currentWeight += item.weight;
                return;
            }
        }

        for(int i = 0; i < maxItems; i++)
        {
            Debug.Log(name[i] + " " + cuantity[i]);
        }
    }

    public void EmptyInventory()
    {
        for(int i = 0; i < maxItems; i++)
        {
            name[i] = "";
            cuantity[i] = 0;
            currentWeight = 0;
        }
    }
}
