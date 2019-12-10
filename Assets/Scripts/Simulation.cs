using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;

public class Simulation : MonoBehaviour
{
    //private variables
    private int year;
    private int countFarmers;
    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<int> variants = new List<int>();
    private List<float> allCollabHarvest = new List<float>();

    //public variables
    public Text Year;


    // Start is called before the first frame update
    void Start()
    {
        
        GameObject gameObject = GameObject.Find("SceneInfoObj");

        //store user Input in year & countFarmers
        year = gameObject.GetComponent<HandleInput>().getYears();
        countFarmers = gameObject.GetComponent<HandleInput>().getNumFarmers() ; //TODO:  MIN: 4, MAX: ?   

        if (countFarmers < 4)
        {
            countFarmers = 4;      // allways min. 4 farmers
        }

        InstantiateLists();
    }

    private void InstantiateLists()
    {
        //first fields, second farmers with fields

        //instantiate a field and it's variant
        int variant = 0;


        for (int i = 0; i < countFarmers / 2; i++) // only 2*n farmers
        {//each field
           
            for (int j = 0; j < 2; j++)
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
            for (int i = 0; i < 10; i++)
            {
                PassYear();
                year++;
                Year.text = "Year " + year;
            }
            ValueTransferToPython();
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
            UnityEngine.Debug.Log(f.name + ": " + f.GetField().GetHarvest());
        }


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
        allCollabHarvest.Add(collabHarvest);
    }

    private void SetMultiplier() //set the multiplier for each variant of field, bc each field with equal variant has the same multiplier
    {
        foreach (int variant in variants)
        {
            float multiplier = UnityEngine.Random.Range(0.6f, 1.5f); //changed the range from 0.001f/3.0f to this
            foreach (Field field in fields)
            {
                if (field.GetVariant() == variant)
                {
                    field.SetMultiplier(multiplier);
                }
            }
        }
    }

    private void ValueTransferToPython()
    {
        foreach (Farmer f in farmers)
        {
            if (f.HasNoCollabFarmer()) //get all farmers who doesn't collab
            {
                //create new textfile with name of farmer as name
                string fileName = f.name + ".txt";
                using (StreamWriter writer = new StreamWriter(fileName, false)) //delete existing files and safe a new one
                {
                    foreach (float h in f.GetField().allHarvest)
                    {
                        //insert all harvest into the file
                        writer.WriteLine(h);
                    }
                }
            }
        }
        //create new textfield for the collabHarvest -> TODO edit it for multiple collabFarmers, or userinput collab farmers to change the collab (?)
        string fileNamee = "collabFarmers.txt";
        using (StreamWriter writer = new StreamWriter(fileNamee, false)) //delete existing files and safe a new one
            foreach (float h in allCollabHarvest)
            {
                //insert all harvest into the file
                writer.WriteLine(h);
            }

        //do the python script call
        Run_cmd(@"python.exe", "CreateGraph.py");
        //VisualizeGraph();
    }

    private void VisualizeGraph()
    {
        //get the graph picture and update it in game (=
        //this not necessary at moment, but improvement is wanted due to it's loading so long
    }

    private void Run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = Path.Combine(cmd),//cmd is full path to python.exe
            Arguments = Path.Combine(args),//args is path to .py file and any cmd line args
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        Process process = Process.Start(start);
    }
}
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