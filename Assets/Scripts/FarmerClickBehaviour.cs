using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FarmerClickBehaviour : MonoBehaviour
{
    public GameObject field;
    private GameObject WeatherTut;
    private HandleInput hi;

    private void Start()
    {
        hi = GameObject.FindWithTag("Information").GetComponent<HandleInput>();

        WeatherTut = GameObject.FindWithTag("WeatherTut");
        WeatherTut.GetComponent<Canvas>().enabled = false;
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if (field.activeSelf)
        {
            field.SetActive(false);
        }
        else
        {
            field.SetActive(true);
            if (hi.tutorial)
            {
                if (!WeatherTut.GetComponent<ActivatedWeatherTut>().activated)
                {
                    WeatherTut.GetComponent<Canvas>().enabled = true;
                    WeatherTut.GetComponent<ActivatedWeatherTut>().activated = true;
                }
            }
        }

    }
}
