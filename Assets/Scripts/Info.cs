using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    public static Button info;

    // Start is called before the first frame update
    void Start()
    {
        Button infoButton = info.GetComponent<Button>();
        infoButton.onClick.AddListener(TaskOnClick);
        Text infoBox = GameObject.Find("Infobox").GetComponent<Text>();
        Destroy(infoBox);
    }

    void TaskOnClick()
    {
        Text infoBox = GameObject.Find("Infobox").GetComponent<Text>();
        /*if(infoBox != null)
        {
            infoBox.enabled = false;
        }
        else
        {
            infoBox.enabled = true;
        }*/
    }
}
