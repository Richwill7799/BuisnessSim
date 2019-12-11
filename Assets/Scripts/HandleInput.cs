using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandleInput : MonoBehaviour
{
    
    public InputField inputFarmers;
    public Slider yearSlider;

    private int numFarmers = 4;
    private int years;
    private int currentVariant = 2; // or 1 if 0 is valide
    private int bauernname = 1;

    public Button coopFarmPrefab;
    public Button sinFarmPrefab;

    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<Button> buttons = new List<Button>();

    public void Start()
    {
    
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

        // activate dlete Button
        
    }

    public void DeleteFarmers() 
    {
        if (numFarmers == 6) // farmers.Count == 6
        {
            //TODO disable delete Button
        }

        Destroy(buttons[buttons.Count]);
        buttons.Remove(buttons[buttons.Count]);

        Destroy(buttons[buttons.Count]);
        buttons.Remove(buttons[buttons.Count]);

        //TODO change offset

       

        numFarmers = numFarmers - 2;

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
