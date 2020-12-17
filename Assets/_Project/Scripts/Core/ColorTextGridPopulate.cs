using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTextGridPopulate : MonoBehaviour {

    public GameObject prefab;


    void Start ()
    {
        PopulateGrid ();
    }

    void PopulateGrid ()
    {
        foreach (KeyValuePair<int, MarketingColor> m in ColorManager.MarketingColorDict) {
            CreateNewGridItem (m.Key, m.Value);
        }
    }


    void CreateNewGridItem (int mid, MarketingColor m)
    {
        GameObject obj = Instantiate (prefab, transform);
        // set name in Unity Editor
        obj.name = "ColorText" + mid;
        // parent under this
        obj.transform.SetParent (gameObject.transform, false);
        // call init on obj
        obj.GetComponent<ColorText> ().Init (mid, m);
    }

}