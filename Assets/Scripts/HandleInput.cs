﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInput : MonoBehaviour
{

    //public variables
    public Button coopFarmPrefab;
    public Button sinFarmPrefab;
    public InputField inputFarmers;

    public Slider yearSlider;
    public Canvas canvas;

    //private Variables
    private int numFarmers = 4;
    private int years;
    private int teamCount = 2;
    private int currentVariant = 2; // or 1 if 0 is valide
    private int bauernname = 1;
    private int farmerCount = 4;

    private int offsetDown = 35;
    private int firstOffset = 35;

    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<Button> buttons = new List<Button>();


    private GameObject addButton;
    private GameObject deleteButton;

    public void Start()
    {
        addButton = GameObject.FindGameObjectWithTag("Add");
        deleteButton = GameObject.FindGameObjectWithTag("Delete");

        addButton.GetComponent<Button>().interactable = true;
        deleteButton.GetComponent<Button>().interactable = false;

        //create 4 Farmers and fields

    }

    public void StartWithData()  // start simulation
    {
        // assign values from input 
        //numFarmers = int.Parse(inputFarmers.text);
        years = (int)yearSlider.value;

        // TODO load scene x

    }

    // if AddButton is pressed
    public void AddFarmers()
    {
        // TODO: Correct Position
        //farmer 3 (187.7 , 104.76 , 0)
        //farmer 4 (329.2 , 104.76 , 0)

        farmerCount++;
        Button temp = Instantiate(sinFarmPrefab, new Vector3(187.7f, 104.76f - offsetDown, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.GetComponentInChildren<Text>().text = "Farmer"+farmerCount;
        temp.transform.SetParent(canvas.transform, false);

        farmerCount++;
        temp = Instantiate(coopFarmPrefab, new Vector3(329.2f, 107.76f - offsetDown, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.GetComponentInChildren<Text>().text = "Farmer" + farmerCount;
        temp.transform.SetParent(canvas.transform, false);

        offsetDown += firstOffset;

        //create 2 fields each time Add is pressed
        for (int j = 0; j < 2; j++)
        {
            Field field = new Field(currentVariant);
            fields.Add(field);
            //variants.Add(variant);

        }

        currentVariant++;

        //create 2 farmers each time Add is pressed
        foreach (Field field in fields)
        {
            Farmer farmer = new Farmer(field, 1, "bauer" + bauernname);
            farmers.Add(farmer);
            bauernname++;
        }

        numFarmers = numFarmers + 2;
        currentVariant++;

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



        // buttons.Count or Count - 1?
        Destroy(buttons[buttons.Count]);
        buttons.Remove(buttons[buttons.Count]);

        /*Button bufferB = buttons[buttons.Count - 1];
        buttons.Remove(bufferB);
        Destroy(bufferB.gameObject);


        bufferB = buttons[buttons.Count - 1];
        buttons.Remove(bufferB);
        Destroy(bufferB.gameObject);*/

        //TODO change offset
        offsetDown -= firstOffset;


        numFarmers = numFarmers - 2;
        currentVariant--;

        addButton.GetComponent<Button>().interactable = true;
        farmerCount = farmerCount - 2;
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
}
