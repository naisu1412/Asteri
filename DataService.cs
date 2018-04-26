using SQLite4Unity3d;
using UnityEngine;
#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService   {

	private SQLiteConnection _connection;

	public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
       // Debug.Log("Final PATH: " + dbPath);     

	}

	public void CreateDB(){
		_connection.DropTable<Person> ();
		_connection.CreateTable<Person> ();
    
		_connection.InsertAll (new[]{
			new Person{
				Id = 1,
				Name = "Tom",
				Surname = "Perez",
				Age = 56
			},
			new Person{
				Id = 2,
				Name = "Fred",
				Surname = "Arthurson",
				Age = 16
			},
			new Person{
				Id = 3,
				Name = "John",
				Surname = "Doe",
				Age = 25
			},
			new Person{
				Id = 4,
				Name = "Roberto",
				Surname = "Huertas",
				Age = 37
			}
		});
	}

	public IEnumerable<Planet> GetPlanet(){
		return _connection.Table<Planet>();
	}

    //not needed
	public IEnumerable<Planet> GetPlanetList(){
 
        return _connection.Table<Planet>().Where(x => x.Name == "Earth");
	}


    public IEnumerable<didYouKnow> GetTrivias(int _id)
    {
        return _connection.Table<didYouKnow>().Where(x => x.triviaID == _id);
    }


    //not needed
    //public Person GetJohnny(){
    //	return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
    //}

    public IEnumerable<Planet> updatePlanet()
    {
        string query = "UPDATE Planet SET Name = 'NewPlanet'";
        query = "DELETE FROM Planet WHERE PlanetID = 1";
        _connection.Execute(query);


        return _connection.Table<Planet>().Where(x => x.Name == "Earth");

    }


    public void Reset()
    {
        //reset for that database
        string query = "DELETE FROM PlanetUser;  ";
        _connection.Execute(query);
        query = "UPDATE SolarSystemUser SET State = 0";
        _connection.Execute(query);

    }


    public IEnumerable<Settings> getValueSetting(string _name)
    {
        return _connection.Table<Settings>().Where(x => x.settingName == _name);
    }

    public void setSettings(string _name, string _value)
    {
        string query;
        query = "UPDATE Settings SET settingValue = "+ _value +" WHERE settingName = '" + _name + "';";
        _connection.Execute(query);

    }



    public void SaveData(string Name, string Position, string Scale, int SolarSystemID, float  mass)  // mag sasave ng data ng user 
    {
        // dito gagamitin yung query ng mga user 
        //sasave sa Planet na Table
        string query;

        //then tsaka lang mag iinsert
        query = "INSERT INTO PlanetUser(Name,Position,Scale,SolarSystemID) VALUES(";
        query += "'" + Name + "',";
        query += "'" + Position + "',";
        query += "'" + Scale + "',";
        query += "'" + SolarSystemID + "'";
        query += ");";  //Updating Planet

       

        Debug.Log(query);
        _connection.Execute(query);





        //   return _connection.Table<PlanetUser>().Where(x => x.Name == "Earth");

    }

    public void UpdateSaveData(int _solarSystemID)
    {
        string query;
        query = "UPDATE SolarSystemUser SET State = 1 WHERE SolarSystemId = " + _solarSystemID + ";";
        _connection.Execute(query);
    }

    public void DeleteData(int id)
    {
        //deleting muna mga other soalr system na may ganun solarsystemID 
        string query = "DELETE FROM PlanetUser WHERE SolarSystemID = " + id + "";
        _connection.Execute(query);
    }


    // loading Planets
    public IEnumerable<PlanetUser> GetPlanetRB(int SolarSystemID)
    {


        return _connection.Table<PlanetUser>().Where(x => x.SolarSystemID == SolarSystemID);

        //return _connection.Table<PlanetUser>();
    }

    string _planetComponent;
    public IEnumerable<Planet> SortBy(string something)
    {

        //string query = "SELECT * FROM Planet ORDER BY Mass ASC;";
        //_connection.Execute(query);
        
        switch (something)
        {
            case "mass":
                return _connection.Table<Planet>().OrderBy(x => x.Mass);
               
            case "name":
                return _connection.Table<Planet>().OrderBy(x => x.Name);

            case "radius":
                return _connection.Table<Planet>().OrderBy(x => x.Radius);
            case "density":
                return _connection.Table<Planet>().OrderBy(x => x.Density);
            case "surfacegravity":
                return _connection.Table<Planet>().OrderBy(x => x.SurfaceGravity);
            case "age":
                return _connection.Table<Planet>().OrderBy(x => x.Age);
            case "escapevelocity":
                return _connection.Table<Planet>().OrderBy(x => x.EscapeVelocity);

            default:
                break;
        }
        return _connection.Table<Planet>();
     
    }


    public IEnumerable<Constellation> GetConstellation(string constellationName) {

        return _connection.Table<Constellation>().Where(x=> x.Name == constellationName);

    }

    public IEnumerable<SolarSystemUser>  getSolarSystem()
    {
        return _connection.Table<SolarSystemUser>();
    }




    public Person CreatePerson(){
		var p = new Person{
				Name = "Johnny",
				Surname = "Mnemonic",
				Age = 21
		};
		_connection.Insert (p);
		return p;
	}
}
