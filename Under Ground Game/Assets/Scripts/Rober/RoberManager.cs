using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberManager : MonoBehaviour
{
    public HologramMenus body, wheels, drill;

    RoberMovement movement;
    RoberMine mine;
    RoberInventory inventory;

    public float wheelsMaxHelath, wheelsCurrentHealth;
    public float bodyMaxHealth, bodyCurrentHealth;
    public float drillMaxHealth, drillCurrentHealth;

    float wheelsSpeedPenalty, maxWheelsPenalty;

    public bool inBase = true;
    void Start()
    {
        maxWheelsPenalty = 2f;

        movement = FindObjectOfType<RoberMovement>();
        mine = FindObjectOfType<RoberMine>();
        inventory = FindObjectOfType<RoberInventory>();

        StartCoroutine(LoosingHealth());
        StartCoroutine(Repair());
    }

    // Update is called once per frame
    void Update()
    {
        //Wheels
        wheelsMaxHelath = wheels.options[wheels.greenPosition].stat1;

        wheelsSpeedPenalty = (wheelsCurrentHealth / wheelsMaxHelath);
        wheelsSpeedPenalty = wheelsSpeedPenalty + (1 - wheelsSpeedPenalty) / maxWheelsPenalty;

        movement.baseSpeed = (wheels.options[wheels.greenPosition].stat2 / 10) * wheelsSpeedPenalty;

        //Body
        bodyMaxHealth = body.options[body.greenPosition].stat1;
        inventory.maxWeight = body.options[body.greenPosition].stat3;

        if(bodyCurrentHealth <= 0)
        {
            movement.GoToBase();
        }

        //Drill
        drillMaxHealth = drill.options[drill.greenPosition].stat1;
        mine.miningSpeed = 2 / (drill.options[drill.greenPosition].stat2/100);

        if(drillCurrentHealth <= 0)
        {
            movement.GoToBase();
        }


        //Repair
        if (Input.GetKeyDown(KeyCode.Mouse2) && inBase)
        {
            StartCoroutine(Repair());
        }
    }

    public IEnumerator Repair()
    {
        yield return new WaitForSeconds(1);
        wheelsCurrentHealth = wheelsMaxHelath;
        bodyCurrentHealth = bodyMaxHealth;
        drillCurrentHealth = drillMaxHealth;
    }

    IEnumerator LoosingHealth()
    {

        while (true)
        {
            yield return new WaitForSeconds(2);
            if (movement.alreadyGoing)
            {
                wheelsCurrentHealth--;
                bodyCurrentHealth--;
            }
        }
    }
}
