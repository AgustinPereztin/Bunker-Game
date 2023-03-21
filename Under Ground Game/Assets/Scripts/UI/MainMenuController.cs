using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject principalPanel;
    public GameObject optionsPanel;

    public Text graphics, music, sfx;

    private void Awake()
    {
        principalPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }

    public void PressOptions()
    {
        principalPanel.SetActive(false);
        optionsPanel.SetActive(true);
    }
    public void PressBack()
    {
        principalPanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void PressStart()
    {
        SceneManager.LoadScene(1);
    }
    public void PressContinue()
    {
        SceneManager.LoadScene(1);
    }
    public void PressQuit()
    {
        Application.Quit();
    }

    //Options Menu
    public void PressGraphics()
    {
        if(graphics.text == "ALTO")
        {
            graphics.text = "MEDIO";
        }
        else if(graphics.text == "MEDIO")
        {
            graphics.text = "BAJO";
        }
        else if (graphics.text == "BAJO")
        {
            graphics.text = "ALTO";
        }
    }
    public void PressMusic()
    {
        if (music.text == "ALTO")
        {
            music.text = "MEDIO";
        }
        else if (music.text == "MEDIO")
        {
            music.text = "BAJO";
        }
        else if (music.text == "BAJO")
        {
            music.text = "ALTO";
        }
    }
    public void PressSFX()
    {
        if (sfx.text == "ALTO")
        {
            sfx.text = "MEDIO";
        }
        else if (sfx.text == "MEDIO")
        {
            sfx.text = "BAJO";
        }
        else if (sfx.text == "BAJO")
        {
            sfx.text = "ALTO";
        }
    }

}
