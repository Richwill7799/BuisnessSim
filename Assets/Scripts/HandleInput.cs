using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInput : MonoBehaviour
{
    public Button coopFarmPrefab;
    public Button sinFarmPrefab;
    //public Button deleteButton;
    public InputField inputFarmers;
    public Slider yearSlider;

    private int numFarmers = 4;
    private int years;
    private int teamCount = 2;
    private int currentVariant = 2; // or 1 if 0 is valide
    private int bauernname = 1;

    

    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<Button> buttons = new List<Button>();

    GameObject addButton = GameObject.Find("Add");
    GameObject deleteButton = GameObject.Find("Delete");

    public void Start()
    {

        addButton.GetComponent<Button>().interactable = true;
        deleteButton.GetComponent<Button>().interactable = false;
        
        //create 4 Farmers and fields
        
    }

    public void StartWithData()  // start simulation
    {
        // assign values from input 
        numFarmers = int.Parse(inputFarmers.text);
        years = (int)yearSlider.value;

        // TODO load scene x

    }

    // if AddButton is pressed
    public void AddFarmers()
    {
        // TODO: Correct Position
        
        Button temp = Instantiate(sinFarmPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buttons.Add(temp);
        
        temp = Instantiate(coopFarmPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buttons.Add(temp);


        //create 2 fields
        for (int j = 0; j < 2; j++)
        {
            Field field = new Field(currentVariant);
            fields.Add(field);
            //variants.Add(variant);

        }

        currentVariant++;

        //create 2 farmrers
        foreach (Field field in fields)
        {
            Farmer farmer = new Farmer(field, 1, "bauer" + bauernname);
            farmers.Add(farmer);
            bauernname++;
        }

        numFarmers = numFarmers + 2;

        deleteButton.GetComponent<Button>().interactable = true;


        if (numFarmers == 20) 
        {
            //TODO disable add button
            addButton.GetComponent<Button>().interactable = false;
        }
        
    }

    public void DeleteFarmers() 
    {
        if (numFarmers == 6) // farmers.Count == 6
        {
            //TODO disable delete Button
            deleteButton.GetComponent<Button>().interactable = false;
        }

        Destroy(buttons[buttons.Count]);
        buttons.Remove(buttons[buttons.Count]);

        Destroy(buttons[buttons.Count]);
        buttons.Remove(buttons[buttons.Count]);

        //TODO change offset

       

        numFarmers = numFarmers - 2;

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

    public int getNumFarmers() {
        return numFarmers;
    }

    public int getYears() 
    {
        return years;
    }
}
