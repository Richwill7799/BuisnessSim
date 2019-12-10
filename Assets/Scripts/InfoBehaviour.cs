using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBehaviour : MonoBehaviour
{
    //private bool clicked;

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

    public void FieldClick()
    {
        Image field = GameObject.Find("Field").GetComponent<Image>();
        Text text = GameObject.Find("Stats").GetComponent<Text>();
        Button x = GameObject.Find("Button").GetComponent<Button>();

        //Button does not get disabled
        x.enabled = false;
        text.enabled = false;
        field.enabled = false;
    }

    public void FarmerClick()
    {
        Image field = GameObject.Find("Field").GetComponent<Image>();
        Button x = GameObject.Find("Button").GetComponent<Button>();

        x.enabled = true;
        field.enabled = true;
    }

    /*void OnGui()
    {
        if(clicked)
        {

        }
    }*/
}
