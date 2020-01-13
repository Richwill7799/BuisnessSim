using System.Collections;
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
    public Canvas canvas;

    //private Variables
    private int numFarmers = 4;
    private int years;
    private int teamCount = 2;
    private int currentVariant = 0;
    private int bauernname = 1;
    private int farmerCount = 4;
    private int offsetDown = 70;
    private int firstOffset = 70;

    //Lists
    private List<Field> fields = new List<Field>();
    private List<Farmer> farmers = new List<Farmer>();
    private List<Button> buttons = new List<Button>();

    private List<Farmer> collabFarmer = new List<Farmer>();

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

        //create 4 Farmers and fields (Constant)
        for (int i = 0; i < 2; i++)
        {
            currentVariant++;

            for (int j = 0; j < 2; j++)
            {
                Field field = new Field(currentVariant);
                fields.Add(field);

                Farmer farmer = new Farmer(field, 1, "Farmer" + bauernname);
                farmers.Add(farmer);
                bauernname++;

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
                    collabFarmer.Add(farmers.First(x => x.name[x.name.Length - 1].Equals(text[text.Length - 1])));
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

        Button temp = Instantiate(sinFarmPrefab, new Vector3(436f, 333f - offsetDown, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.GetComponentInChildren<Text>().text = "Farmer" + farmerCount;
        temp.transform.SetParent(canvas.transform, false);

        farmerCount++;
        temp = Instantiate(coopFarmPrefab, new Vector3(707f, 333f - offsetDown, 0), Quaternion.identity);
        buttons.Add(temp);
        temp.GetComponentInChildren<Text>().text = "Farmer" + farmerCount;
        temp.transform.SetParent(canvas.transform, false);

        offsetDown += firstOffset;

        //create 2 fields each time Add is pressed

        for (int j = 0; j < 2; j++)
        {
            Field field = new Field(currentVariant); //Must be 3 first time 
            fields.Add(field);

            //create 2 farmers each time Add is pressed
            Farmer farmer = new Farmer(field, 1, "Farmer" + bauernname);
            farmers.Add(farmer);
            bauernname++;

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
            buttons.Remove(buttons[buttons.Count - 1]);
            farmers.Remove(farmers[farmers.Count - 1]); // First remove Farmer that his field
            fields.Remove(fields[fields.Count - 1]);
        }

        Debug.Log("FamersCount:" + farmers.Count + "    fields:" + fields.Count);

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
