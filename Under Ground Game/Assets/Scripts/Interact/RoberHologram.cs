using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoberHologram : MonoBehaviour
{
    public GameObject hologram;
    public GameObject[] drills;
    public GameObject[] wheels;
    public GameObject[] body;

    public GameObject drillMenu, wheelsMenu, bodyMenu;

    public Animator camAnimator, hologramAnimator;
    public BasicInteraction table;
    public int positionIndex = 0;
    bool alreadyInside, menusOpend;
    void Start()
    {
        hologram.SetActive(false);
        positionIndex = 0;
        camAnimator.SetInteger("Position", positionIndex);

        drillMenu.SetActive(false);
        bodyMenu.SetActive(false);
        wheelsMenu.SetActive(false);
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

        CheckForInput();

        if (Input.GetKeyDown(KeyCode.F) && !menusOpend && table.open)
        {
            OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            menusOpend = false;
            drillMenu.SetActive(false);
            wheelsMenu.SetActive(false);
            bodyMenu.SetActive(false);
        }
    }

    public void OpenMenu()
    {
        menusOpend = true;
        drillMenu.SetActive(false);
        wheelsMenu.SetActive(false);
        bodyMenu.SetActive(false);
        switch (positionIndex)
        {
            case -1:
                wheelsMenu.SetActive(true);
                break;

            case 0:
                bodyMenu.SetActive(true);
                break;

            case 1:
                drillMenu.SetActive(true);
                break;
        }
    }

    public void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && table.open && positionIndex > -1)
        {
            positionIndex--;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
            OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.A) && table.open && positionIndex == -1)
        {
            positionIndex = 1;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
            OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.D) && table.open && positionIndex < 1)
        {
            positionIndex++;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
            OpenMenu();
        }
        else if (Input.GetKeyDown(KeyCode.D) && table.open && positionIndex == 1)
        {
            positionIndex = -1;
            camAnimator.SetInteger("Position", positionIndex);
            hologramAnimator.SetInteger("Part", positionIndex);
            hologramAnimator.SetTrigger("Inside");
            OpenMenu();
        }
    }
    IEnumerator TurnOffAnimator()
    {
        yield return new WaitForSeconds(0.12f);
        camAnimator.enabled = false;
    }
}
