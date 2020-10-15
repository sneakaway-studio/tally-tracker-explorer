using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


/**
 *  Deserialize external JSON file using Newtonsoft.Json 
 *  - File is located in Assets/ and referenced using TextAsset in Inspector
 *  - JSON is an object of objects so code uses JObject
 *  - Stores objects in Dictionary for later use
 */


// define a custom class to hold each JSON object in the data returned
[System.Serializable]
public class GradientsByMidPoint {
    public int mid { get; set; }
    public string hex1 { get; set; }
    public string hex2 { get; set; }
    public int tier1 { get; set; }
}
[System.Serializable]
public class MonstersByMidPoint {
    public int mid { get; set; }
    public string slug { get; set; }
    public int tier1id { get; set; }
    public int parent { get; set; }
    public string name { get; set; }
    public string tags { get; set; }
    public float accuracy { get; set; }
    public float attack { get; set; }
    public float defense { get; set; }
    public float evasion { get; set; }
    public float health { get; set; }
    public float stamina { get; set; }
    public int cp { get; set; }
}






public class JsonData : MonoBehaviour {

    // GradientsByMid
    public TextAsset gradientsByMidFile;
    public Dictionary<string, GradientsByMidPoint> GradientsByMid;

    // MonstersByMid
    public TextAsset monstersByMidFile;
    public Dictionary<string, MonstersByMidPoint> MonstersByMid;


    void Awake ()
    {
        // GradientsByMid
        GradientsByMid = new Dictionary<string, GradientsByMidPoint> ();
        ParseGradientsByMid (gradientsByMidFile.text);

        // MonstersByMid
        MonstersByMid = new Dictionary<string, MonstersByMidPoint> ();
        ParseMonstersByMid (monstersByMidFile.text);
    }


    void ParseGradientsByMid (string text)
    {
        // parse the string as JObject
        JObject jsonData = JObject.Parse (text);

        print ("---- GradientsByMidPoint -> count: " + jsonData.Count);

        // loop through each, accessing it as a string:JToken
        foreach (KeyValuePair<string, JToken> item in jsonData) {
            //// the key and value
            //Debug.Log ("key/val = " + item.Key + " -->" + item.Value);
            //// access a value on the object
            //Debug.Log (jsonData [item.Key].Value<string> ("hex1"));

            // store the value in a dict
            GradientsByMid.Add (item.Key, item.Value.ToObject<GradientsByMidPoint> ());
        }
    }


    void ParseMonstersByMid (string text)
    {
        // parse the string as JObject
        JObject jsonData = JObject.Parse (text);

        print ("---- MonstersByMidPoint -> count: " + jsonData.Count);

        // loop through each, accessing it as a string:JToken
        foreach (KeyValuePair<string, JToken> item in jsonData) {
            //// the key and value
            //Debug.Log ("key/val = " + item.Key + " -->" + item.Value);
            //// access a value on the object
            //Debug.Log (jsonData [item.Key].Value<string> ("hex1"));

            // store the value in a dict
            MonstersByMid.Add (item.Key, item.Value.ToObject<MonstersByMidPoint> ());
        }
    }



}
