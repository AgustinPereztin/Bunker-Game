using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberHologram : MonoBehaviour
{
    public GameObject hologram;
    public GameObject[] drills;
    public GameObject[] wheels;
    public GameObject[] body;

    public Animator camAnimator, hologramAnimator;
    public BasicInteraction table;
    public int positionIndex = 0;
    bool alreadyInside;
    void Start()
    {
        hologram.SetActive(false);
        positionIndex = 0;
        camAnimator.SetInteger("Position", positionIndex);
    }

    void Update()
    {
        if(table.open && !alreadyInside)
        {
            camAnimator.enabled = true;
            alreadyInside = true;
            positionIndex = 0;
            camAnimator.SetTrigger("Inside");
            camAnimator.SetInteger("Position", positionIndex);
            hologram.SetActive(true);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
        }

        if (!table.open && alreadyInside)
        {
            alreadyInside= false;
            camAnimator.SetTrigger("Outside");
            hologram.SetActive(false);
            StartCoroutine(TurnOffAnimator());
        }

        if (Input.GetKeyDown(KeyCode.A) && table.open && positionIndex > -1)
        {
            positionIndex--;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
        }
        else if (Input.GetKeyDown(KeyCode.A) && table.open && positionIndex == -1)
        {
            positionIndex = 1;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
        }

        if(Input.GetKeyDown(KeyCode.D) && table.open && positionIndex < 1)
        {
            positionIndex++;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
        }
        else if(Input.GetKeyDown(KeyCode.D) && table.open && positionIndex == 1)
        {
            positionIndex = -1;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
        }

        
    }

    IEnumerator TurnOffAnimator()
    {
        yield return new WaitForSeconds(0.12f);
        camAnimator.enabled = false;
    }
}
