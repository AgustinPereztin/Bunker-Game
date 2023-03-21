using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComPuzzlePart : MonoBehaviour
{
    public Animator animator;
    public bool hasPower;
    public int type;
    public bool[] recives;
    public bool[] gives;
    public int recivingPos;
    public int initialRotation;
    void Start()
    {
        recivingPos = 4;

        recives = new bool[4];
        gives = new bool[4];

        switch (type)
        {
            case 0:
                for(int i = 0; i < gives.Length; i++)
                {
                    gives[i] = true;
                }
                break;

            case 1:

                recives[0] = true;
                recives[2] = true;

                break;

            case 2:

                recives[0] = true;
                recives[3] = true;

                break;

            case 3:

                for(int i = 0; i < recives.Length; i++)
                {
                    recives[i] = true;
                }

                break;

            case 4:

                recives[0] = true;
                recives[1] = true;
                recives[3] = true;

                break;

            case 5:

                for (int i = 0; i < recives.Length; i++)
                {
                    recives[i] = true;
                }

                break;

            case 6:

                for (int i = 0; i < recives.Length; i++)
                {
                    recives[i] = true;
                }

                break;
        }
    }

    private void Update()
    {
        if(type != 0 && type != 6)
        {
            for (int i = 0; i < gives.Length; i++)
            {
                if (i != recivingPos && recives[i])
                {
                    gives[i] = true;
                }
                else
                {
                    gives[i] = false;
                }
            }
        }
    }

    public void Rotate()
    {
        bool[] newRotation = new bool[4];
        for(int i = 0; i < recives.Length; i++)
        {
            if(recives[i] && i < 3)
            {
                newRotation[i+1] = true;
            }
            else if(recives[i] && i == 3)
            {
                newRotation[0] = true;
            }

            recives[i] = false;
        }

        for(int i = 0; i < recives.Length; i++)
        {
            if (newRotation[i])
            {
                recives[i] = true;
            }
        }
    }
}
