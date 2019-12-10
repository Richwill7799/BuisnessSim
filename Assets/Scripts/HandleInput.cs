using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInput : MonoBehaviour
{
    
    public InputField inputFarmers;
    public Slider yearSlider;

    public int numFarmers;
    public int years;

    public void StartWithData() 
    {
        // assign values from input 
        numFarmers = int.Parse(inputFarmers.text);
        years = (int)yearSlider.value;

        // TODO load scene x

    }
}
