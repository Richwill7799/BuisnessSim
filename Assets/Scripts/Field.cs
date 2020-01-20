using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//the class field holds every property and growth/loss rate
//also the harvest is part of the field, bc it depends on the properies

public class Field : MonoBehaviour
{

    private float harvest; //in kilograms
    private float startValue;
    private float multiplier; //changes every year
    private int variant;
    private bool weatherChangedByUser;

    public List<float> allHarvest = new List<float>();

    public Field(int variant)
    {
        harvest = 0;
        startValue = 0;
        multiplier = 0;
        weatherChangedByUser = false;
        this.variant = variant; //this is the variant of the field. means: every two fields are different, so we have variant 0 and variant 1 e.g. four fields, where each two are equivalent, so we have 2 fields with variant 0 and two fields with variant 1 
    }

    public void SimulateField()
    {
        if (harvest == 0)
        {
            harvest = startValue * multiplier;
        }
        else
        {
            harvest *= multiplier;
        }
        allHarvest.Add(harvest);
    }

    public float GetHarvest()
    {
        return harvest;
    }

    public void SetHarvest(float harvest)
    {
        this.harvest = harvest;
    }

    public void SetStartValue(float startValue)
    {
        this.startValue = startValue;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }

    public void SetMultiplier(float multiplier)
    {
        this.multiplier = multiplier;
    }
    public int GetVariant()
    {
        return variant;
    }

    public bool IsWeatherChangedByUser() {
        return weatherChangedByUser;
    }

    public void SetWeatherChangeByUser(bool weatherChanged) {
        weatherChangedByUser = weatherChanged;
    }
}
