
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

    private int[,] partRotation;
    public int partIndexX, partIndexY;

    public bool completed;
    public ComPuzzlePart[] endParts;

    bool isReady = false;
    float timePased = 0;
    void Start()
    {
        

    }
    public void Rotate(int position, int positionY)
    {
        partRotation[position, positionY] += 90;
        switch (positionY)
        {
            case 0:
                part1[position].transform.localRotation = Quaternion.Euler(0, -90, part1[position].transform.localRotation.z - partRotation[position, positionY]);
                part1[position].Rotate();
                break;

            case 1:
                part2[position].transform.localRotation = Quaternion.Euler(0, -90, part2[position].transform.localRotation.z - partRotation[position, positionY]);
                part2[position].Rotate();
                break;

            case 2:
                part3[position].transform.localRotation = Quaternion.Euler(0, -90, part3[position].transform.localRotation.z - partRotation[position, positionY]);
                part3[position].Rotate();
                Debug.Log("Entra tercera fila");
                break;
        }
    }

    void Update()
    {

        timePased += Time.deltaTime;

        if(timePased > 2 && !isReady)
        {
            isReady = true;
            Debug.Log("Entra1");

            partRotation = new int[part1.Length, 3];

            int endPartsIndex = 0;
            for (int i = 0; i < part1.Length; i++)
            {
                Debug.Log("Entra2");
                for (int j = 0; j < part1[i].initialRotation; j++)
                {
                    Rotate(i, 0);
                }
                if (part1[i].type == 6)
                {
                    Debug.Log("Entra3");
                    endParts[endPartsIndex] = part1[i];
                    endPartsIndex++;
                }
            }
            for (int i = 0; i < part2.Length; i++)
            {
                for (int j = 0; j < part2[i].initialRotation; j++)
                {
                    Rotate(i, 1);
                }
                if (part2[i].type == 6)
                {
                    endParts[endPartsIndex] = part2[i];
                    endPartsIndex++;
                }
            }
            for (int i = 0; i < part3.Length; i++)
            {
                for (int j = 0; j < part3[i].initialRotation; j++)
                {
                    Rotate(i, 2);
                }

                if (part3[i].type == 6)
                {
                    endParts[endPartsIndex] = part3[i];
                    endPartsIndex++;
                }
            }
        }

        if(comsPanel.open && portada.activeInHierarchy)
        {
            portada.SetActive(false);
            partIndexX = 0;
            partIndexY = 0;
            ChangePosition();
        }
        else if(!comsPanel.open)
        {
            portada.SetActive(false);
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

            if (Input.GetKeyDown(KeyCode.F))
            {
                Rotate(partIndexX, partIndexY);
            }

            for (int i = 0; i < part1.Length; i++)
            {
                if (part1[i].hasPower)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (part1[i].gives[j])
                        {
                            switch (j)
                            {
                                case 0:
                                    if (i > 0 && part1[i].recivingPos != 0)
                                    {
                                        if (part1[i - 1].recives[2])
                                        {
                                            part1[i - 1].hasPower = true;
                                            part1[i - 1].recivingPos = 2;
                                            part1[i - 1].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 2:
                                    if (i < part1.Length - 1 && part1[i].recivingPos != 2)
                                    {
                                        if (part1[i + 1].recives[0])
                                        {
                                            part1[i + 1].hasPower = true;
                                            part1[i + 1].recivingPos = 0;
                                            part1[i + 1].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 3:
                                    if (part1[i].recivingPos != 3)
                                    {
                                        if (part2[i].recives[1])
                                        {
                                            part2[i].hasPower = true;
                                            part2[i].recivingPos = 1;
                                            part2[i].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < part2.Length; i++)
            {
                if (part2[i].hasPower)
                {
                    for (int j = 0; j < 4; j++)
                    {

                        if (part2[i].gives[j])
                        {
                            switch (j)
                            {
                                case 0:
                                    if (i > 0 && part2[i].recivingPos != 0)
                                    {
                                        if (part2[i - 1].recives[2])
                                        {
                                            part2[i - 1].hasPower = true;
                                            part2[i - 1].recivingPos = 2;
                                            part2[i - 1].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 1:
                                    if (part2[i].recivingPos != 1)
                                    {
                                        if (part1[i].recives[3])
                                        {
                                            part1[i].hasPower = true;
                                            part1[i].recivingPos = 3;
                                            part1[i].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 2:
                                    if (i < part2.Length - 1 && part2[i].recivingPos != 2)
                                    {
                                        if (part2[i + 1].recives[0])
                                        {
                                            part2[i + 1].hasPower = true;
                                            part2[i + 1].recivingPos = 0;
                                            part2[i + 1].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 3:
                                    if (part2[i].recivingPos != 3)
                                    {
                                        if (part3[i].recives[1])
                                        {
                                            part3[i].hasPower = true;
                                            part3[i].recivingPos = 1;
                                            part3[i].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

            }
            for (int i = 0; i < part3.Length; i++)
            {
                if (part3[i].hasPower)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (part3[i].gives[j])
                        {
                            switch (j)
                            {
                                case 0:
                                    if (i > 0 && part3[i].recivingPos != 0)
                                    {
                                        if (part3[i - 1].recives[2])
                                        {
                                            part3[i - 1].hasPower = true;
                                            part3[i - 1].recivingPos = 2;
                                            part3[i - 1].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 1:
                                    if (part3[i].recivingPos != 1)
                                    {
                                        if (part2[i].recives[3])
                                        {
                                            part2[i].hasPower = true;
                                            part2[i].recivingPos = 3;
                                            part2[i].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                                case 2:
                                    if (i < part3.Length - 1 && part3[i].recivingPos != 2)
                                    {
                                        if (part3[i + 1].recives[0])
                                        {
                                            part3[i + 1].hasPower = true;
                                            part3[i + 1].recivingPos = 0;
                                            part3[i + 1].animator.SetFloat("Prendido", 1);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

            }

            for (int i = 0; i < part1.Length; i++)
            {
                if (part1[i].hasPower && part1[i].type != 0)
                {
                    bool hasPower = false;
                    if (i > 0 && part1[i].recives[0])
                    {
                        if (part1[i - 1].gives[2] && part1[i - 1].hasPower)
                        {
                            hasPower = true;
                            part1[i].recivingPos = 0;
                        }
                    }
                    if (i < part1.Length - 1 && part1[i].recives[2])
                    {
                        if (part1[i + 1].gives[0] && part1[i + 1].hasPower)
                        {
                            part1[i].recivingPos = 2;
                            hasPower = true;
                        }
                    }
                    if (part1[i].recives[3])
                    {
                        if (part2[i].gives[1] && part2[i].hasPower)
                        {
                            part1[i].recivingPos = 3;
                            hasPower = true;
                        }
                    }

                    if (!hasPower)
                    {
                        part1[i].hasPower = false;
                        part1[i].animator.SetFloat("Prendido", 0);
                    }
                    else
                    {
                        part1[i].hasPower = true;
                        part1[i].animator.SetFloat("Prendido", 1);
                    }
                }
            }
            for (int i = 0; i < part2.Length; i++)
            {
                if (part2[i].hasPower && part2[i].type != 0)
                {
                    bool hasPower = false;
                    if (i > 0 && part2[i].recives[0])
                    {
                        if (part2[i - 1].gives[2] && part2[i - 1].hasPower)
                        {
                            part2[i].recivingPos = 0;
                            hasPower = true;
                        }
                    }
                    if (part2[i].recives[1])
                    {
                        if (part1[i].gives[3] && part1[i].hasPower)
                        {
                            part2[i].recivingPos = 1;
                            hasPower = true;
                        }
                    }
                    if (i < part2.Length - 1 && part2[i].recives[2])
                    {
                        if (part2[i + 1].gives[0] && part2[i + 1].hasPower)
                        {
                            part2[i].recivingPos = 2;
                            hasPower = true;
                        }
                    }
                    if (part2[i].recives[3])
                    {
                        if (part3[i].gives[1] && part3[i].hasPower)
                        {
                            part2[i].recivingPos = 3;
                            hasPower = true;
                        }
                    }

                    if (!hasPower)
                    {
                        part2[i].hasPower = false;
                        part2[i].animator.SetFloat("Prendido", 0);
                    }
                    else
                    {
                        part2[i].hasPower = true;
                        part2[i].animator.SetFloat("Prendido", 1);
                    }
                }
            }
            for (int i = 0; i < part3.Length; i++)
            {
                if (part3[i].hasPower && part3[i].type != 0)
                {
                    bool hasPower = false;
                    if (i > 0 && part3[i].recives[0])
                    {
                        if (part3[i - 1].gives[2] && part3[i - 1].hasPower)
                        {
                            part3[i].recivingPos = 0;
                            hasPower = true;
                        }
                    }
                    if (part3[i].recives[1])
                    {
                        if (part2[i].gives[3] && part2[i].hasPower)
                        {
                            part3[i].recivingPos = 1;
                            hasPower = true;
                        }
                    }
                    if (i < part3.Length - 1 && part3[i].recives[2])
                    {
                        if (part3[i + 1].gives[0] && part3[i + 1].hasPower)
                        {
                            part3[i].recivingPos = 2;
                            hasPower = true;
                        }
                    }

                    if (!hasPower)
                    {
                        part3[i].hasPower = false;
                        part3[i].animator.SetFloat("Prendido", 0);
                    }
                    else
                    {
                        part3[i].hasPower = true;
                        part3[i].animator.SetFloat("Prendido", 1);
                    }
                }
            }

            //check if is completed
            bool isCompletedCheck = true;
            for (int i = 0; i < endParts.Length; i++)
            {
                if (!endParts[i].hasPower)
                {
                    isCompletedCheck = false;
                }
            }

            if (isCompletedCheck && !completed)
            {
                completed = true;
            }
        }
    }

    public void ChangePosition()
    {
        for(int i = 0; i < part1.Length; i++)
        {
            part1[i].animator.SetFloat("Seleccionado", 0);
        }

        for (int i = 0; i < part2.Length; i++)
        {
            part2[i].animator.SetFloat("Seleccionado", 0);
        }

        for (int i = 0; i < part3.Length; i++)
        {
            part3[i].animator.SetFloat("Seleccionado", 0);
        }

        switch (partIndexY)
        {
            case 0:
                part1[partIndexX].animator.SetFloat("Seleccionado", 1);
                break;

            case 1:
                part2[partIndexX].animator.SetFloat("Seleccionado", 1);
                break;

            case 2:
                part3[partIndexX].animator.SetFloat("Seleccionado", 1);
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
