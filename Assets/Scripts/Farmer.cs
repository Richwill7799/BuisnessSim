using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//every farmer has a field and a startValue ''the first corn'' but in kilograms
public class Farmer : MonoBehaviour
{
    private Field field; //his own field
    private List<Farmer> collabFarmer = new List<Farmer>();
    public string name;
    public Color color;
    public Color nameColor;

    public Farmer(Field field, int startValue, string name, Color color)
    {
        this.field = field;
        this.field.SetStartValue(startValue);
        this.name = name;
        this.color = color;
    }

    public List<Farmer> GetCollabFarmer()
    {
        return collabFarmer;
    }
    public Field GetField()
    {
        return field;
    }
    public void SetCollabFarmer(Farmer farmer)
    {
        collabFarmer.Add(farmer);
    }

    public void SetNameColor(Color color)
    {
        nameColor = color;
    }

    public bool HasNoCollabFarmer()
    {
        return collabFarmer.Count == 0;
    }
}
