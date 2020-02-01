﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class HandleInput : MonoBehaviour
{

    //public variables
    public Button coopFarmPrefab;
    public Button sinFarmPrefab;

    public Slider yearSlider;
    public GameObject canvas;

    //private Variables
    private int numFarmers = 4;
    private int years;
    private int teamCount = 2;
    private int currentVariant = 0;
    private int bauernname = 0;
    private int farmerCount = 4;
    private int offsetDown = 70;
    private int firstOffset = 70;

    //Lists
    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<Button> buttons = new List<Button>();

    private List<Farmer> collabFarmer = new List<Farmer>();
    private List<Color> color = new List<Color>(); //Hard coded colors

    //GameObjects
    private GameObject addButton;
    private GameObject deleteButton;

    public void Start()
    {
        buttons.Add(GameObject.Find("FirstFB").GetComponent<Button>());
        buttons.Add(GameObject.Find("SecondFB").GetComponent<Button>());
        addButton = GameObject.FindGameObjectWithTag("Add");
        deleteButton = GameObject.FindGameObjectWithTag("Delete");

        addButton.GetComponent<Button>().interactable = true;
        deleteButton.GetComponent<Button>().interactable = false;

        //Setting hard coded colors
        //We need to get them from Python - in the graph farmer11 follows directly under farmer1 -> shift in colors
        //Colors are from MatPlotLib, how do I access this?
        color.Add(new Color(0.116f, 0.624f, 0.467f)); //1 mintgrün - collab
        color.Add(new Color(0.851f, 0.373f, 0.008f)); //2 ziegelorange - 1&2
        color.Add(new Color(0.459f, 0.439f, 0.702f)); //3 lavender - 3&4
        color.Add(new Color(0.906f, 0.161f, 0.541f)); //4 magenta - 5&6
        color.Add(new Color(0.400f, 0.651f, 0.118f)); //5 grün - 7&8
        color.Add(new Color(0.902f, 0.671f, 0.008f)); //6 ocker - 9&10
        color.Add(new Color(0.651f, 0.463f, 0.114f)); //7 braun - 11&12
        color.Add(new Color(0.400f, 0.400f, 0.400f)); //8 grau - 13&14
        //Additional ones for full functionality
        color.Add(new Color(0.553f, 0.627f, 0.796f)); //9 blau - 15&16
        color.Add(new Color(0.906f, 0.541f, 0.765f)); //10 rosa - 17&18
        color.Add(new Color(0.651f, 0.847f, 0.329f)); //11 limette - 19&20

        //create 4 Farmers and fields (Constant)
        for (int i = 0; i < 2; i++)
        {
            currentVariant++;

            for (int j = 0; j < 2; j++)
            {
                Field field = new Field(currentVariant);
                fields.Add(field);

                bauernname++;
                Farmer farmer = new Farmer(field, 1, "Farmer " + bauernname, color[currentVariant]);
                farmers.Add(farmer);

            }

        }

        Debug.Log("FamersCount:" + farmers.Count + "    fields:" + fields.Count);

    }

    public void StartWithData()  // start simulation
    {
        // assign values from input 

        years = (int)yearSlider.value;
        JoinFarmers();

        SceneManager.LoadScene(1);
    }
    private void JoinFarmers()
    {
        foreach (Button button in buttons)
        {
            if (button.tag == "CoopButton")
            {
                if (button.GetComponent<StartGroupFarmers>().isInTeam)
                {
                    string text = button.GetComponentInChildren<Text>().text;
                    //get the farmer, which name's ending is the same as the button [so farmer5(button) == farmer5 (name)]
                    //I check the number in the End, to avoid Rechschreibfehler oder abweichende namen
                    collabFarmer.Add(farmers.First(x => x.name.Equals(text)));
                }
            }
        }
        foreach (Farmer farmer in collabFarmer)
        {
            foreach (Farmer collabFarmer in collabFarmer)
            {
                if (!farmer.name.Equals(collabFarmer.name))
                {
                    farmer.SetCollabFarmer(collabFarmer);
                }
            }
        }
    }
    // if AddButton is pressed
    public void AddFarmers()
    {
        // TODO: Correct Position
        //farmer 3 (436 , 333 , 0)
        //farmer 4 (707 , 333 , 0)

        currentVariant++;
        farmerCount++;

        //This is just for the buttons in the start screen
        Button temp = Instantiate(sinFarmPrefab, new Vector3(436f, 333f - offsetDown, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.GetComponentInChildren<Text>().text = "Farmer " + farmerCount;
        temp.transform.SetParent(canvas.transform, false);

        farmerCount++;
        temp = Instantiate(coopFarmPrefab, new Vector3(707f, 333f - offsetDown, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.GetComponentInChildren<Text>().text = "Farmer " + farmerCount;
        temp.transform.SetParent(canvas.transform, false);

        offsetDown += firstOffset;

        //In this loop we create the farmers, so colours should be set here
        //create 2 fields each time Add is pressed
        for (int j = 0; j < 2; j++)
        {
            Field field = new Field(currentVariant); //Must be 3 first time 
            fields.Add(field);

            //create 2 farmers each time Add is pressed
            bauernname++;
            Farmer farmer = new Farmer(field, 1, "Farmer " + bauernname, color[currentVariant]);

            farmers.Add(farmer);

        }

        Debug.Log("FamersCount:" + farmers.Count + "    fields:" + fields.Count);

        numFarmers = numFarmers + 2;

        deleteButton.GetComponent<Button>().interactable = true;


        if (numFarmers == 20)
        {
            addButton.GetComponent<Button>().interactable = false;
        }

    }

    public void DeleteFarmers()
    {
        if (numFarmers == 6) // farmers.Count == 6 ,; if there are only 4 farmers the delete button should be disabled
        {
            deleteButton.GetComponent<Button>().interactable = false;
        }

        for (int i = 0; i < 2; i++)
        {
            //Remove button in scene & List
            Destroy(buttons[buttons.Count - 1].gameObject);
            buttons.RemoveAt(buttons.Count - 1);
            farmers.RemoveAt(farmers.Count - 1); // First remove Farmer that his field
            fields.RemoveAt(fields.Count - 1);
            bauernname--;
        }

        Debug.Log("FamersCount:" + farmers.Count + "    fields:" + fields.Count);
        /*for (int i = 0; i < farmers.Count; i++)
        {
            Debug.Log(farmers[i].name);
        }*/

        //adjust vaiables
        offsetDown -= firstOffset;
        numFarmers = numFarmers - 2;
        currentVariant--;
        farmerCount = farmerCount - 2;

        // Debug.Log("DELETEvar" + currentVariant);

        addButton.GetComponent<Button>().interactable = true;

    }


    public int ChangeTeam()
    {
        // onClick change teamnum +1
        return 0;
    }

    public void randomTeams()
    {
        // random valid team Values
    }

    public int getNumFarmers()
    {
        return numFarmers;
    }

    public int getYears()
    {
        return years;

    }

    public List<Field> GetFields() { return fields; }
    public List<Farmer> GetFarmers() { return farmers; }
    public List<Button> GetButtons() { return buttons; }

}
