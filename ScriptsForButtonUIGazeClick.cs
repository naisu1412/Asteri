using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class ScriptsForButtonUIGazeClick : MonoBehaviour
{
    RaycastResult UIResult { get; set; }
    public object ExecutiveEvents { get; private set; }

    GameObject[] uis;
    RaycastResult noValue;       
    GameObject gazedOBJ;        //object that has been gazed at
    public GameObject canvasForCurveUI;
    Vector3 DefaultSizeOfTheCanvas;

    byte counter = 0;           //default counter checker
    bool looking;               // as it says
    float timer;                // var for the time



    byte counterCurveUI = 0;    // counter checker for curved UI
    byte counterClose = 0;    // counter checker for closing the curve UI
    TimeCountdown tc = new TimeCountdown();

    void Awake()
    {
        uis = GameObject.FindGameObjectsWithTag("UIButt");
        DefaultSizeOfTheCanvas = canvasForCurveUI.transform.localPosition;
        canvasForCurveUI.transform.localPosition += new Vector3(0, 1000, 0);
    }

    void Start()
    {

    }


    public void YouLookAtTheUI()
    {
        looking = true;
        UIResult = ValueGetter.result;
        counter = 0;
        foreach (GameObject ui in uis)
        {

            if (UIResult.gameObject)
            {
                //Debug.Log(UIResult.gameObject.name);
                if (UIResult.gameObject.name == ui.name)
                {
                    gazedOBJ = ui;
                    counter = 1;
                }
                else
                {
                    ui.transform.rotation = new Quaternion(0, 0, 0, 0);
                }
            }
        }


    }


    public void YouLookOutUI()
    {
        looking = false;
        Debug.Log("You exited");
        foreach (GameObject ui in uis)
        {
            ui.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        counter = 0;
        timer = 0;
    }

    public void YouClickedMe()
    {
        //
        if(ValueGetter.UIPlanet.Count > 1)
        {

            Debug.Log("You clicked " + ValueGetter.UIPlanet[ValueGetter.UIPlanet.Count - 1]);  // test logic
            ValueGetter.UIPlanet.Clear();
        }
        else
        {
            ValueGetter.UIPlanet.Add("You Forced Clicked it");
        }
    }
 
    public void CurveUI()
    {
        counterCurveUI = 1;
        
    }

    public void CloseUI()
    {
        counterClose = 1;
    }

    


    void Update()
    {
        
        if (counter == 1)
        {
            if (gazedOBJ.transform.rotation.x < .5)
            {
                gazedOBJ.transform.Rotate(Time.deltaTime * 500, 0, 0);      //rotating in y axis

            }
        }

        if(counterCurveUI == 1)
        {
          if(canvasForCurveUI.transform.localPosition.y > DefaultSizeOfTheCanvas.y)
            {
                canvasForCurveUI.transform.localPosition -= new Vector3(0, 5, 0) * (Time.deltaTime * 300);

            }
            else
            {
                counterCurveUI = 0;
            }


        }

        if(counterClose == 1)
        {
            //canvasForCurveUI.transform.localPosition = new Vector3(0, 1000, 0);
            if(canvasForCurveUI.transform.localPosition.y <= DefaultSizeOfTheCanvas.y + 1000)
            {
                canvasForCurveUI.transform.localPosition += new Vector3(0, 5, 0) * (Time.deltaTime * 500);

            }
            else
            {
                counterClose = 0;
            }
            
        }

  
        //click event

        if (looking)
        {
            timer += Time.deltaTime;
            if(timer >= 1.3f)
            {
                ExecuteEvents.Execute(gazedOBJ, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);          //triiger events
                timer = 0f; //reset timer
            }
        }

        //Debug.Log(UIResult.gameObject.name);
    }


}
