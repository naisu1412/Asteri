using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class loadingScript : MonoBehaviour {
    public Text text;
    DataService ds;
    IEnumerable<didYouKnow> dyks;
    System.Random r;
    public  GameObject gameCanvas;

    void Awake()
    {
        ds = new DataService("Asterii.db"); 
     
      r = new System.Random();


        dyks = ds.GetTrivias(r.Next(1, 8));
        foreach (didYouKnow item in dyks)
        {
            text.text = item.description;
            Debug.Log("This is your trivia " + item.description);
        }
    }

    void OnEnable()
    {
        if (gameCanvas)
        {
            if (gameCanvas.activeInHierarchy)
            {

            }
            else
            {
                dyks = ds.GetTrivias(r.Next(1, 8));
                foreach (didYouKnow item in dyks)
                {
                    text.text = item.description;
                    Debug.Log("This is your trivia " + item.description);
                }
            }
        }
       
    }
}
