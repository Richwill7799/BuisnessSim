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
    private List<Field> fields;
    private List<Farmer> farmers;
    private List<int> variants = new List<int>();
    private List<float> allCollabHarvest = new List<float>();
    private List<Transform> walkingFarmers = new List<Transform>();

    private int weatherCount;
    private SortedList<int, List<float>> variantenMultiplList = new SortedList<int, List<float>>();

    //public variables
    public Text Year;
    public Transform farmerPrefab;
    public Transform teamZone;


    // Start is called before the first frame update
    void Start()
    {
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        year = userInput.GetComponent<HandleInput>().getYears();
        fields = userInput.GetComponent<HandleInput>().GetFields();
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();


        countFarmers = userInput.GetComponent<HandleInput>().getNumFarmers();


        weatherCount = 5; //todo: This has to be specified in the next meeting :)
        //instatiate list for weather graph
        int variant = countFarmers / 2;
        for (int i = 1; i <= variant; i++)
        {
            List<float> weather = new List<float>();
            for (int j = 0; j < weatherCount; j++)
            {
                weather.Add(0);
            }
            variantenMultiplList.Add(i, weather);
            //UnityEngine.Debug.Log(i);
        }

        //Moving Bois creation
        for (int i = 0; i < farmers.Count; i++)
        {
            walkingFarmers.Insert(i, Instantiate(farmerPrefab, new Vector3(0, 0), Quaternion.identity).transform);
            if (!farmers[i].HasNoCollabFarmer())
            {
                walkingFarmers[i].tag = "1";
                walkingFarmers[i].GetComponent<FarmerMovementScript>().team = 1;
                walkingFarmers[i].GetComponent<FarmerMovementScript>().teamTransform = teamZone;
                walkingFarmers[i].GetComponent<FarmerMovementScript>().SetTowardsTeam(true);
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
        foreach (Farmer farmer in farmers)
        {
            farmer.GetField().SimulateField();
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
        int allFarmers = fa.GetCollabFarmer().Count + 1; //+1 bc we have the first bauer
        float collabHarvest = 0;
        foreach (Farmer collabF in fa.GetCollabFarmer())
        {
            collabHarvest += collabF.GetField().GetHarvest();
        }
        collabHarvest = (harvestF + collabHarvest) / allFarmers;

        fa.GetField().SetHarvest(collabHarvest); // set the harvest now to the half of the collab harvest
        foreach (Farmer collabF in fa.GetCollabFarmer())// set the harvest now to the half of the collab harvest
        {
            collabF.GetField().SetHarvest(collabHarvest);
        }
        // set the harvest now to tzhe half of the collab harvest
        allCollabHarvest.Add(collabHarvest);
    }

    private void SetMultiplier() //set the multiplier for each variant of field, bc each field with equal variant has the same multiplier
    {
        for (int v = 1; v <= farmers.Count() / 2; v++)
        {
            float multiplier = UnityEngine.Random.Range(0.6f, 1.5f); //changed the range 
            UnityEngine.Debug.Log(multiplier);
            foreach (Farmer farmer in farmers)
            {
                if (farmer.GetField().GetVariant() == v)
                {
                    farmer.GetField().SetMultiplier(multiplier);
                }
            }
            //Map die multiplier auf 0-1 im abstand von weatherCount
            float normalizedValue = multiplier / 1.5f;
            //je nach bereich dann [v][0,...,1] += multiplier rechnen
            if (normalizedValue >= 0 && normalizedValue <= 0.20f)
            {
                variantenMultiplList[v][0]++;
            }
            else if (normalizedValue >= 0.21f && normalizedValue <= 0.40f)
            {
                variantenMultiplList[v][1]++;
            }
            else if (normalizedValue >= 0.41f && normalizedValue <= 0.60f)
            {
                variantenMultiplList[v][2]++;
            }
            else if (normalizedValue >= 0.61f && normalizedValue <= 0.80f)
            {
                variantenMultiplList[v][3]++;
            }
            else
            {
                variantenMultiplList[v][4]++;
            }
        }
    }

    public void EndSimulation()
    {
        //extract variantenMultiplList as a txt file and get the resulting picture from python to unity

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