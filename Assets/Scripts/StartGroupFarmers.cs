using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGroupFarmers : MonoBehaviour
{
    public bool isInTeam = false;
    private int clicked = 0;

    private Button btn;
    void Start()
    {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(ClickToJoinAndLeftTeam);
    }

    // TODO
    private void ClickToJoinAndLeftTeam()
    {
        clicked++;
        //deselected button
        if (clicked % 2 == 0)
        {
            isInTeam = false;
            btn.image.color = Color.white;
            //  ColorBlock cb = btn.colors;
            //  cb.normalColor = Color.white;
            //  btn.colors = cb;
        }
        //selected button
        else
        {
            isInTeam = true;
            btn.image.color = Color.gray; 
            // ColorBlock cb = btn.colors;
            // cb.normalColor = Color.gray;
            // btn.colors = cb;
        }
    }
}
