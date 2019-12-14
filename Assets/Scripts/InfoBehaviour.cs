using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBehaviour : MonoBehaviour
{
    public void InfoClick()
    {
        Text infoBox = GameObject.Find("Infobox").GetComponent<Text>();

        if(infoBox.enabled)
        {
            infoBox.enabled = false;
        }
        else
        {
            infoBox.enabled = true;
        }
    }
}
