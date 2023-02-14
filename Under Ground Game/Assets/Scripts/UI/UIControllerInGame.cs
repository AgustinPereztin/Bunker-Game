using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerInGame : MonoBehaviour
{
    public Text eToInteract;
    // Start is called before the first frame update
    void Start()
    {
        eToInteract.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowEInteraction()
    {
        eToInteract.gameObject.SetActive(true);
    }
    public void HideEInteraction()
    {
        eToInteract.gameObject.SetActive(false);
    }
}
