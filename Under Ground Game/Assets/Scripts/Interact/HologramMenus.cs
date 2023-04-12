using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HologramMenus : MonoBehaviour
{
    public HologramMenuOptions[] options;
    public int optionIndex, greenPosition;

    public string stat1, stat2, stat3;

    public Text stat1Txt, stat2Txt, stat3Txt;

    void Start()
    {
        greenPosition = 0;
        optionIndex = 0;
    }

    void Update()
    {
        options[greenPosition].animator.SetBool("Green", true);
        options[optionIndex].animator.SetBool("Selected", true);

        if (Input.GetKeyDown(KeyCode.F))
        {
            for (int i = 0; i < options.Length; i++)
            {
                options[i].animator.SetBool("Green", false);
            }
            greenPosition = optionIndex;
            options[greenPosition].animator.SetBool("Green", true);

            StartCoroutine(FindObjectOfType<RoberManager>().Repair());
        }
        ChangeOption();

        CompareStats();
    }

    public void CompareStats()
    {
        if(options[greenPosition].stat1 == options[optionIndex].stat1)
        {
            stat1Txt.text = stat1 + ": " + options[optionIndex].stat1 + " = " + options[greenPosition].stat1;
        }
        else if(options[optionIndex].stat1 > options[greenPosition].stat1)
        {
            stat1Txt.text = stat1 + ": <color=green>" + options[optionIndex].stat1  + " > " + options[greenPosition].stat1 + "</color>";
        }
        else
        {
            stat1Txt.text = stat1 + ": <color=red>" + options[optionIndex].stat1 + " < " + options[greenPosition].stat1 + "</color>";
        }


        if (options[greenPosition].stat2 == options[optionIndex].stat2)
        {
            stat2Txt.text = stat2 + ": " + options[optionIndex].stat2  + " = " + options[greenPosition].stat2;
        }
        else if (options[optionIndex].stat2 > options[greenPosition].stat2)
        {
            stat2Txt.text = stat2 + ": <color=green>" + options[optionIndex].stat2 + " > " + options[greenPosition].stat2 + "</color>";
        }
        else
        {
            stat2Txt.text = stat2 + ": <color=red>" + options[optionIndex].stat2 + " < " + options[greenPosition].stat2 + "</color>";
        }

        if (options[greenPosition].stat3 == options[optionIndex].stat3)
        {
            stat3Txt.text = stat3 + ": " + options[optionIndex].stat3  + " = " + options[greenPosition].stat3;
        }
        else if (options[optionIndex].stat3 > options[greenPosition].stat3)
        {
            stat3Txt.text = stat3 + ": <color=green>" + options[optionIndex].stat3 + " > " + options[greenPosition].stat3 + "</color>";
        }
        else
        {
            stat3Txt.text = stat3 + ": <color=red>" + options[optionIndex].stat3 + " < " + options[greenPosition].stat3 + "</color>";
        }
    }

    public void ChangeOption()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if(optionIndex > 0)
            {
                optionIndex--;
            }
            else
            {
                optionIndex = 2;
            }
            for (int i = 0; i < options.Length; i++)
            {
                options[i].animator.SetBool("Selected", false);
            }
            options[optionIndex].animator.SetBool("Selected", true);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if(optionIndex < 2)
            {
                optionIndex++;
            }
            else
            {
                optionIndex = 0;
            }
            for (int i = 0; i < options.Length; i++)
            {
                options[i].animator.SetBool("Selected", false);
            }
            options[optionIndex].animator.SetBool("Selected", true);
        }
        
    }
}
