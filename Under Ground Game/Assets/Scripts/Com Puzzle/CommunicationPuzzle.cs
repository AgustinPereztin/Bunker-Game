
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommunicationPuzzle : MonoBehaviour
{
    public GameObject portada;
    public int day;
    public BasicInteraction comsPanel;
    public ComPuzzlePart[] part1;
    public ComPuzzlePart[] part2;
    public ComPuzzlePart[] part3;
    public int partIndexX, partIndexY;
    void Start()
    {
        
    }

    void Update()
    {
        if(comsPanel.open && portada.activeInHierarchy)
        {
            portada.SetActive(false);
            partIndexX = 0;
            partIndexY = 0;
            ChangePosition();
        }

        if (comsPanel.open)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(-1, 0);
            }else if (Input.GetKeyDown(KeyCode.D))
            {
                Move(1, 0);
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Move(0, -1);
            }else if (Input.GetKeyDown(KeyCode.S))
            {
                Move(0, 1);
            }
        }
    }

    public void ChangePosition()
    {
        for(int i = 0; i < part1.Length; i++)
        {
            part1[i].animator.SetBool("Selected", false);
        }

        for (int i = 0; i < part2.Length; i++)
        {
            part2[i].animator.SetBool("Selected", false);
        }

        for (int i = 0; i < part3.Length; i++)
        {
            part3[i].animator.SetBool("Selected", false);
        }

        switch (partIndexY)
        {
            case 0:
                part1[partIndexX].animator.SetBool("Selected", true);
                break;

            case 1:
                part2[partIndexX].animator.SetBool("Selected", true);
                break;

            case 2:
                part3[partIndexX].animator.SetBool("Selected", true);
                break;
        }
    }

    public void Move(int x, int y)
    {
        if(partIndexX > 0 && x == -1)
        {
            partIndexX--;
        }
        else if(partIndexX < part1.Length-1 && x == +1)
        {
            partIndexX++;
        }

        if (partIndexY > 0 && y == -1)
        {
            partIndexY--;
        }
        else if (partIndexY < 2 && y == +1)
        {
            partIndexY++;
        }

        ChangePosition();
    }
}
