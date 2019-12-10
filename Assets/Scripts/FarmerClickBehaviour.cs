using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerClickBehaviour : MonoBehaviour
{
    private bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        //Sprite farmer = GetComponent<Sprite>();
        //farmer.enable = true;
    }

    void OnMouseDown()
    {
        if (clicked)
        {
            clicked = false;
        }
        else
        {
            clicked = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        if (clicked)
        {
            //GUI.DrawTexture(new Rect(10, 10, 100, 90), Texture ???);
            GUI.Box(new Rect(10, 10, 100, 90), "Field");
        }
    }
}
