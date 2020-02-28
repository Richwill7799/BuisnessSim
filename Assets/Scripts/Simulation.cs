using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;
using System;
using UnityEngine.SceneManagement;
using UnityEditor;
using Random = UnityEngine.Random;

public class Simulation : MonoBehaviour
{
    //private variables
    private int year = 0;
    private int years;
    private int countFarmers;
    private List<Field> fields;
    public List<Farmer> farmers;
    private List<int> variants = new List<int>();
    private List<float> allCollabHarvest = new List<float>();
    private List<Transform> walkingFarmers = new List<Transform>();
    private int weatherCount;
    private SortedList<int, List<float>> variantenMultiplList = new SortedList<int, List<float>>();
    private string folderPath;
    private string[] filePaths;
    private Color lastColor;

    //public variables
    public Text Year;
    public Transform farmerPrefab;
    public Image graph;
    //public Sprite farmerSprite;
    public Transform teamZone;
    public GameObject circle;


    // Start is called before the first frame update
    void Start()
    {
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        years = userInput.GetComponent<HandleInput>().GetYears();
        fields = userInput.GetComponent<HandleInput>().GetFields();
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();
        countFarmers = userInput.GetComponent<HandleInput>().GetNumFarmers();
        lastColor = userInput.GetComponent<HandleInput>().GetLastColor();


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

            circle.GetComponent<SpriteRenderer>().color = lastColor; //Circle color
        }

        //Moving Bois creation
        for (int i = 0; i < farmers.Count; i++)
        {
            walkingFarmers.Insert(i, Instantiate(farmerPrefab, new Vector3(Random.Range(0,10), Random.Range(-5,5)), Quaternion.identity).transform);
            // assign id of the farmer to farmer
            walkingFarmers[i].GetComponent<FarmerMovementScript>().id = i;
            //walkingFarmers[i].GetComponent<FarmerMovementScript>().s = this;
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
    public void UpdateSimulation()
    {
        int buffer = 0;

        if (years == 0)
        {
            EndSimulation();
        }
        else if (years > 10)
        {
            buffer = 10;
            years = years - 10;
        }
        else
        {
            buffer = years;
            years = 0;
        }
        for (int i = 0; i < buffer; i++)
        {
            PassYear();
            year++;
            Year.text = "Year " + year;
        }
        ValueTransferToPython();

        ImageLoader();
    }
    void ImageLoader()
    {
        //Create an array of file paths from which to choose
        folderPath = Application.streamingAssetsPath;  //Get path of folder
        filePaths = Directory.GetFiles(folderPath, "*.png"); // Get all files of type .png in this folder

        //Converts desired path into byte array

        byte[] pngBytes = System.IO.File.ReadAllBytes(filePaths[0]);

        //Creates texture and loads byte array data to create image
        Texture2D tex = new Texture2D(2, 2);
        tex.LoadImage(pngBytes);

        //Creates a new Sprite based on the Texture2D
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));


