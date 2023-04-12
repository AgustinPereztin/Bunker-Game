using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTileMap : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<RoberMovement>())
        {
            FindObjectOfType<RoberMovement>().model.SetActive(false);
            FindObjectOfType<RoberManager>().inBase = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<RoberMovement>())
        {
            FindObjectOfType<RoberMovement>().model.SetActive(true);
            FindObjectOfType<RoberManager>().inBase = false;
        }
    }
}
