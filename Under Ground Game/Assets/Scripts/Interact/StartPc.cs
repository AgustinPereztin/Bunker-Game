using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPc : MonoBehaviour
{
    PlayerMovement pm;
    public GameObject interectText;
    bool inRange, open, onCooldown;

    public float cooldown;
    void Start()
    {
        interectText.SetActive(false);
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !open)
        {
            Debug.Log("Entra");
            open = true;
            interectText.SetActive(false);
            pm.canMove = false;
            pm.InteractComputer();
            onCooldown = true;
            StartCoroutine(Cooldown());
        }

        if (open && !onCooldown && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape)))
        {
            Debug.Log("Sale");
            open = false;
            interectText.SetActive(true);
            pm.canMove = true;
            pm.InteractComputer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            inRange = true;
            interectText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>() != null)
        {
            inRange = false;
            interectText.SetActive(false);
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }
}
