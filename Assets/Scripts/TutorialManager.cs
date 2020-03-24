using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] popUps;

    private int popUpIndex = 0;
    private bool wantTutorial = true;

    private void Update()
    {
        if (wantTutorial)
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
        wantTutorial = true;
        ClosePopUp();
    }

    public void DontWantTutorial()
    {
        wantTutorial = false;
        CloseTutorial();
    }

    public void ClosePopUp()
    {
        popUps[popUpIndex].SetActive(false);
        popUpIndex++;
    }

}
