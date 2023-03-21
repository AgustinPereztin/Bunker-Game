using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComsManager : MonoBehaviour
{
    public CommunicationPuzzle[] puzzles;
    Bed bed;
    void Start()
    {
        ChangeDay();
    }

    public void ChangeDay()
    {
        for(int i = 0; i < puzzles.Length; i++)
        {
            puzzles[i].gameObject.SetActive(false); 
        }

        puzzles[bed.day].gameObject.SetActive(true);
    }
}