        //Assigns the UI sprite
        graph.sprite = fromTex;
    }

    //IEnumerator LoadGraphImage(FileInfo graphImage)
    //{
    //    //1
    //    if (graphImage.Name.Contains("meta"))
    //    {
    //        yield break;
    //    }
    //    //2
    //    else
    //    {
    //        string graphWithoutExtension = Path.GetFileNameWithoutExtension(graphImage.ToString());
    //        string[] playerNameData = graphWithoutExtension.Split(" "[0]);
    //        //3
    //        string wwwPlayerFilePath = "file://" + graphImage.FullName.ToString();
    //        WWW www = new WWW(wwwPlayerFilePath);
    //        yield return www;
    //        //4
    //        graphImg = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0.5f,0.5f));
    //    }
    //}

    private void PassYear()
    {
        SetMultiplier(); //set multiplier each year
        foreach (Farmer farmer in farmers)
        {
            farmer.GetField().SimulateField();
        }

        Collaboration();

        //Debug 
        //foreach (Farmer f in farmers)
        //{
        //    UnityEngine.Debug.Log(f.name + ": " + f.GetField().GetHarvest());
        //}
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
            //UnityEngine.Debug.Log(multiplier);
            foreach (Farmer farmer in farmers)
            {
                if (farmer.GetField().GetVariant() == v)
                {
                    if (!farmer.GetField().IsWeatherChangedByUser())
                    {
                        farmer.GetField().SetMultiplier(multiplier);
                    }
                }
            }
            //Map die multiplier auf 0-1 im abstand von weatherCount
            float normalizedValue = (farmers.First(x => x.GetField().GetVariant() == v).GetField().GetMultiplier() - 0.6f) / (1.5f - 0.6f);
            //je nach bereich dann [v][0,...,1] += multiplier rechnen
            if (normalizedValue >= 0 && normalizedValue <= 0.20f) //goblinattac
            {
                variantenMultiplList[v][0]++;
            }
            else if (normalizedValue >= 0.21f && normalizedValue <= 0.40f) //Storm
            {
                variantenMultiplList[v][1]++;
            }
            else if (normalizedValue >= 0.41f && normalizedValue <= 0.60f) //Rain
            {
                variantenMultiplList[v][2]++;
            }
            else if (normalizedValue >= 0.61f && normalizedValue <= 0.80f) //cloudy
            {
                variantenMultiplList[v][3]++;
            }
            else
            {
                variantenMultiplList[v][4]++; //Sun
            }
        }
    }

    public void EndSimulation()
    {
        //extract variantenMultiplList as a txt file and get the resulting picture from python to unity
        string workingDirectory = Environment.CurrentDirectory;
        int variants = countFarmers / 2;
        string fileName = "star.x";
        using (StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "\\"+ fileName, false)) //delete existing files and safe a new one
        {
            for (int i = 1; i <= variants; i++)
            {
                writer.Write(i + ",");
            }
            writer.WriteLine("");
            for (int i = 0; i < 5; i++)
            {
                foreach (KeyValuePair<int, List<float>> kvp in variantenMultiplList)
                {

                    //insert all harvest into the file
                    writer.Write(kvp.Value[i].ToString() + ",");
                }
                writer.WriteLine("");
            }
        }
        //AssetDatabase.ImportAsset("Assets\\Resources\\" + fileName);

        Run_cmd(@"python.exe", Application.streamingAssetsPath + "\\CreateSpider.py");
        //ImageLoader(1);
        //UnityEngine.Debug.Log(variantenMultiplList);

        SceneManager.LoadScene(3);

    }

    private void ValueTransferToPython()
    {
        //delete existing txt files. 
        string workingDirectory = Environment.CurrentDirectory;
        //UnityEngine.Debug.Log(workingDirectory);
        string[] files = System.IO.Directory.GetFiles(Application.streamingAssetsPath + "\\" , "*.txt");

        foreach (string file in files)
        {
            System.IO.File.Delete(file);
        }
        foreach (Farmer f in farmers)
        {
            if (f.HasNoCollabFarmer()) //get all farmers who doesn't collab
            {
                //create new textfile with name of farmer as name
                string fileName = f.name + ".txt";
                using (StreamWriter writer = new StreamWriter(Application.streamingAssetsPath +"\\"+ fileName, false)) //delete existing files and safe a new one
                {
                    foreach (float h in f.GetField().allHarvest)
                    {
                        //insert all harvest into the file
                        writer.WriteLine(h);
                    }

                }
                //AssetDatabase.ImportAsset("Assets\\Resources\\" +  fileName);
            }
        }
        //create new textfield for the collabHarvest -> TODO edit it for multiple collabFarmers, or userinput collab farmers to change the collab (?)
        string fileNamee = "collabFarmers.txt";
        using (StreamWriter writer = new StreamWriter(Application.streamingAssetsPath + "\\"  + fileNamee, false)) //delete existing files and safe a new one
            foreach (float h in allCollabHarvest)
            {
                //insert all harvest into the file
                writer.WriteLine(h);
            }
        //AssetDatabase.ImportAsset("Assets\\Resources\\" + fileNamee);

        //do the python script call
        Run_cmd(@"python.exe", Application.streamingAssetsPath + "\\CreateGraph.py");

    }

    private void Run_cmd(string cmd, string args)
    {
        ProcessStartInfo start = new ProcessStartInfo
        {
            FileName = Path.Combine(cmd),//cmd is full path to python.exe
            Arguments = Path.Combine(args),//args is path to .py file and any cmd line args
            CreateNoWindow = true,
            UseShellExecute = false,
            RedirectStandardOutput = true
        };
        Process process = Process.Start(start);
        process.WaitForExit();

    }
}
