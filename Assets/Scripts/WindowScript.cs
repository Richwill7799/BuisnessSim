using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WindowScript : MonoBehaviour
{
    public FarmerMovementScript mv;

    public Image field;
    public Image weather;

    private List<Farmer> farmers;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        GameObject userInput = GameObject.FindGameObjectWithTag("Information");
        farmers = userInput.GetComponent<HandleInput>().GetFarmers();
    }



    private void Update()
    {
        if (this.isActiveAndEnabled)
        {
            int id = mv.id;
            Farmer farmer = farmers[id];
            LoadImageOfField(farmer.GetField().GetMultiplier(), farmer);
        }
    }
    public void EditWeather(int type)
    {
        //  int i = this.gameObject.GetComponent<FarmerMovementScript>().id;
        int id = mv.id;
        Farmer farmer = farmers[id];
        Field f = farmer.GetField();
        UnityEngine.Debug.Log("bauer mit id " + id + "geklickt ");
        switch (type)
        {
            case 1:
                f.SetMultiplier(0.6f);
                f.SetWeatherChangeByUser(true);
                break;
            case 2:
                f.SetMultiplier(0.78f);
                f.SetWeatherChangeByUser(true);
                //storm
                break;
            case 3:
                f.SetMultiplier(0.96f);
                f.SetWeatherChangeByUser(true);
                //rain
                break;
            case 4:
                f.SetMultiplier(1.14f);
                f.SetWeatherChangeByUser(true);
                //cloud
                break;
            case 5:
                f.SetMultiplier(1.32f);
                f.SetWeatherChangeByUser(true);
                //sun
                break;
            case 6:
                f.SetWeatherChangeByUser(false);
                break;
            default:
                break;
        }
    }

    public void RemoveWindow()
    {
        this.gameObject.SetActive(false);
    }

    private void LoadImageOfField(float weatherCondition, Farmer farmer)
    {

        if (weatherCondition >= 0.6 && weatherCondition < 0.78)
        {
            //deer
            field.sprite = Resources.Load<Sprite>("field_deer_clean");
            weather.sprite = Resources.Load<Sprite>("skybox_sundown");
        }
        else if (weatherCondition >= 0.78 && weatherCondition < 0.96)
        {
            field.sprite = Resources.Load<Sprite>("field_sowed_clean");
            weather.sprite = Resources.Load<Sprite>("skybox_storm");
            //storm
        }
        else if (weatherCondition >= 0.96 && weatherCondition < 1.14)
        {
            field.sprite = Resources.Load<Sprite>("field_sowed_clean");
            weather.sprite = Resources.Load<Sprite>("skybox_clouds");
            //rain
        }
        else if (weatherCondition >= 1.4 && weatherCondition < 1.32)
        {
            field.sprite = Resources.Load<Sprite>("field_full_clean");
            weather.sprite = Resources.Load<Sprite>("skybox_clouds");
            //cloud
        }
        else
        {
            field.sprite = Resources.Load<Sprite>("field_full_clean");
            weather.sprite = Resources.Load<Sprite>("skybox_sun");
            //sun
        }

    }

}
