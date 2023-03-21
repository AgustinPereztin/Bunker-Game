using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bed : MonoBehaviour
{
    public int day;
    public Image fundido, superFundido;
    public GameObject dayText;
    public Text dayTexttxt;
    PlayerMovement player;
    public bool inRange;

    ComsManager comsPuzzle;
    ElectricPanelMinigame1[] electricPanel;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        dayText.SetActive(false);
        fundido.CrossFadeAlpha(0,0,false);
        superFundido.CrossFadeAlpha(0,0.5f,false);

        electricPanel = FindObjectsOfType<ElectricPanelMinigame1>();
        comsPuzzle = FindObjectOfType<ComsManager>();
    }
    void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.E))
        {
            NextDay();
        }
    }
    public void NextDay()
    {
        bool allLightsOn = true;

        for(int i = 0; i < electricPanel.Length; i++)
        {
            if (!electricPanel[i].completed)
            {
                allLightsOn = false;
            }
        }

        if(comsPuzzle.puzzles[day].completed && allLightsOn)
        {
            StartCoroutine(SleepingSecuence());
        }
    }

    IEnumerator SleepingSecuence()
    {
        day++;
        comsPuzzle.ChangeDay();
        dayTexttxt.text = "Day " + day;
        player.canMove = false;
        fundido.CrossFadeAlpha(1,1,false);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < electricPanel.Length; i++)
        {
            electricPanel[i].turnOffLights();
        }
        dayText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        superFundido.CrossFadeAlpha(1,1,false);
        yield return new WaitForSeconds(1f);
        dayText.SetActive(false);
        fundido.CrossFadeAlpha(0,1,false);
        superFundido.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1);
        player.canMove = true;
    }
}
