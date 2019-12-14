using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerClickBehaviour : MonoBehaviour
{
    private bool clicked;
    public GameObject field;

    // Start is called before the first frame update
    void Start()
    {
        //Sprite farmer = GetComponent<Sprite>();
        //farmer.enable = true;
    }

    void OnMouseDown()
    {   
        if (field.activeSelf)
        {
            //How to get enable from EmptyGameObject
            //field.enable = false;
            field.SetActive(false);

        }
        else
        {
            //How to get enable from EmptyGameObject
            //field.enable = false;
            field.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void OnGUI()
    {
        if (clicked)
        {
            //GUI.DrawTexture(new Rect(10, 10, 100, 90), Texture ???);
            GUI.Box(new Rect(10, 10, 100, 90), "Field");
        }
    }*/
}
