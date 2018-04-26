using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;
using CurvedUI;
using System;

public class CurveUIPlanetRecognition : MonoBehaviour
{

    RaycastResult UIResult { get; set; }
    public Camera m_Camera;
    public Text text;
    public Text desc;
    /* Initializing planets */
    PlanetComponent Mercury = new PlanetComponent();
    PlanetComponent Venus = new PlanetComponent();
    PlanetComponent Earth = new PlanetComponent();
    PlanetComponent Mars = new PlanetComponent();
    PlanetComponent Jupiter = new PlanetComponent();
    PlanetComponent Saturn = new PlanetComponent();
    PlanetComponent Uranus = new PlanetComponent();
    PlanetComponent Neptune = new PlanetComponent();
    List<PlanetComponent> Planets = new List<PlanetComponent>();

    /// <summary>
    /// This will be for demonstration only 
    /// all the data will be added later in the database
    /// </summary>
    /// 
    GameObject[] PlanetsUI;


    void PlanetComponentsInput()
    {


        //Mercury
        Mercury.planetName = "Mercury";
        Mercury.planetDesc.set("Mercury is the closest planet to the Sun and due to its proximity it is not easily seen except during twilight. ");
        Mercury.planetMass.set(3.285E23f);
        Mercury.planetRadius.set(2440f);
        Mercury.planetDensity.set(5.43f);
        Mercury.planetAge.set(4600000000f);
        Mercury.planetSurfaceTemperature.set(430f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Mercury.planetDistance.set(57.91f); //million km
        Mercury.planetRotationalPeriod.set(58f); //d 15h 30m
        Mercury.planetSurfaceGravity.set(3.7f); // m/s²
        Mercury.planetEscapeVelocity.set(4.25f); // km/s


        //Venus
        Venus.planetName = "Venus";
        Venus.planetDesc.set("Venus is the second planet from the Sun and is the second brightest object in the night sky after the Moon. ");
        Venus.planetMass.set(4.867E24f); //10E24
        Venus.planetRadius.set(6052f);
        Venus.planetDensity.set(5.24f);
        Venus.planetAge.set(4600000000f);
        Venus.planetSurfaceTemperature.set(462f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Venus.planetDistance.set(108.2f); //million km
        Venus.planetRotationalPeriod.set(116f); //d 15h 30m
        Venus.planetSurfaceGravity.set(8.87f); // m/s²
        Venus.planetEscapeVelocity.set(11.2f); // km/s


        //Earth
        Earth.planetName = "Earth";
        Earth.planetDesc.set("Earth is the third planet from the Sun and is the largest of the terrestrial planets.");
        Earth.planetMass.set(5.972E24f); //10E24
        Earth.planetRadius.set(6371f);
        Earth.planetDensity.set(5.51f);
        Earth.planetAge.set(4500000000f);
        Earth.planetSurfaceTemperature.set(16f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Earth.planetDistance.set(149.6f); //million km
        Earth.planetRotationalPeriod.set(23.93f); //d 15h 30m
        Earth.planetSurfaceGravity.set(9.807f); // m/s²
        Earth.planetEscapeVelocity.set(11.2f); // km/s

        //Mars
        Mars.planetName = "Mars";
        Mars.planetDesc.set("Mars is a terrestrial planet with a thin atmosphere composed primarily of carbon dioxide.");
        Mars.planetMass.set(6.39E23f); //10E23
        Mars.planetRadius.set(3990f);
        Mars.planetDensity.set(3.93f);
        Mars.planetAge.set(4500000000f);
        Mars.planetSurfaceTemperature.set(-55f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Mars.planetDistance.set(227.9f); //million km
        Mars.planetRotationalPeriod.set(0f); //1d 0h 40m
        Mars.planetSurfaceGravity.set(3.711f); // m/s2
        Mars.planetEscapeVelocity.set(5.027f); // km/s

        //Jupiter
        Jupiter.planetName = "Jupiter";
        Jupiter.planetDesc.set(" Jupiter is the fifth planet from the Sun and the largest in the Solar System. ");
        Jupiter.planetMass.set(1.898E27f); //10E27
        Jupiter.planetRadius.set(69911f);
        Jupiter.planetDensity.set(1.33f);
        Jupiter.planetAge.set(0f);
        Jupiter.planetSurfaceTemperature.set(24000f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Jupiter.planetDistance.set(778.5f); //million km
        Jupiter.planetRotationalPeriod.set(0f); //0d 9h 56m
        Jupiter.planetSurfaceGravity.set(24.79f); // m/s2
        Jupiter.planetEscapeVelocity.set(59.5f); // km/s


        //Saturn
        Saturn.planetName = "Saturn";
        Saturn.planetDesc.set("Saturn most distant that can be seen with the naked eye. ");
        Saturn.planetMass.set(5.683E26f); //10E26
        Saturn.planetRadius.set(58232f);
        Saturn.planetDensity.set(687f);
        Saturn.planetAge.set(4600000000f);
        Saturn.planetSurfaceTemperature.set(0f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Saturn.planetDistance.set(1.429f); //billion km
        Saturn.planetRotationalPeriod.set(0f); //0d 10h 42m
        Saturn.planetSurfaceGravity.set(10.44f); // m/s2
        Saturn.planetEscapeVelocity.set(35.5f); // km/s


        //Uranus
        Uranus.planetName = "Uranus";
        Uranus.planetDesc.set("Uranus became the first planet discovered with the use of a telescope. ");
        Uranus.planetMass.set(8.681E25f); //10E25
        Uranus.planetRadius.set(25362f);
        Uranus.planetDensity.set(1.27f);
        Uranus.planetAge.set(4600000000f);
        Uranus.planetSurfaceTemperature.set(-224f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Uranus.planetDistance.set(2.871f); //billion km
        Uranus.planetRotationalPeriod.set(0f); //0d 17h 14m
        Uranus.planetSurfaceGravity.set(8.69f); // m/s2
        Uranus.planetEscapeVelocity.set(21.3f); // km/s

        //Neptune
        Neptune.planetName = "Neptune";
        Neptune.planetDesc.set("Neptune, \"Ice giant\" the second largest planet and the coldest planet in our solar system.");
        Neptune.planetMass.set(1.02E26f); //10E26
        Neptune.planetRadius.set(24764f);
        Neptune.planetDensity.set(1.64f);
        Neptune.planetAge.set(4500000000f);
        Neptune.planetSurfaceTemperature.set(-214f); // degrees Celsius (daytime) / -180 degrees Celsius (nighttime)
        Neptune.planetDistance.set(4.498f); //billion km
        Neptune.planetRotationalPeriod.set(0f); //0d 16.11h 0m
        Neptune.planetSurfaceGravity.set(11.15f); // m/s2
        Neptune.planetEscapeVelocity.set(12.5f); // km/s

    }
    void PlanetsInitialize()
    {
        PlanetComponentsInput();
        /// 
        /// 
        /// Adding all the planets in the list
        /// 

        Planets.Add(Mercury);
        Planets.Add(Venus);
        Planets.Add(Earth);
        Planets.Add(Mars);
        Planets.Add(Jupiter);
        Planets.Add(Saturn);
        Planets.Add(Uranus);
        Planets.Add(Neptune);



    }


    void Awake()
    {
        PlanetsInitialize();    //initializing all the planet
        m_Camera = Camera.main; //setting the m_Camera to main camera

    }



    /* The data is added by hardcoding all the data will be added to the database
     * in the future
     */


    GameObject[] UIValues;
    List<Text> TextValues = new List<Text>();

    void Start()
    {

        UIValues = GameObject.FindGameObjectsWithTag("PlanetValues");        //
        foreach (var item in UIValues)
        {
            TextValues.Add(item.GetComponent<Text>());
        }

      

    }

    bool cameraFoVTo20;
    float timer;

    //Canvas To zoom
    public GameObject canvas;
    // zooming in and out effect
    // edited version of the fov because fov is not working in VR
    void Update()
    {

        if (cameraFoVTo20)
        {
            timer += Time.deltaTime;
            Debug.Log(canvas.transform.localPosition.z);

            if (canvas.transform.localPosition.z < -99)
            {
                canvas.transform.localPosition += new Vector3(0,0, 5 * Time.deltaTime * 2);

            }

            //if (text.transform.localScale.x > .06f && text.transform.localScale.y > .06f)
            //{
            //    text.transform.localScale -= new Vector3(.005f, .005f) * Time.deltaTime * 100;
            //}
        }
    //    Debug.Log(timer);
        if (timer > 6f)
        {
            cameraFoVTo20 = false;
            Debug.Log("Trying to work");
            if (canvas.transform.localPosition.z > -103)
            {

                Debug.Log(" Worked ~! ");

                //Debug.Log("Im true");
                //m_Camera.fieldOfView += 65 * Time.deltaTime * 2;
                canvas.transform.localPosition -= new Vector3(0, 0, 5 * Time.deltaTime * 2);

            }

            //if (text.transform.localScale.x < .1 && text.transform.localScale.y < .1)
            //{
            //    text.transform.localScale += new Vector3(.005f, .005f) * Time.deltaTime * 100;
            //}
        }



        //Debug.Log(ValueGetter.UIPlanet[ValueGetter.UIPlanet.Count - 1]);
    }



    string planetRecognized(PlanetComponent planet, string component)
    {
        switch (component)
        {
            case "Mass":
                return planet.planetMass.outputValues;
            case "Radius":
                return planet.planetRadius.outputValues;
            case "Density":
                return planet.planetDensity.outputValues;
            case "Age":
                return planet.planetAge.outputValues;
            case "SurfaceTemperature":
                return planet.planetSurfaceTemperature.outputValues;
            case "Distance":
                return planet.planetDistance.outputValues;
            case "RotationalPeriod":
                return planet.planetRotationalPeriod.outputValues;
            case "SurfaceGravity":
                return planet.planetSurfaceGravity.outputValues;
            case "EscapeVelocity":
                return planet.planetEscapeVelocity.outputValues;
            default:
                return "";
        }
    }

    float planetRecogVal(PlanetComponent planet, string component)
    {
        switch (component)
        {
            case "Mass":
                return planet.planetMass.outputValuesREAL;
            case "Radius":
                return planet.planetRadius.outputValuesREAL;
            case "Density":
                return planet.planetDensity.outputValuesREAL;
            case "Age":
                return planet.planetAge.outputValuesREAL;
            case "SurfaceTemperature":
                return planet.planetSurfaceTemperature.outputValuesREAL;
            case "Distance":
                return planet.planetDistance.outputValuesREAL;
            case "RotationalPeriod":
                return planet.planetRotationalPeriod.outputValuesREAL;
            case "SurfaceGravity":
                return planet.planetSurfaceGravity.outputValuesREAL;
            case "EscapeVelocity":
                return planet.planetEscapeVelocity.outputValuesREAL;
            default:
                return 0;
        }
    }

    void ArrangePlanet()
    {
        PlanetsUI = GameObject.FindGameObjectsWithTag("PanelPlanet");
        foreach (var planetUI in PlanetsUI)
        {
            
            string Name = planetUI.name;
            string nVal = Name.Substring(0, Name.Length - 5);
            Text[] nums = planetUI.GetComponentsInChildren<Text>();
            for (int i = 0;i < Planets.Count;i++)
            {
                if(nVal == Planets[i].planetName)
                {
                    planetUI.GetComponent<Transform>().SetSiblingIndex(i);
                  
                    nums[nums.Length - 1].text = (i+1).ToString();
                }
            }
        }
  


    }

    void exchange(List<PlanetComponent> data, int m, int n)
    {
        PlanetComponent temporary;

        temporary = data[m];
        data[m] = data[n];
        data[n] = temporary;


    }

    void BubbSort(List<PlanetComponent> planets, string component)
    {
        int i, j;
        int N = planets.Count;

        for (j = N - 1; j > 0; j--)
        {
            for (i = 0; i < j; i++)
            {

                if (planetRecogVal(planets[i], component) > planetRecogVal(planets[i + 1], component))
                    exchange(planets, i, i + 1);
            }
        }
    }



    public void ComponentsClicked()
    {
        string val = ValueGetter.UIPlanet[ValueGetter.UIPlanet.Count - 1];
        foreach (var value in DataList.DataCompListsValues)
        {
            if (val == value)
            {

                //Debug.Log(planet.planetName + " " + planetRecognized(planet, val));
                BubbSort(Planets, val); //bubble sort

                //planet sorted
                ArrangePlanet();
                Debug.Log("Succesfully sorted by " + val);

                timer = 0;
                cameraFoVTo20 = true;
            }
        }
    }

    GameObject[] go_planets;
    public void PlanetClicked()
    {
        go_planets = GameObject.FindGameObjectsWithTag("PanelPlanet");

        if (ValueGetter.UIPlanet.Count > 1) //making sure that the list is not empty
        {
            Debug.Log("You clicked " + ValueGetter.UIPlanet[ValueGetter.UIPlanet.Count - 1]);  // test logic

            Debug.Log(DataList.DataCompListsValues.Count);


            string val = ValueGetter.UIPlanet[ValueGetter.UIPlanet.Count - 1];
            val = val.Substring(0, val.Length - 5);
            foreach (var planet in Planets)
            {
                //Debug.Log(val + "space" + planet.planetName);

                if (val == planet.planetName)
                {
                    foreach (var dataCompListsValue in DataList.DataCompListsValues)
                    {
                        foreach (var TextValue in TextValues)
                        {
                            if (dataCompListsValue == TextValue.transform.parent.gameObject.name)
                            {

                                TextValue.text = planetRecognized(planet, dataCompListsValue);

                            }
                        }
                    }
                    desc.text = planet.planetDesc.outputValues;
                }
            }
            ValueGetter.UIPlanet.Clear(); //Clearing the list
        }
        else
        {
            ValueGetter.UIPlanet.Add("You Forced Clicked it");  // in case there's some weird shit 
        }


        //foreach (var item in DataList.DataCompListsValues)
        //{
        //    Debug.Log(item);
        //}
    }
}
