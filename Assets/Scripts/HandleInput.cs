using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInput : MonoBehaviour
{
    
    public InputField inputFarmers;
    public Slider yearSlider;

    private int numFarmers;
    private int years;
    private int currentVariant = 2; // or 1 if 0 is valide

    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();

    public void StartWithData() 
    {
        // assign values from input 
        numFarmers = int.Parse(inputFarmers.text);
        years = (int)yearSlider.value;

        // TODO load scene x

    }

    public int getNumFarmers() {
        return numFarmers;
    }

    public int getYears() 
    {
        return years;
    }
}
