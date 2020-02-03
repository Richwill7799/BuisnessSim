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
    private List<Color> name = new List<Color>();

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
        //We could get them from Python?
        //Colors are from MatPlotLib, how do I access this?

        //Using tab20
        color.Add(new Color(0.122f, 0.467f, 0.706f)); //1 dblau
        color.Add(new Color(0.682f, 0.780f, 0.910f)); //2 hblau
        color.Add(new Color(1.000f, 0.498f, 0.055f)); //3 dorange
        color.Add(new Color(1.000f, 0.733f, 0.471f)); //4 horange
        color.Add(new Color(0.173f, 0.627f, 0.173f)); //5 dgrün
        color.Add(new Color(0.596f, 0.875f, 0.541f)); //6 hgrün
        color.Add(new Color(0.839f, 0.153f, 0.157f)); //7 drot
        color.Add(new Color(1.000f, 0.596f, 0.588f)); //8 hrot
        color.Add(new Color(0.580f, 0.404f, 0.741f)); //9 dlila
        color.Add(new Color(0.773f, 0.690f, 0.835f)); //10 hlila
        color.Add(new Color(0.549f, 0.337f, 0.294f)); //11 dbraun
        color.Add(new Color(0.769f, 0.612f, 0.580f)); //12 hbraun
        color.Add(new Color(0.890f, 0.467f, 0.761f)); //13 drosa
        color.Add(new Color(0.969f, 0.714f, 0.824f)); //14 hrosa
        color.Add(new Color(0.498f, 0.498f, 0.498f)); //15 dgrau
        color.Add(new Color(0.780f, 0.780f, 0.780f)); //16 hgrau
        color.Add(new Color(0.737f, 0.741f, 0.133f)); //17 dlime
        color.Add(new Color(0.859f, 0.859f, 0.553f)); //18 hlime
        color.Add(new Color(0.090f, 0.745f, 0.812f)); //19 dcyan
        color.Add(new Color(0.620f, 0.855f, 0.898f)); //20 hcyan

        //create 4 Farmers and fields (Constant)
        for (int i = 0; i < 2; i++)
        {
            currentVariant++; //Arrays start at 1 ;P

            for (int j = 0; j < 2; j++)
            {
                Field field = new Field(currentVariant);
                fields.Add(field);

                bauernname++;
                Farmer farmer = new Farmer(field, 1, "Farmer " + bauernname, color[currentVariant - 1]);
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
        ColorNames();

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

    private void ColorNames()
    {
        int n = 0;
        for(int i = 0; i < farmers.Count; i++)
        {
            if (farmers[i].HasNoCollabFarmer())
            {
                farmers[i].SetNameColor(color[n]);
                n++;
            }
            else
            {
                //Should be the latest color in the list
                farmers[i].SetNameColor(color[farmers.Count - collabFarmer.Count]);
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
            Farmer farmer = new Farmer(field, 1, "Farmer " + bauernname, color[currentVariant - 1]);

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
    public Color GetLastColor() { return color[farmers.Count - collabFarmer.Count]; }

}
