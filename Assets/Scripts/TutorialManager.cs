using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;

    private int popUpIndex = 0;
    private HandleInput hi;

    private void Start()
    {
        hi = GameObject.FindWithTag("Information").GetComponent<HandleInput>();
    }
    private void Update()
    {

        if (!hi.tutorial)
        {
            foreach (GameObject popUp in popUps)
            {
                popUp.SetActive(false);
            }
        }
        if (hi.tutorial)
        {
            ////Debug.Log(popUps[popUpIndex].name);
            for (int i = 0; i < popUps.Length; i++)
            {
                if (i == popUpIndex)
                {
                    popUps[popUpIndex].SetActive(true);
                }
                else
                {
                    popUps[i].SetActive(false);
                }
            }
        }


    }

    private void CloseTutorial()
    {
        foreach (GameObject go in popUps)
        {
            go.SetActive(false);
        }
    }

    //button functions
    public void WantTutorial()
    {
        hi.tutorial = true;
        ClosePopUp();
    }

    public void DontWantTutorial()
    {
        hi.tutorial = false;
        CloseTutorial();
    }

    public void ClosePopUp()
    {
        popUps[popUpIndex].SetActive(false);
        popUpIndex++;
    }

    public void CloseWeather() {
        GameObject.FindWithTag("WeatherTut").GetComponent<Canvas>().enabled = false; 
    }
}
