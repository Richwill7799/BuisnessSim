using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowGraph : MonoBehaviour
{
    public GameObject graph;
    public Text text;
   
    public void PressShowHide() {
      
        graph.SetActive(!graph.activeSelf);
        if (graph.activeSelf == false)
            text.text = ">";
        else
            text.text = "<";
    }
}
