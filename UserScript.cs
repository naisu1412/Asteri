using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UserScript : Confirmation
{
    public GameObject stellarObject;
    public GameObject SlotsParent;
    public GameObject SlotGO;
    public GameObject planetSavingSlots;
    GameObject[] objects;
    public GameObject[] planetsData ;

    public GameObject[] userCamera;
    List<Button> slotButtons;

    public Slider sliderOBJ;
    DataService ds; //holds the database
    IEnumerable<SolarSystemUser> planet;


    Confirmation confirmUser;   //para maglagay ng confirmation 
    void Start()
    {
        // saveData();

        // sample();
        slotButtons = new List<Button>();
        ds = new DataService("User.db");

        planet = ds.getSolarSystem();

        // ToConsole(planet);

       

    }

  
    bool SaveOrLoad;
    public void planetChoose(bool _saveOrLoad)
    {
        SaveOrLoad = _saveOrLoad;
        loadSlots(planet);
        planetSavingSlots.SetActive(true);




    }
    bool showUI = true;
    public void hideUI(GameObject UiObj)
    {
        UiObj.SetActive(!showUI);
        showUI = !showUI;
    }


    private void GetPlanets(int _solarSystemID)
    {
        EditPlanet.numCount = 0;
        foreach (var item in ds.GetPlanetRB(_solarSystemID))
        {

            //finding the tag

            foreach (var plData in planetsData)
            {
                if(item.Name == plData.tag)
                {
                    //instatiating planets

                    GameObject go = Instantiate(plData);
                    //para may bilang yung mga planet
                    string name = go.name;
                    go.name = name + EditPlanet.numCount.ToString();
                    EditPlanet.numCount++;

                    go.GetComponent<Transform>().SetParent(stellarObject.GetComponent<Transform>());
                    go.transform.localPosition = TextToVector3(item.Position);
                }
            }
        }
    }

    private void ToConsole(IEnumerable<PlanetUser> planets)
    {
        foreach (var planet in planets)
        {
            ToConsole(planet.ToString());
        }
    }

    private void ToConsole(string msg)
    {

        Debug.Log(msg);
    }



    void SaveData(int _solarSystemID)
    {
        
        ds.DeleteData(_solarSystemID);  // need to delete all planets
        // Addding data to the DB
        for (int i = 0; i < stellarObject.transform.childCount; i++)
        {
            Transform obj = stellarObject.transform.GetChild(i);
            ds.SaveData(obj.tag,
            Vector3ToText(obj.localPosition),
            Vector3ToText(obj.localScale),
            _solarSystemID,
            obj.GetComponent<Rigidbody>().mass
            );
        }
        ds.UpdateSaveData(_solarSystemID);   
        planetSavingSlots.SetActive(false);
        Debug.Log("Save Done");

    }


    void LoadData(int _solarSystemID)
    {
        //Deleting the objects here
        for (int i = 0; i < stellarObject.transform.childCount; i++)
        {
            Destroy(stellarObject.transform.GetChild(i).gameObject);

           
        }
        //Instantiate the other objects here
        GetPlanets(_solarSystemID);
        planetSavingSlots.SetActive(false);
        Debug.Log("Load Done");

    }

    int resetVal = 0;

    public Text nameUI; // name ng object
    public Text massUi; //yung text na mass
    public Text positionUI;
   
    void Update()
    {
        var objParent = stellarObject.GetComponent<Transform>();

     
        var obj = GameObject.Find(EditPlanet.lastTouchPlanet);
        if (obj)
        {
            nameUI.text = "Name: " + obj.GetComponent<Rigidbody>().name;
            massUi.text = "Mass: " + obj.GetComponent<Rigidbody>().mass.ToString();
            positionUI.text = "Position: " + obj.GetComponent<Rigidbody>().transform.localPosition.ToString();
        }
    }



    public void Reset()
    {

        confirm("Are you sure you want to reset?", FinalReset,true);
        
    }

    public void FinalReset()
    {
        Debug.Log("Resetting");
        if (resetVal != 0)
        {
            LoadData(resetVal);

        }
    }

    // loading slots data 

    public void someMethod(string name) // yung name dito ay yung name ng button
    {

        //Debug.Log(name.Remove(0,4));
        int numVal = 0;
        Int32.TryParse(name.Remove(0, 4), out numVal);
        if (SaveOrLoad)
        {
           
            SaveData(numVal); //saving the data
        }
        else
        {
            LoadData(numVal);
            resetVal = numVal;
        }

    }

    public void closeScreen()
    {
        planetSavingSlots.SetActive(false);
    }
    

    void loadSlots(IEnumerable<SolarSystemUser> solarSystems)
    {


        for (int i = 0; i < SlotsParent.GetComponent<Transform>().childCount; i++)
        {
            Destroy(SlotsParent.transform.GetChild(i).gameObject);

        }

        foreach (var solarSystem in solarSystems)
        {
            //clearing slots parent datas

            GameObject goButton = Instantiate(SlotGO, SlotsParent.GetComponent<Transform>());      //instatiate ng mga slot buttons
            goButton.transform.GetChild(1).GetComponent<Text>().text = solarSystem.Name;        // change ng mga solar system name
            goButton.GetComponent<Button>().onClick.AddListener(() => {
                confirm("Are you sure?",
                   () => someMethod(goButton.transform.GetChild(1).GetComponent<Text>().text),
                     false
                ); }  );    // pag aadd ng onclick listener sa bawat button

            if (solarSystem.State == "0")
            {
                Image image = goButton.transform.GetChild(0).GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = .3f;
                image.color = tempColor;

            }
            else
            {
                Image image = goButton.transform.GetChild(0).GetComponent<Image>();
                var tempColor = image.color;
                tempColor.a = 1f;
                image.color = tempColor;
            }
            slotButtons.Add(goButton.GetComponent<Button>());      
        }
    }

    

    public void editPlanetChange()
    {
        EditPlanet.editPlanet = !EditPlanet.editPlanet;


    }


    void loadSlots(string msg)
    {
        Debug.Log(msg);
    }


    public void changeCam()
    {
        if (EditPlanet.camPlanet)
        {
            userCamera[0].SetActive(true);
            userCamera[1].SetActive(false);
            EditPlanet.camPlanet = !EditPlanet.camPlanet;
        }
        else
        {
            userCamera[1].SetActive(true);
            userCamera[0].SetActive(false);
            EditPlanet.camPlanet = !EditPlanet.camPlanet;

        }
    }



    string Vector3ToText(Vector3 vector3Val)
    {

        string text = vector3Val.x.ToString() + "|" + vector3Val.y.ToString() + "|" + vector3Val.z.ToString();
        return text;
    }


    Vector3 TextToVector3(string stringVal)
    {
        Vector3 dataV3 = Vector3.zero;
        string[] SeparatedStrings = stringVal.Split('|');
        dataV3.x = float.Parse(SeparatedStrings[0]);
        dataV3.y = float.Parse(SeparatedStrings[1]);
        dataV3.z = float.Parse(SeparatedStrings[2]);
        return dataV3;
    }
    

    //change mass of the object when invoke

    public void changeMass()
    {
        var objParent = stellarObject.GetComponent<Transform>();
        for (int i = 0; i < objParent.childCount; i++)
        {
                Debug.Log("Buttopn " + EditPlanet.lastTouchPlanet);

            if (objParent.GetChild(i).name == EditPlanet.lastTouchPlanet)
            {

                Debug.Log("a hit " + objParent.GetChild(i).name);
                objParent.GetChild(i).GetComponent<Rigidbody>().mass = 10 * (sliderOBJ.value * 1000);
            }
        }
    }
}
