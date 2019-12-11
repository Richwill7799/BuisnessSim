using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInput : MonoBehaviour
{
    //public Button coopFarmPrefab;
    //public Button sinFarmPrefab;
    //public Button deleteButton;
    //public InputField inputFarmers;

    public Button buttonPrefab;
    public Slider yearSlider;
    public Canvas canvas;

    private int numFarmers = 4;
    private int years;
    private int teamCount = 2;
    private int currentVariant = 2; // or 1 if 0 is valide
    private int bauernname = 1;



    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<Button> buttons = new List<Button>();

    GameObject addButton;
    GameObject deleteButton;

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

        Button temp = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.transform.SetParent(canvas.transform, false);

        temp = Instantiate(buttonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.transform.SetParent(canvas.transform, false);


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

        deleteButton.GetComponent<Button>().interactable = true;


        if (numFarmers == 20)
        {
            addButton.GetComponent<Button>().interactable = false;
        }

    }

    public void DeleteFarmers()
    {
        if (numFarmers == 6) // farmers.Count == 6
        {
            deleteButton.GetComponent<Button>().interactable = false;
        }

        Button bufferB = buttons[buttons.Count - 1];
        buttons.Remove(bufferB);
        Destroy(bufferB.gameObject);

        bufferB = buttons[buttons.Count - 1];
        buttons.Remove(bufferB);
        Destroy(bufferB.gameObject);

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

    public int getNumFarmers()
    {
        return numFarmers;
    }

    public int getYears()
    {
        return years;
    }
}
