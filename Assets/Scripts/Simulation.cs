﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


public class Simulation : MonoBehaviour
{
    //private variables
    private int year;
    private int countFarmers;
    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<int> variants = new List<int>();

    //public variables
    public Text Year;

    // Start is called before the first frame update
    void Start()
    {
        year = 0;
        countFarmers = 4; //TODO: this should be editable for the user at the beginning via input field, MIN: 4, MAX: ?   
        InstantiateLists();
    }

    private void InstantiateLists()
    {
        //first fields, second farmers with fields

        //instantiate a field and it's variant
        int variant = 0;
        for (int i = 0; i < countFarmers / 2; i++)
        {//each field
            for (int j = 0; j < countFarmers / 2; j++)
            {//each variant of a field, in this case two different variants, this can be extended in future
                Field field = new Field(variant);
                fields.Add(field);
                variants.Add(variant);
            }
            variant += 1;
        }

        //insantiate for each field a farmer, with startvalue as 1 kg
        int bauernname = 1;
        foreach (Field field in fields)
        {
            Farmer farmer = new Farmer(field, 1, "bauer" + bauernname);
            farmers.Add(farmer);
            bauernname++;
        }

        //instantiate the CollabFarmers - FIRST VERSION only two collab. farmers - TODO this should be done by the User in future
        Farmer f = farmers[0];
        for (int i = 1; i < farmers.Count; i++)
        {
            if (f.GetField().GetVariant() != farmers[i].GetField().GetVariant())
            {
                f.SetCollabFarmer(farmers[i]);
                farmers[i].SetCollabFarmer(f);
                break; //this is absolutely not wanted, this has to be rewrited!!!!**************
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            PassYear();
            year++;
            Year.text = "Year " + year;
        }
    }

    private void PassYear()
    {
        SetMultiplier(); //set multiplier each year - to be edited with weather ect
        foreach (Field field in fields)
        {
            field.SimulateField();
        }

        Collaboration();

        //Debug 
        foreach (Farmer f in farmers)
        {
            Debug.Log(f.name + ": " + f.GetField().GetHarvest());
        }

        Visualisierung(); //todo for everyone who wants this  xD
    }

    private void Collaboration()
    {
        //now combine the farmer's harvest if they collab and then halve it and give each collab farmer the half of the collab.Harvest
        Farmer fa = farmers.First(x => !x.HasNoCollabFarmer()); //this should find in any case a farmer. if not, the simulation is corrupt xD
        //get the collab farmer
        float harvestF = fa.GetField().GetHarvest();
        float harvestCollabF = fa.GetCollabFarmer().GetField().GetHarvest();
        float collabHarvest = (harvestF + harvestCollabF) / 2;
        fa.GetField().SetHarvest(collabHarvest); // set the harvest now to tzhe half of the collab harvest
        fa.GetCollabFarmer().GetField().SetHarvest(collabHarvest); // set the harvest now to tzhe half of the collab harvest
    }

    private void SetMultiplier() //set the multiplier for each variant of field, bc each field with equal variant has the same multiplier
    {
        foreach (int variant in variants)
        {
            float multiplier = Random.Range(0.6f, 1.5f); //changed the range from 0.001f/3.0f to this
            foreach (Field field in fields)
            {
                if (field.GetVariant() == variant)
                {
                    field.SetMultiplier(multiplier);
                }
            }
        }
    }

    private void Visualisierung()
    {
        //TODO: add again the corn visualization

        //Debug.Log("Corn: " + corn[0] + " | " + corn[1] + " | " + corn[2] + " | " + corn[3] + " with multipliers: " + multiplier[0] + " | " + multiplier[1]);
        //for (int i = 0; i < 4; i++)
        //{
        //    Scores[i].text = "Score " + (i + 1) + ": " + corn[i];
        //}
        //Mults[0].text = "Multiplier: " + multiplier[0];
        //Mults[1].text = "Multiplier: " + multiplier[1];

        //float scaling = 0.25f;
        //float tempCorn = 1;
        //for (int i = 0; i < 4; i++)
        //{
        //    tempCorn = corn[i];
        //    while (tempCorn > 11)
        //    {
        //        scaling += 0.5f;
        //        tempCorn /= 10;
        //    }
        //    cornPrefab.transform.localScale *= Mathf.Min(scaling, 2);
        //    for (int j = 0; j < tempCorn; j++)
        //    {
        //        Instantiate(cornPrefab, feldpos[i].position + new Vector3(Random.Range(-2f, 2f), Random.Range(-1f, 1f), 1), Quaternion.identity);
        //    }
        //    cornPrefab.transform.localScale = new Vector3(1, 1, 1);
        //    scaling = 0.25f;
        //}
    }
}