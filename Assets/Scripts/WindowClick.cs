using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowClick : MonoBehaviour
{
    private bool clicked;
 
    void Update()
    {
        if(Input.GetKeyDown("k"))
        {
            if(clicked)
            {
                clicked = false;
            }  
            else
            {
                clicked = true;
            }
        }
    }

    void OnGUI()
    {
        if(clicked)
        {
            //GUI.DrawTexture(new Rect(10, 10, 100, 90), Texture ???);
            GUI.Box(new Rect(10, 10, 100, 90), "Field");
        }
    }
}
