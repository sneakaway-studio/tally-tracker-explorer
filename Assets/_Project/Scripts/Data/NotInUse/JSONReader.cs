using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


using System.Collections;
using System;
using System.Net;
using System.IO;


//[System.Serializable]
//public class Gradient {
//    public int mid;
//    public string hex1;
//    public string hex2;
//    public int tier1;
//}
//[System.Serializable]
//public class Gradients {
//    public Gradient gradients;
//}





//[System.Serializable]
//public class GradientDataPoint {
//    public int mid { get; set; }
//    public string hex1 { get; set; }
//    public string hex2 { get; set; }
//    public int tier1 { get; set; }
//}
//[System.Serializable]
//public class GradientData {
//    public Dictionary<int, GradientDataPoint> data;
//}





//[System.Serializable]
//public class Employee {
//    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
//    public string firstName;
//    public string lastName;
//}

//[System.Serializable]
//public class Employees {
//    //employees is case sensitive and must match the string "employees" in the JSON.
//    public Employee [] employees;
//}

public class JSONReader : MonoBehaviour {
    public TextAsset employeesFile;
    public TextAsset gradientsFile;

    //void Start ()
    //{
    //    //Employees employeesInJson = JsonUtility.FromJson<Employees> (employeesFile.text);

    //    //foreach (Employee employee in employeesInJson.employees) {
    //    //    Debug.Log ("Found employee: " + employee.firstName + " " + employee.lastName);
    //    //}




    //        //Dictionary<int, GradientDataPoint> grads = new Dictionary<int, GradientDataPoint> ();

    //        //GradientData.data = new Dictionary<int, GradientDataPoint> ();


    //        GradientData d = JsonUtility.FromJson<GradientData> (gradientsFile.text);


    //        foreach (int item in d.data) {
    //            grads.Add (item.mid, item);

    //        }

    //        foreach (var item in grads) {
    //            Debug.Log (item.ToString ());

    //        }




    //        //object obj = JsonHelper.Deserialize (gradientsFile.text);

    //        //foreach (var x in obj) {
    //        //    Debug.Log (obj.ToString ());
    //        //}


    //        //return;

    //        //JObject obj = JObject.Parse (gradientsFile.text);

    //        ////Debug.Log (obj.ToString ());

    //        //// loop through array and add each 
    //        //foreach (var item in obj) {
    //        //    //Debug.Log (item.Key.ToString () + "," + item.Value.ToString ());

    //        //    JObject d = JObject.Parse (item.Value.ToString ());
    //        //    Debug.Log (d.Property ("mid").ToString ());



    //        //    //GradientData o = new GradientData {

    //        //    //    mid = (int)d ["mid"],
    //        //    //    hex1 = (string)d ["hex1"],
    //        //    //    hex2 = (string)d ["hex2"],
    //        //    //    tier1 = (int)d ["tier1"]
    //        //    //};

    //        //    //int key = d.mid;



    //        //}


    //        //Gradients gradientsInJson = JsonUtility.FromJson<Gradients> (gradientsFile.text);



    //        //foreach (Gradient gradient in gradientsInJson.gradients) {
    //        //    Debug.Log ("Found gradient: " + gradient.mid + " " + gradient.hex1);
    //        //}
    //    }
}