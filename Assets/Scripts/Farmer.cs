using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//every farmer has a field and a startValue ''the first corn'' but in kilograms
public class Farmer : MonoBehaviour
{
    private Field field; //his own field
    private List<Farmer> collabFarmer;
    public string name;

    public Farmer(Field field, int startValue, string name)
    {
        this.field = field;
        this.field.SetStartValue(startValue);
        collabFarmer = null;
        this.name = name;
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

    public bool HasNoCollabFarmer()
    {
        return collabFarmer is null;
    }
}
