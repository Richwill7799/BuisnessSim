using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowSliderValue : MonoBehaviour
{
    public Slider sliderUI;
    private Text textSliderValue;

    void Start()
    {
        textSliderValue = GetComponent<Text>();
        ShowValue();
    }
    private void Update()
    {
        ShowValue();
    }
    public void ShowValue()
    {
        string sliderMessage = "" + sliderUI.value;
        textSliderValue.text = sliderMessage;
    }
}