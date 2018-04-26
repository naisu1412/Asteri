using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
	void Start () {
		var ds = new DataService ("Asterii.db");
		//ds.CreateDB ();
		var planet = ds.GetPlanet ();

		ToConsole (planet);
//        planet = ds.updatePlanet();
        // planet = ds.GetPlanetList();
       

		// ds.CreatePerson ();
		// ToConsole("New person has been created");
		// var p = ds.GetJohnny ();
		// ToConsole(p.ToString());

	}
	
	private void ToConsole(IEnumerable<Planet> planets){
		foreach (var planet in planets) {
			ToConsole(planet.ToString());
		}
	}

	private void ToConsole(string msg){
		
		Debug.Log (msg);
	}

}
