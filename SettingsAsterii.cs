using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsAsterii : Confirmation
{
    public Slider SpeedSC;
    public Slider PitchSC;



    DataService ds;
    DataService ds_TTS;
    IEnumerable<Settings> settingsUserToUse;
    
    void Awake()
    {
         ds = new DataService("User.db");   // DataService for the reset
         ds_TTS = new DataService("Asterii.db");


        //isusure na pagbukas ng app iseset sya kung ano yung value sa DB
        updatValueUI();

        buttonUK.interactable = false;
        buttonUS.interactable = true;
    }

    

    public void updatValueUI()
    {
        settingsUserToUse = ds_TTS.getValueSetting("Speed");
        foreach (var setting in settingsUserToUse)
        {
            SpeedSC.value = float.Parse(setting.settingValue);
        }


        settingsUserToUse = ds_TTS.getValueSetting("Pitch");
        foreach (var setting in settingsUserToUse)
        {
            PitchSC.value = float.Parse(setting.settingValue);
        }
        settingsUserToUse = ds_TTS.getValueSetting("Locale");

        foreach (var setting in settingsUserToUse)
        {
            buttonSwitching(setting.settingValue);
        }

    }

    public void Exit()
    {
        bool answer = confirm("Are you sure you want to Exit?", iWillExitNow, false);
    }

    public void iWillExitNow()
    {
        Application.Quit();
        Debug.Log("Application will now Quit");
    }

    public void Reset()
    {
        bool answer = confirm("Are you sure you want to clear all the data?", ds.Reset,true);
        
    }

    public void alertSomething()
    {

    }

    public void SetSettings()
    {
        confirm("Are you sure?", _SetSettings, false);        
    }

    void _SetSettings()
    {
        ds_TTS.setSettings("Speed", SpeedSC.value.ToString());  //applying the speed setting
        ds_TTS.setSettings("Pitch", PitchSC.value.ToString());  //applying the pitch setting
        if(buttonUK.interactable == false)
        {
            ds_TTS.setSettings("Locale", "'UK'");
        }
        else
        {
            ds_TTS.setSettings("Locale", "'US'");

        }
    }
    public Button buttonUK;
    public Button buttonUS;
    public void buttonSwitching(string _name)
    {
        if(_name == "UK")
        {
            buttonUK.interactable = false;
            buttonUS.interactable = true;
        }
        else
        {
            buttonUK.interactable = true;
            buttonUS.interactable = false;

        }
    }

 

    public void TestSettings()
    {
        TextToSpeech tts;
        tts = GetComponent<TextToSpeech>();
        tts.SetSpeed(EditPlanet.speedTTS);  // changing the speed to text process 
        tts.SetPitch(EditPlanet.pitchTTS);
        tts.Speak("This is my voice. How are you today.");
    }
}
